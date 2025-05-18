using Assets.Scripts.Coroutines;
using Assets.Scripts.Managers;
using System.Collections;

namespace Assets.Scripts.DemonAbilities.Implementations
{
    public class Armageddon : Ability
    {
        public override AbilityClassType AbilityType { get { return AbilityClassType.Armageddon; } }

        public Armageddon() : base() { }

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

            /*
             * ...
             * 
             */

            yield return base.ApplyAbility(targetActorNumber, responseData, sTableCards);
        }
    }
}
