using System;
using static Assets.Scripts.DemonAbilities.Ability;
using System.Collections.Generic;
using Assets.Scripts.DemonAbilities;
using Assets.Scripts.Managers;
using UnityEditor.Localization.Plugins.XLIFF.V12;

namespace Assets.Scripts.GameplayElements
{
    [Serializable]
    public class Card
    {
        public enum TypeCardSelection { None, HeroeLocal, HeroeRemote, Table, HandLocal, HandRemote, Pet }

        public enum Suit { All = -1, Fire, Water, Land, Air };
        public enum Value { All = -1, Two = 2, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace };

        public Suit suit { get; set; }
        public Value value { get; set; }

        public AbilityClassType[] abilitiesType { get; set; }
        public bool[] isRandomAbility { get; set; }

        public bool[] abilitiesCasted { get; set; }

        public int[] manaCosts { get; set; }
        public int LifePoints { get; set; }
        public int MaxLifePoints { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Initiative { get; set; }
        public int Edition { get; set; }
        public string CardName { get; set; }
        public bool isSleep { get; set; }
        public int idCard { get; set; }


        private List<Ability> abilities = null;
        public List<Ability> Abilities { get { return abilities; } }

        private bool isDefeated = false;
        public bool IsDefeated { get { return isDefeated; } }


        public Card()
        {
            abilities = new List<Ability>();

            abilitiesType = new AbilityClassType[0];
            isRandomAbility = new bool[0];
            manaCosts = new int[0];
        }

        public Card(Suit s, Value v)
        {
            value = v;
            suit = s;
        }

        public void SetAbilitiesCard(int actorNumber, int idCard)
        {
            abilities.Clear();

            for (int i = 0; i < abilitiesType.Length; i++)
            {
                Ability ability = DemonMenuAbilityManager.Instance.GetAbility(abilitiesType[i]);
                abilities.Add(ability);
            }
        }
    }
}