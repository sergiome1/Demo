using Assets.Scripts.Coroutines;
using Assets.Scripts.Deities;
using Assets.Scripts.GameDatas;
using Assets.Scripts.Managers;
using Assets.Scripts.Memories;
using System;
using System.Collections.Generic;
using static Assets.Scripts.Deities.Deity;
using static Assets.Scripts.DemonAbilities.Ability;
using static Assets.Scripts.GameDatas.Attributes;
using static Assets.Scripts.GameDatas.NFTData;
using static Assets.Scripts.GameDatas.Stats;
using static Assets.Scripts.Managers.GameEvents;
using static Assets.Scripts.Spells.Spell;

namespace Assets.Scripts.Heroes
{
    [Serializable]
    public class Heroe
    {
        public enum HeroeType
        {
            None,
            Hector,
            Tiresias,
            Herakles,
            Odysseus,
            Theseus,
            Achilles,
            Psyche,
            Eneas,
            Orpheus,
            Atalanta,
            Perseus,
            Helena,
            Leonidas,
            Circe,
            Jason
        }

        public enum HeroeClassType
        {
            None,
            Warrior,
            Ranger,
            Paladin,
            Wizard,
            Bard
        }

        public string heroeId { get; set; }
        public HeroeType heroeType { get; set; }
        public HeroeClassType heroeClassType { get; set; }
        public Attributes progressionAttributes { get; set; }
        public Attributes modificatorAttributes { get; set; }
        public Stats progressionStats { get; set; }
        public Attributes baseAttributes { get; set; }
        public Stats baseStats { get; set; }
        public int points { get; set; }
        public int edition { get; set; }
        public int level { get; set; }
        public int circle { get; set; }
        public List<SpellInfoData> spellInfoDatas { get; set; }
        public DeityClassType DeityType { get; set; }
        public string petAssetId { get; set; }
        public string weaponAssetId { get; set; }
        public string shieldAssetId { get; set; }
        public string armourAssetId { get; set; }
        public string helmetAssetId { get; set; }
        public string petNftHash { get; set; }
        public string weaponNftHash { get; set; }
        public string shieldNftHash { get; set; }
        public string armourNftHash { get; set; }
        public string helmetNftHash { get; set; }
        public string circleNftHash { get; set; }
        public ItemData weaponNftData { get; set; }
        public ItemData shieldNftData { get; set; }
        public ItemData armourNftData { get; set; }
        public ItemData helmetNftData { get; set; }
        public Rarity HeroeRarity { get; set; }
        public Stats matchStats { get; set; }
        public int heroeAbilityCountLeft { get; set; }
        public bool isAttributesCorrect { get; set; }

        private Deity deity = null;
        public Deity Deity { get { return deity; } }

        public bool IsDefeated { get { return matchRunningStats.LifePoints == 0; } }


        private Dictionary<AbilityClassType, object> roundAbilities = null;
        public Dictionary<AbilityClassType, object> RoundAbilities { get { return roundAbilities; } }

        private Dictionary<HeroeType, string> roundHeroAbilities = null;
        public Dictionary<HeroeType, string> RoundHeroAbilities { get { return roundHeroAbilities; } }

        private Dictionary<SpellClassType, object> roundSpells = null;
        public Dictionary<SpellClassType, object> RoundSpells { get { return roundSpells; } }

        private Dictionary<SpellClassType, object> turnSpells = null;
        public Dictionary<SpellClassType, object> TurnSpells { get { return turnSpells; } }

        private Dictionary<DeityClassType, object> roundDeityAbilities = null;
        public Dictionary<DeityClassType, object> RoundDeityAbilities { get { return roundDeityAbilities; } }

        private int maxMana = 0;
        public int MaxMana { get { return maxMana; } }

        private int maxMoral = 0;
        public int MaxMoral { get { return maxMoral; } }

        private int maxLifePoints = 0;
        public int MaxLifePoints { get { return maxLifePoints; } }

        private Stats matchRunningStats = null;

        public Heroe()
        {
            roundAbilities = new Dictionary<AbilityClassType, object>();
            roundHeroAbilities = new Dictionary<HeroeType, string>();
            roundSpells = new Dictionary<SpellClassType, object>();
            turnSpells = new Dictionary<SpellClassType, object>();
            roundDeityAbilities = new Dictionary<DeityClassType, object>();
            matchRunningStats = new Stats();
            isAttributesCorrect = true;
        }

        public void SetModificatorAttribute(AttributeType attributeType, int value)
        {
            modificatorAttributes.SetAttribute(attributeType, value);
        }

