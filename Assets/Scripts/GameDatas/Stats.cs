using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.GameDatas
{
    [Serializable]
    public class Stats
    {
        public enum StatType { LifePoints = 1, Attack, Defense, Initiative, Mana, Moral, ExtraTurn }

        public int LifePoints { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Initiative { get; set; }
        public int Mana { get; set; }
        public int Moral { get; set; }
        public int ExtraTurn { get; set; }

        public int GetStat(StatType statType)
        {
            int statValue = 0;

            switch (statType)
            {
                case StatType.LifePoints:
                    statValue = LifePoints;
                    break;
                case StatType.Attack:
                    statValue = Attack;
                    break;
                case StatType.Defense:
                    statValue = Defense;
                    break;
                case StatType.Initiative:
                    statValue = Initiative;
                    break;
                case StatType.Mana:
                    statValue = Mana;
                    break;
                case StatType.Moral:
                    statValue = Moral;
                    break;
                case StatType.ExtraTurn:
                    statValue = ExtraTurn;
                    break;
            }

            return statValue;
        }
    }
}
