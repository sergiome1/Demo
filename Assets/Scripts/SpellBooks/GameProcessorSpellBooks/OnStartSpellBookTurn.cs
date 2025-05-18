using Assets.Scripts.GameProcessors;
using Assets.Scripts.Managers;
using System.Collections;

namespace Assets.Scripts.SpellBooks.GameProcessorSpellBooks
{
    public class OnStartSpellBookTurn : GameProcessor
    {
        public override IEnumerator ProcessAsync(string data)
        {
            GameEvents.ShowSpells?.Invoke();

            yield break;
        }
    }
}
