using Assets.Scripts.Coroutines;
using Assets.Scripts.DemonAbilities.Data;
using Assets.Scripts.DemonAbilities.Request;
using Assets.Scripts.DemonAbilities.Response;
using Assets.Scripts.Managers;
using Assets.Scripts.MatchUI;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using static Assets.Scripts.GameplayElements.Card;

namespace Assets.Scripts.DemonAbilities.Implementations
{
    public class HandSwapper : Ability
    {
        public override AbilityClassType AbilityType { get { return AbilityClassType.HandSwapper; } }

        private AbilityData abilityData;

        private bool isOnUpdatedPlayerCard = false;

        public HandSwapper() : base()
        {
            message = LocalizationManager.Instance.GetEntryValue(LocalizationManager.SelectOneHandCard);
        }

        public override bool CanBeActivated(int actorNumber)
        {
            bool isActivable = false;

            /*
             * ...
             * 
             */

            return isActivable;
        }


        protected override IEnumerator ApplyAbility(int targetActorNumber, string responseData, string sTableCards)
        {
            string abilityName = LocalizationManager.GetLocalizedString(LocalizationManager.DEMONS_ABILITY_TABLE, AbilityType.ToString().ToUpper());
            CoroutineManager.Instance.RunCoroutine(ShowTitlePopupMessage(abilityName));

            HandSwapperResponse response = JsonConvert.DeserializeObject<HandSwapperResponse>(responseData);

            /*
             * ...
             * 
             */

            yield return base.ApplyAbility(targetActorNumber, responseData, sTableCards);
        }

        public override void DefineRequest(AbilityData abilityData)
        {
            this.abilityData = abilityData;

            GameEvents.OnSelectableSelected += OnSelectPlayerCardTarget;
            SelectPlayerCardTarget();
        }

        private void OnSelectPlayerCardTarget(UICard uICard)
        {
            GameEvents.OnSelectableSelected -= OnSelectPlayerCardTarget;

            HandSwapperRequest request = new HandSwapperRequest();
            request.idCard = uICard.IdCard;

            abilityData.idCard = uICard.IdCard;
            abilityData.extraData = JsonConvert.SerializeObject(request);

            OnDemonAbilityRequestDefined(abilityData);
        }

        private void SelectPlayerCardTarget()
        {
            List<UIDeckCard> playerCards = GameEvents.GetUIPlayerCardLocal();

            for (int i = 0; i < playerCards.Count; i++)
            {
                    GameEvents.ActivateSelectableCard?.Invoke(TypeCardSelection.HandLocal, playerCards[i].Card.suit, playerCards[i].Card.value);
            }
        }
    }
}