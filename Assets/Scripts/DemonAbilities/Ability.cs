
using Assets.Scripts.DemonAbilities.Data;
using Assets.Scripts.FSMs;
using Assets.Scripts.Managers;
using Newtonsoft.Json;
using System.Collections;
using System.Net.Sockets;

namespace Assets.Scripts.DemonAbilities
{
    public abstract class Ability
    {
        protected string message;

        public enum AbilityClassType
        {
            None,
            Armageddon,
            HandSwapper,
            Revelation
        }
        public abstract AbilityClassType AbilityType { get; }

        public IEnumerator RunAbility(int targetActorNumber, string responseData, string tableCards)
        {
            yield return ApplyAbility(targetActorNumber, responseData, tableCards);
        }

        public virtual void DefineRequest(AbilityData abilityData)
        {
            OnDemonAbilityRequestDefined(abilityData);
        }

        public abstract bool CanBeActivated(int actorNumber);

        protected IEnumerator ShowTitlePopupMessage(string message)
        {
            yield return GameEvents.OnCandyAbilitySuccess?.Invoke(message);
        }

        protected virtual IEnumerator ApplyAbility(int targetActorNumber, string responseData, string sTableCards)
        {
            FSM.Instance.DemonMenuAbilityFSM.ChangeToEndTurn(string.Empty);

            yield break;
        }

        protected void OnDemonAbilityRequestDefined(AbilityData abilityData)
        {
            FSM.Instance.DemonMenuAbilityFSM.ChangeToApplyAbility(JsonConvert.SerializeObject(abilityData));
        }
    }
}
