using Assets.MirrorApp;
using Assets.Scripts.Coroutines;
using Assets.Scripts.FSMs;
using Assets.Scripts.GameProcessors;
using Assets.Scripts.Memories;

namespace Assets.Scripts.SpellBooks.GameProcessorSpellBooks
{
    public class OnEndSpellTurn : GameProcessor
    {
        public override void Process(string data)
        {
            FSM.Instance.SpellBookFSM.ChangeToNone();

            CoroutineManager.Instance.RunCoroutine(FSM.Instance.HeroeUIActionsFSM.ChangeToNone());

            if (Memory.CurrentActorNumberTurn == MirrorAppClient.Instance.LocalActorNumber)
            {
                CoroutineManager.Instance.RunCoroutine(FSM.Instance.HeroeUIActionsFSM.ChangeToStart());
            }
        }
    }
}
