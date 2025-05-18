using Assets.Scripts.DemonAbilities;
using Assets.Scripts.MatchUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.GameplayElements.Card;
using static Assets.Scripts.Spells.Spell;

namespace Assets.Scripts.Managers
{
    public static class GameEvents
    {
        public enum HeroeLocalRemoteType { None, Local, Remote }

        public static Func<string, IEnumerator> OnCandyAbilitySuccess;
        public static Func<List<UIDeckCard>> GetUIPlayerCardLocal;

        public static Action<UICard> OnSelectableSelected;
        public static Action<TypeCardSelection, Suit, Value> ActivateSelectableCard;

        public static Action<SpellClassType, HeroeLocalRemoteType> OnAddRoundSpellAbility;

        public static Action SetAbilityCondition;
        public static Action<Ability> OnSetAbilityCondition;
        public static Action SetHeroeAbilityCondition;
        public static Action OnSetHeroeAbilityCondition;
        public static Action SetDeityAbilityCondition;
        public static Action OnSetDeityAbilityCondition;
        public static Action SetSpellCondition;
        public static Action OnSetSpellCondition;

        public static Action<int, int> OnBlindUpdate;
        public static Action<object[]> OnUIUpdateAmountBet;

        public static Action<int, int> OnUIUpdateSouls;
        public static Action<string> OnUITributeToPot;

        public static Action<int> SetGemCostDeity;
        public static Action<int> ShowFXGemDeity;

        public static Action OnChangeLanguageDone;

        public static Action ShowSpells;

        public static Action<int, HeroeLocalRemoteType> OnUpdateMana;
        public static Action<int, HeroeLocalRemoteType> OnUpdateMoral;
        public static Func<HeroeLocalRemoteType, IEnumerator> OnUpdateBottleMana;
        public static Func<HeroeLocalRemoteType, IEnumerator> OnUpdateBottleMoral;

        public static IEnumerator RunEvents(List<Func<object[], IEnumerator>> actions, params object[] args)
        {
            for (int i = 0; i < actions.Count; i++)
            {
                if (actions[i] != null)
                {
                    yield return actions[i].Invoke(args);
                }
            }

            for (int i = actions.Count - 1; i >= 0; i--)
            {
                if (actions[i] == null)
                {
                    actions.RemoveAt(i);
                }
            }
        }

        public static IEnumerator RunEvents(List<Action<object[], Action>> actions, params object[] args)
        {
            actions.Remove(null);

            int toComplete = actions.Count;
            int completedActions = 0;

            void OnCompleteAction()
            {
                completedActions++;
            }

            for (int i = 0; i < actions.Count; i++)
            {
                if (actions[i] != null)
                {
                    actions[i].Invoke(args, OnCompleteAction);
                }
            }

            yield return new WaitUntil(() => completedActions >= actions.Count);
        }
    }
}
