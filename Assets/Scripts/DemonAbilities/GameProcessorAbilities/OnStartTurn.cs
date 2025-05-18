using Assets.Scripts.Managers;
using Assets.Scripts.MatchUI;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.GameProcessors.DemonAbilities.GameProcessors
{
    public class OnStartTurn : GameProcessor
    {
        public override IEnumerator ProcessAsync(string data)
        {
            List<UIDeckCard> playerCards = GameEvents.GetUIPlayerCardLocal?.Invoke();

            int idCard = int.Parse(data);

            yield return ((UICardLocal)playerCards[idCard]).OnDemonMenuAbilityEnter();
        }
    }
}
