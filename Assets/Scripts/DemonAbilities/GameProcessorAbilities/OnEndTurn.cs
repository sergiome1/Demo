using Assets.Scripts.FSMs;
using System.Collections;

namespace Assets.Scripts.GameProcessors.DemonAbilities.GameProcessors
{
    public class OnEndTurn : GameProcessor
    {
        public override IEnumerator ProcessAsync(string data)
        {
            FSM.Instance.DemonMenuAbilityFSM.ChangeToNone();

            /*
             * ...
             * 
             */

            yield break;
        }
    }
}
