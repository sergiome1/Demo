using System;
using UnityEngine;

namespace Assets.Scripts.FSMs
{
    public class DeityTurnFSM : MonoBehaviour
    {
        public enum TurnStep
        {
            None,
            StartTurn,
            ApplyDeityAbility,
            EndTurn,
        }

        private TurnStep step = TurnStep.None;

        public TurnStep Step { get { return step; } }

        public Action<string> OnStartTurn;
        public Action<string> OnApplyDeityAbility;
        public Action<string> OnEndTurn;

        public void ChangeToNone()
        {
            step = TurnStep.None;
        }

        public void ChangeToStartTurn(string data)
        {
            ChangeState(TurnStep.StartTurn, data);
        }

        public void ChangeToApplyDeityAbility(string data)
        {
            ChangeState(TurnStep.ApplyDeityAbility, data);
        }

        public void ChangeToEndTurn()
        {
            ChangeState(TurnStep.EndTurn, null);
        }

        private void ChangeState(TurnStep step, string data)
        {
            this.step = step;

            switch (step)
            {
                case TurnStep.StartTurn:
                    OnStartTurn.Invoke(data);
                    break;
                case TurnStep.ApplyDeityAbility:
                    OnApplyDeityAbility.Invoke(data);
                    break;
                case TurnStep.EndTurn:
                    OnEndTurn.Invoke(data);
                    break;
            }
        }

    }
}
