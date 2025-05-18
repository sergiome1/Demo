using System;
using UnityEngine;

namespace Assets.Scripts.FSMs
{
    public class DemonAbilityFSM : MonoBehaviour
    {
        public enum ActionStep
        {
            None,
            StartTurn,
            ApplyAbility,
            EndTurn
        }

        private ActionStep step = ActionStep.None;

        public ActionStep Step { get { return step; } }

        public Action<string> OnStartTurn;
        public Action<string> OnApplyAbility;
        public Action<string> OnEndTurn;

        public void ChangeToNone()
        {
            step = ActionStep.None;
        }

        public void ChangeToStartTurn(string data)
        {
            ChangeState(ActionStep.StartTurn, data);
        }

        public void ChangeToApplyAbility(string data)
        {
            ChangeState(ActionStep.ApplyAbility, data);
        }

        public void ChangeToEndTurn(string data)
        {
            ChangeState(ActionStep.EndTurn, data);
        }

        private void ChangeState(ActionStep step, string data)
        {
            this.step = step;

            switch (step)
            {
                case ActionStep.StartTurn:
                    OnStartTurn.Invoke(data);
                    break;
                case ActionStep.ApplyAbility:
                    OnApplyAbility.Invoke(data);
                    break;
                case ActionStep.EndTurn:
                    OnEndTurn.Invoke(data);
                    break;
            }
        }
    }
}