        public int GetAttribute(AttributeType attributeType)
        {
            int attribute = GetBaseAttribute(attributeType) + GetModifiedAttribute(attributeType);

            if (modificatorAttributes.GetAttribute(attributeType) > 0)
            {
                return attribute * modificatorAttributes.GetAttribute(attributeType);
            }
            else
            {
                return attribute;
            }
        }

        public int GetBaseAttribute(AttributeType attributeType)
        {
            return baseAttributes.GetAttribute(attributeType);
        }

        public int GetModifiedAttribute(AttributeType attributeType)
        {
            return progressionAttributes.GetAttribute(attributeType);
        }

        public int GetBaseAndNFTAttribute(AttributeType attributeType)
        {
            int attribute = baseAttributes.GetAttribute(attributeType) + GetNFTAttribute(attributeType);

            if (modificatorAttributes.GetAttribute(attributeType) > 0)
            {
                return attribute * modificatorAttributes.GetAttribute(attributeType);
            }
            else
            {
                return attribute;
            }
        }

        public int GetProgressionStat(StatType statType)
        {
            return baseStats.GetStat(statType) + progressionStats.GetStat(statType);
        }

        public int GetMatchStat(StatType statType)
        {
            int stat = 0;

            switch (statType)
            {
                case StatType.LifePoints:
                    stat = matchStats.LifePoints + GetAttribute(AttributeType.Constitution);
                    break;
                case StatType.Attack:
                    stat = matchStats.Attack + GetAttribute(AttributeType.Strength);
                    break;
                case StatType.Defense:
                    stat = matchStats.Defense + GetAttribute(AttributeType.Dexterity);
                    break;
                case StatType.Initiative:
                    stat = matchStats.Initiative + GetAttribute(AttributeType.Speed);
                    break;
                case StatType.Mana:
                    stat = matchStats.Mana + GetAttribute(AttributeType.Intelligence);
                    break;
                case StatType.Moral:
                    stat = matchStats.Moral + GetAttribute(AttributeType.Willpower);
                    break;
                case StatType.ExtraTurn:
                    stat = matchStats.ExtraTurn + (GetAttribute(AttributeType.Speed) / 2);
                    break;
            }

            return stat;
        }

        public void SetRunningMatchStats()
        {
            matchRunningStats.LifePoints = GetMatchStat(StatType.LifePoints);
            matchRunningStats.Attack = GetMatchStat(StatType.Attack);
            matchRunningStats.Defense = GetMatchStat(StatType.Defense);
            matchRunningStats.Initiative = GetMatchStat(StatType.Initiative);
            matchRunningStats.Mana = GetMatchStat(StatType.Mana);
            matchRunningStats.Moral = GetMatchStat(StatType.Moral);
            matchRunningStats.ExtraTurn = GetMatchStat(StatType.ExtraTurn);
        }

        public int GetRunningMatchStat(StatType statType)
        {
            int stat = 0;

            switch (statType)
            {
                case StatType.LifePoints:
                    stat = matchRunningStats.LifePoints;
                    break;
                case StatType.Attack:
                    stat = matchRunningStats.Attack;
                    break;
                case StatType.Defense:
                    stat = matchRunningStats.Defense;
                    break;
                case StatType.Initiative:
                    stat = matchRunningStats.Initiative;
                    break;
                case StatType.Mana:
                    stat = matchRunningStats.Mana;
                    break;
                case StatType.Moral:
                    stat = matchRunningStats.Moral;
                    break;
                case StatType.ExtraTurn:
                    stat = matchRunningStats.ExtraTurn;
                    break;
            }

            return stat;
        }

        public void SetHeroe()
        {
            SetDeity();
            SetRunningMatchStats();
        }

        public void SetMaxMatchStats()
        {
            maxMana = GetMatchStat(Stats.StatType.Mana);
            maxMoral = GetMatchStat(Stats.StatType.Moral);
            maxLifePoints = GetMatchStat(Stats.StatType.LifePoints);
        }

        public void SetRunningMatchStat(StatType statType, int value)
        {
            switch (statType)
            {
                case StatType.LifePoints:
                    matchRunningStats.LifePoints = value;
                    break;
                case StatType.Attack:
                    matchRunningStats.Attack = value;
                    break;
                case StatType.Defense:
                    matchRunningStats.Defense = value;
                    break;
                case StatType.Initiative:
                    matchRunningStats.Initiative = value;
                    break;
                case StatType.Mana:
                    matchRunningStats.Mana = value;
                    HeroeLocalRemoteType heroeLocalRemoteType = this == Memory.LocalHeroe ? HeroeLocalRemoteType.Local : HeroeLocalRemoteType.Remote;
                    GameEvents.OnUpdateMana?.Invoke(value, heroeLocalRemoteType);
                    CoroutineManager.Instance.RunCoroutine(GameEvents.OnUpdateBottleMana?.Invoke(heroeLocalRemoteType));
                    break;
                case StatType.Moral:
                    matchRunningStats.Moral = value;
                    heroeLocalRemoteType = this == Memory.LocalHeroe ? HeroeLocalRemoteType.Local : HeroeLocalRemoteType.Remote;
                    GameEvents.OnUpdateMoral?.Invoke(value, heroeLocalRemoteType);
                    CoroutineManager.Instance.RunCoroutine(GameEvents.OnUpdateBottleMoral?.Invoke(heroeLocalRemoteType));
                    break;
                case StatType.ExtraTurn:
                    matchRunningStats.ExtraTurn = value;
                    break;
            }
        }

