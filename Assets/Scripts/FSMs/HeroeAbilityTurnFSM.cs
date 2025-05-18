using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.FSMs
{
    public class HeroeAbilityTurnFSM : MonoBehaviour
    {
        public enum TurnStep
        {
            None,
            StartTurn,
            ApplyHeroeAbility,
            EndTurn,
        }

        private TurnStep step = TurnStep.None;

        public TurnStep Step { get { return step; } }

        public Func<string, IEnumerator> OnStartTurn;
        public Action<string> OnApplyHeroeAbility;
        public Func<string, IEnumerator> OnEndTurn;

        public void ChangeToNone()
        {
            step = TurnStep.None;
        }

        public IEnumerator ChangeToStartTurn(string data)
        {
            yield return ChangeState(TurnStep.StartTurn, data);
        }

        public IEnumerator ChangeToApplyHeroeAbility(string data)
        {
            yield return ChangeState(TurnStep.ApplyHeroeAbility, data);
        }

        public IEnumerator ChangeToEndTurn(string data)
        {
            yield return ChangeState(TurnStep.EndTurn, data);
        }

        private IEnumerator ChangeState(TurnStep step, string data)
        {
            this.step = step;

            switch (step)
            {
                case TurnStep.StartTurn:
                    yield return OnStartTurn.Invoke(data);
                    break;
                case TurnStep.ApplyHeroeAbility:
                    OnApplyHeroeAbility.Invoke(data);
                    break;
                case TurnStep.EndTurn:
                    yield return OnEndTurn.Invoke(data);
                    break;
            }
        }

    }
}
