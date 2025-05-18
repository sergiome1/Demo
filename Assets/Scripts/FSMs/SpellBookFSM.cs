using System;
using UnityEngine;

namespace Assets.Scripts.FSMs
{
    public class SpellBookFSM : MonoBehaviour
    {
        public enum SpellBookStep
        {
            None,
            StartTurn,
            ApplySpell,
            EndTurn,
            StartingSpell
        }

        private SpellBookStep step = SpellBookStep.None;

        public SpellBookStep Step { get { return step; } }

        public Action<string> OnStartTurn;
        public Action<string> OnApplySpell;
        public Action<string> OnEndTurn;

        public void ChangeToNone()
        {
            step = SpellBookStep.None;
        }

        public void ChangeToStartTurn(string data)
        {
            ChangeState(SpellBookStep.StartTurn, data);
        }

        public void ChangeToStartingSpell()
        {
            ChangeState(SpellBookStep.StartingSpell, string.Empty);
        }

        public void ChangeToApplySpell(string data)
        {
            ChangeState(SpellBookStep.ApplySpell, data);
        }

        public void ChangeToEndTurn(string data)
        {
            ChangeState(SpellBookStep.EndTurn, data);
        }

        private void ChangeState(SpellBookStep step, string data)
        {
            this.step = step;

            switch (step)
            {
                case SpellBookStep.StartTurn:
                    OnStartTurn.Invoke(data);
                    break;
                case SpellBookStep.ApplySpell:
                    OnApplySpell.Invoke(data);
                    break;
                case SpellBookStep.EndTurn:
                    OnEndTurn.Invoke(data);
                    break;
            }
        }
    }
}