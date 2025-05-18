using System;
using static Assets.Scripts.Spells.Spell;

namespace Assets.Scripts.GameDatas
{
    [Serializable]
    public class SpellInfoData
    {
        public SpellClassType spellClassType { get; set; }
        public int manaCost { get; set; }
        public int level { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int spellRewardLevel { get; set; }
        public string nameES { get; set; }
        public string descriptionES { get; set; }
        public int slot { get; set; }
    }
}
