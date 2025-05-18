using Assets.MirrorApp;
using Assets.Scripts.Coroutines;
using Assets.Scripts.FSMs;
using Assets.Scripts.GameDatas;
using Assets.Scripts.Heroes;
using Assets.Scripts.HTTPs.Requests;
using Assets.Scripts.Managers;
using Assets.Scripts.Memories;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Assets.Scripts.Heroes.Heroe;

namespace Assets.Scripts.HeroAbilities
{
    public abstract class HeroAbility
    {
        public abstract string NameAbility { get; }
        public abstract string DescriptionAbility { get; }
        public abstract HeroeType HeroeType { get; }

        protected PowerData PowerData;

        protected bool isHeroeAbilityCondition = false;
        public bool IsHeroeAbilityCondition { get { return isHeroeAbilityCondition; } }

        private int totalCount = 0;
        public int TotalCount { get { return totalCount; } }

        public HeroAbility()
        {
            GameEvents.SetHeroeAbilityCondition += SetHeroeAbilityCondition;
        }

        protected virtual void SetHeroeAbilityCondition()
        {
            isHeroeAbilityCondition = CanBeActivated(Memory.LocalHeroe, MirrorAppClient.Instance.LocalActorNumber);

            GameEvents.OnSetHeroeAbilityCondition?.Invoke();
        }

        public void SetAbilityCounts(int heroeAbilityCountLeft, HeroeType heroeType)
        {
            totalCount = heroeAbilityCountLeft;
        }

        public IEnumerator RunHeroeAbility(int targetActorNumber, string responseData, HeroeType heroeType, string tableCards)
        {
            UnityEngine.Debug.Log(System.DateTime.UtcNow.ToString() + " | " + "RunHeroeAbility");

            yield return ApplyHeroeAbility(targetActorNumber, responseData, heroeType, tableCards);
        }

        protected virtual IEnumerator ApplyHeroeAbility(int targetActorNumber, string responseData, HeroeType heroeType, string sTableCards)
        {
            if (MirrorAppClient.Instance.LocalActorNumber == Memory.CurrentActorNumberTurn)
            {
                Memory.LocalHeroe.heroeAbilityCountLeft--;
            }

            GameEvents.SetHeroeAbilityCondition?.Invoke();

            yield return OnApplyAbilityDone(heroeType);
        }

        private IEnumerator OnApplyAbilityDone(HeroeType heroeType)
        {
            yield return FSM.Instance.HeroeAbilityTurnFSM.ChangeToEndTurn(((int)heroeType).ToString());
        }

        public virtual void DefineRequest(PowerData powerData)
        {
            GameEvents.SetHeroeAbilityCondition?.Invoke();

            ApplyHeroeAbilityRequest request = new ApplyHeroeAbilityRequest(
            MirrorAppClient.Instance.LocalActorNumber,
            MirrorAppClient.Instance.RemoteActorNumber,
            (int)HeroeType,
            powerData);

            CoroutineManager.Instance.RunCoroutine(FSM.Instance.HeroeAbilityTurnFSM.ChangeToApplyHeroeAbility(JsonConvert.SerializeObject(request)));
        }

        public virtual ApplyHeroeAbilityRequest DefineAIRequest(PowerData abilityData)
        {
            return new ApplyHeroeAbilityRequest(
                MirrorAppClient.Instance.RemoteActorNumber,
                MirrorAppClient.Instance.LocalActorNumber,
                 (int)HeroeType,
                abilityData
                );
        }

        protected IEnumerator ShowTitlePopupMessage(string message)
        {
            yield return GameEvents.OnCandyAbilitySuccess?.Invoke(message);
        }

        public virtual bool CanBeActivated(Heroe heroe, int actorNumber)
        {
            return heroe.heroeAbilityCountLeft > 0 &&
                Memory.CurrentActorNumberTurn == actorNumber;
        }

        public void Clean()
        {
            GameEvents.SetHeroeAbilityCondition -= SetHeroeAbilityCondition;
        }
    }
}
