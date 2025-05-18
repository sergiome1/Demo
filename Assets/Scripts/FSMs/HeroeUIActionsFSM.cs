using Assets.Scripts.Coroutines;
using Assets.Scripts.DemonAbilities;
using Assets.Scripts.HeroAbilities;
using Assets.Scripts.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.FSMs
{
    public class HeroeUIActionsFSM : MonoBehaviour
    {
        public enum ActionStep
        {
            None,
            Start,
            LaunchHeroeAbility,
            LaunchingDemonAbility,
            LaunchDemonAbility,
            ShowDemon,
            DemonMenuAbility,
            DragPet,
            LaunchCompanionAbility,
            LaunchDeityAbility,
            OpenSpellbook,
            Continue,
            ChangeToStartPet,
            CloseSpellbook,
            LaunchingSpell,
            OpenLaunchPanelAbility
        }

        private ActionStep step = ActionStep.None;

        public ActionStep Step { get { return step; } }

        public Action OnStartEnter;
        public Action<object> OnLaunchHeroeAbilityEnter;
        public Action<object> OnLaunchDemonAbilityEnter;
        public Action<object> OnShowDemonEnter;
        public Action<object> OnDemonMenuAbilityEnter;
        public Action OnLaunchCompanionAbilityEnter;
        public Action<object> OnLaunchDeityAbilityEnter;
        public Action OnOpenSpellbookEnter;
        public List<Func<object[], IEnumerator>> OnContinueEnter = new List<Func<object[], IEnumerator>>();
        public Action OnChangeToStartPetEnter;
        public Func<IEnumerator> OnCloseSpellbookEnter;
        public Action<object> OnOpenLaunchAbilityEnter;

        public Func<IEnumerator> OnShowDemonExit;
        public Func<IEnumerator> OnDemonMenuAbilityExit;
        public Func<IEnumerator> OnContinuebookExit;
        public Func<IEnumerator> OnChangeToStartPetExit;

        public IEnumerator ChangeToNone()
        {
            yield return ChangeState(ActionStep.None, string.Empty);
        }

        public IEnumerator ChangeToStart()
        {
            yield return ChangeState(ActionStep.Start, string.Empty);
        }

        public IEnumerator ChangeToLaunchHeroeAbility(HeroAbility heroeAbility)
        {
            yield return ChangeState(ActionStep.LaunchHeroeAbility, heroeAbility);
        }

        public IEnumerator ChangeToLaunchingDemonAbility()
        {
            yield return ChangeState(ActionStep.LaunchingDemonAbility, string.Empty);
        }

        public IEnumerator ChangeToLaunchingSpell()
        {
            yield return ChangeState(ActionStep.LaunchingSpell, string.Empty);
        }

        public IEnumerator ChangeToLaunchDemonAbility(Ability ability)
        {
            yield return ChangeState(ActionStep.LaunchDemonAbility, ability);
        }

        public IEnumerator ChangeToShowDemon(int data)
        {
            yield return ChangeState(ActionStep.ShowDemon, data);
        }

        public IEnumerator ChangeToDemonMenuAbility(int data)
        {
            yield return ChangeState(ActionStep.DemonMenuAbility, data);
        }

        public IEnumerator ChangeToLaunchCompanionAbility()
        {
            yield return ChangeState(ActionStep.LaunchCompanionAbility, string.Empty);
        }

        public IEnumerator ChangeToLaunchDeityAbility(object deity)
        {
            yield return ChangeState(ActionStep.LaunchDeityAbility, deity);
        }

        public IEnumerator ChangeToOpenSpellbook()
        {
            yield return ChangeState(ActionStep.OpenSpellbook, string.Empty);
        }

        public IEnumerator ChangeToCloseSpellbook()
        {
            yield return ChangeState(ActionStep.CloseSpellbook, string.Empty);
        }

        public IEnumerator ChangeToContinue()
        {
            yield return ChangeState(ActionStep.Continue, string.Empty);
        }

        public IEnumerator ChangeToStartPet()
        {
            yield return ChangeState(ActionStep.ChangeToStartPet, string.Empty);
        }

        private IEnumerator ChangeExitState()
        {
            switch (step)
            {
                case ActionStep.ShowDemon:
                    CoroutineManager.Instance.RunCoroutine(OnShowDemonExit?.Invoke());
                    break;
                case ActionStep.DemonMenuAbility:
                    yield return OnDemonMenuAbilityExit?.Invoke();
                    CoroutineManager.Instance.RunCoroutine(OnShowDemonExit?.Invoke());
                    break;
                case ActionStep.ChangeToStartPet:
                    yield return OnChangeToStartPetExit?.Invoke();
                    break;
            }
        }

        private IEnumerator ChangeState(ActionStep step, object data)
        {
            yield return ChangeExitState();
            this.step = step;

            switch (step)
            {
                case ActionStep.Start:
                    OnStartEnter?.Invoke();
                    break;
                case ActionStep.LaunchHeroeAbility:
                    OnLaunchHeroeAbilityEnter?.Invoke(data);
                    break;
                case ActionStep.LaunchDemonAbility:
                    OnLaunchDemonAbilityEnter?.Invoke(data);
                    break;
                case ActionStep.ShowDemon:
                    OnShowDemonEnter?.Invoke(data);
                    break;
                case ActionStep.DemonMenuAbility:
                    OnDemonMenuAbilityEnter?.Invoke(data);
                    break;
                case ActionStep.LaunchCompanionAbility:
                    OnLaunchCompanionAbilityEnter?.Invoke();
                    break;
                case ActionStep.LaunchDeityAbility:
                    OnLaunchDeityAbilityEnter?.Invoke(data);
                    break;
                case ActionStep.OpenSpellbook:
                    OnOpenSpellbookEnter?.Invoke();
                    break;
                case ActionStep.CloseSpellbook:
                    yield return OnCloseSpellbookEnter?.Invoke();
                    break;
                case ActionStep.Continue:
                    yield return GameEvents.RunEvents(OnContinueEnter, null);
                    break;
                case ActionStep.ChangeToStartPet:
                    OnChangeToStartPetEnter?.Invoke();
                    break;
                case ActionStep.OpenLaunchPanelAbility:
                    OnOpenLaunchAbilityEnter?.Invoke(data);
                    break;
            }
        }
    }
}
