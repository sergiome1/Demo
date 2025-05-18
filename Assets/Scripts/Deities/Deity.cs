using Assets.Scripts.FSMs;
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
using UnityEngine;
using Assets.Scripts.DemonAbilities;
using Assets.Scripts.Spells;
using Assets.Scripts.GameDatas;
using Assets.MirrorApp;
using Assets.Scripts.HTTPs.Requests;
using Assets.Scripts.HTTPs.Responses;
using Assets.Scripts.MatchMessages.Requests;
using Assets.Scripts.MatchMessages.Responses;

namespace Assets.Scripts.Deities
{
    [Serializable]
    public abstract class Deity
    {
        private const int REQUIREDSOULS = 150;

        public enum DeityClassType
        {
            None,
            Zeus,     
            Poseidon, 
            Athenea,  
            Hades,    
            Artemis,  
            Aphrodite,
            Apollo,
            Hephaestus,
            Hermes,
            Dionysus,
            Ares,
            Demeter,
            Hera
        }

        public abstract string NameAbility { get; }
        public abstract string DescriptionAbility { get; }
        public abstract DeityClassType DeityType { get; }

        protected bool isDeityAbilityCondition = true;
        public bool IsDeityAbilityCondition { get { return isDeityAbilityCondition; } }

        public Deity()
        {
            GameEvents.SetDeityAbilityCondition += SetDeityAbilityCondition;
        }

        ~Deity()
        {
            GameEvents.SetDeityAbilityCondition -= SetDeityAbilityCondition;
        }

        public IEnumerator RunAbility(ApplyPowerResponse applyResponse)
        {
            if (applyResponse.responseType != ApplyPowerResponse.ResponseType.Success) yield break;

            if (MirrorAppClient.Instance.LocalActorNumber == Memory.CurrentActorNumberTurn)
            {
                GameEvents.SetAbilityCondition?.Invoke();
                GameEvents.SetHeroeAbilityCondition?.Invoke();

                GameEvents.SetDeityAbilityCondition?.Invoke();
            }

            bool isLocalAttacker = applyResponse.sourceActorNumber == MirrorAppClient.Instance.LocalActorNumber;

            GetPotBetResult result = new GetPotBetResult();
            bool isDone = false;
            yield return HTTPManager.Instance.HellmasterData<GetPotBetMessage, GetPotBetResult>(new GetPotBetMessage()
            {
                roomId = MirrorAppClient.Instance.CurrentRoom
            }, false, response => { result = response; isDone = true; });
            yield return new WaitUntil(() => isDone);

            PotBetsData potBetsData = JsonConvert.DeserializeObject<PotBetsData>(result.potBetsData);

            Pokers.PotBets.Instance.SetPotBets(potBetsData);

            if (applyResponse.sourceActorNumber == potBetsData.actorNumbers[0])
            {
                GameEvents.OnUIUpdateSouls?.Invoke(applyResponse.sourceActorNumber, potBetsData.amountSoulsPlayer1);
            }
            else
            {
                GameEvents.OnUIUpdateSouls?.Invoke(applyResponse.sourceActorNumber, potBetsData.amountSoulsPlayer2);
            }

            GameEvents.ShowFXGemDeity?.Invoke(applyResponse.sourceActorNumber);

            GameEvents.OnUITributeToPot?.Invoke(potBetsData.pot.ToString("N0"));

            yield return ApplyDeityAbility(applyResponse);
        }

        protected virtual void SetDeityAbilityCondition()
        {
            if (IsDeityAbilityCondition)
            {
                isDeityAbilityCondition = CanActivate() &&
                    Memory.CurrentActorNumberTurn == MirrorAppClient.Instance.LocalActorNumber &&
                        !Memory.LocalHeroe.RoundSpells.ContainsKey(Spells.Spell.SpellClassType.Apostasy) &&
                        !Memory.LocalHeroe.RoundDeityAbilities.ContainsKey(DeityType);
            }

            GameEvents.OnSetDeityAbilityCondition?.Invoke();
        }

        public virtual bool CanActivate()
        {
            int requiredSouls = REQUIREDSOULS;

            if (Memory.LocalHeroe.RoundHeroAbilities.ContainsKey(HeroeType.Theseus))
            {
                requiredSouls = 0;
            }

            if (Memory.LocalHeroe.RoundSpells.ContainsKey(Spell.SpellClassType.Sacrifice))
            {
                requiredSouls = 0;
            }

            return Pokers.PotBets.Instance.LocalAmountSouls >= requiredSouls;
        }

        public virtual void DefineRequest(PowerData powerData)
        {
            GameEvents.SetHeroeAbilityCondition?.Invoke();

            ApplyDeityAbilityRequest request = new ApplyDeityAbilityRequest(
            MirrorAppClient.Instance.LocalActorNumber,
            MirrorAppClient.Instance.RemoteActorNumber,
            DeityType,
            powerData);

            FSM.Instance.DeityTurnFSM.ChangeToApplyDeityAbility(JsonConvert.SerializeObject(request));
        }

        protected abstract IEnumerator ApplyDeityAbility(ApplyPowerResponse targetActorNumber);

        protected IEnumerator ShowTitlePopupMessage(string message)
        {
            yield return GameEvents.OnCandyAbilitySuccess?.Invoke(message);
        }

    }
}