        public void SetDeity(DeityClassType deityClassType = DeityClassType.None)
        {

            // *** NOTE: Commented on Demo purposes ***

            //    switch (deityClassType)
            //    {
            //        case DeityClassType.Zeus:
            //            deity = new ZeusDeity();
            //            break;
            //        case DeityClassType.Poseidon:
            //            deity = new PoseidonDeity();
            //            break;
            //        case DeityClassType.Athenea:
            //            deity = new AtheneaDeity();
            //            break;
            //        case DeityClassType.Hades:
            //            deity = new HadesDeity();
            //            break;
            //        case DeityClassType.Artemis:
            //            deity = new ArtemisDeity();
            //            break;
            //        case DeityClassType.Aphrodite:
            //            deity = new AphroditeDeity();
            //            break;
            //        case DeityClassType.Apollo:
            //            deity = new ApolloDeity();
            //            break;
            //        case DeityClassType.Hephaestus:
            //            deity = new HephaestusDeity();
            //            break;
            //        case DeityClassType.Hermes:
            //            deity = new HermesDeity();
            //            break;
            //        case DeityClassType.Dionysus:
            //            deity = new DionysusDeity();
            //            break;
            //        case DeityClassType.Ares:
            //            deity = new AresDeity();
            //            break;
            //        case DeityClassType.Demeter:
            //            deity = new DemeterDeity();
            //            break;
            //        case DeityClassType.Hera:
            //            deity = new HeraDeity();
            //            break;
            //        case DeityClassType.None:
            //            deity = null;
            //            break;
            //    }
        }

        private int GetNFTAttribute(AttributeType attributeType)
        {
            int extraValue = 0;

            switch (attributeType)
            {
                case AttributeType.Strength:
                    extraValue += weaponNftData.attributes.Strength;
                    extraValue += shieldNftData.attributes.Strength;
                    extraValue += armourNftData.attributes.Strength;
                    extraValue += helmetNftData.attributes.Strength;
                    break;
                case AttributeType.Dexterity:
                    extraValue += weaponNftData.attributes.Dexterity;
                    extraValue += shieldNftData.attributes.Dexterity;
                    extraValue += armourNftData.attributes.Dexterity;
                    extraValue += helmetNftData.attributes.Dexterity;
                    break;
                case AttributeType.Constitution:
                    extraValue += weaponNftData.attributes.Constitution;
                    extraValue += shieldNftData.attributes.Constitution;
                    extraValue += armourNftData.attributes.Constitution;
                    extraValue += helmetNftData.attributes.Constitution;
                    break;
                case AttributeType.Intelligence:
                    extraValue += weaponNftData.attributes.Intelligence;
                    extraValue += shieldNftData.attributes.Intelligence;
                    extraValue += armourNftData.attributes.Intelligence;
                    extraValue += helmetNftData.attributes.Intelligence;
                    break;
                case AttributeType.Willpower:
                    extraValue += weaponNftData.attributes.Willpower;
                    extraValue += shieldNftData.attributes.Willpower;
                    extraValue += armourNftData.attributes.Willpower;
                    extraValue += helmetNftData.attributes.Willpower;
                    break;
                case AttributeType.Speed:
                    extraValue += weaponNftData.attributes.Speed;
                    extraValue += shieldNftData.attributes.Speed;
                    extraValue += armourNftData.attributes.Speed;
                    extraValue += helmetNftData.attributes.Speed;
                    break;
            }

            return extraValue;
        }

        public string GetLocalizedHeroName()
        {
            string key = string.Format("{0}_NAME", heroeType.ToString().ToUpper());

            return LocalizationManager.GetLocalizedString(LocalizationManager.HEROES_TABLE, key);
        }

        public string GetLocalizedHeroRarity()
        {
            return LocalizationManager.GetLocalizedString(LocalizationManager.RARITY, HeroeRarity.ToString());
        }
    }
}
