using Assets.MirrorApp;
using Assets.Scripts.FSMs;
using Assets.Scripts.GameDatas;
using Assets.Scripts.Managers;
using Assets.Scripts.Memories;
using Newtonsoft.Json;
using System;
using System.Collections;
using static Assets.Scripts.Managers.GameEvents;

namespace Assets.Scripts.Spells
{
    public abstract class Spell
    {

        protected enum TargetType
        {
            OldMethod,
            LocalPlayer,
            RemotePlayer
        }

        public enum SpellClassType
        {
            None = 0,


            //GenericSpells 

            GodwinPact,                      // Cantrip                 // 0
            ParadiseOfMilton,                // Cantrip                 // 0                        
            BloodofEliphas,                  // Cantrip                 // 0                               
            DarkPact,                                                   // 0                  
            ShareHealth,
            SpectralAura,
            SharpenSenses,
            StairwayToHeaven,
            Vertigo,
            Apostasy,                                                   // *** DONE ***                                                 Candy
            SilkOfSpider,                                               // *** DONE ***                                                 Candy
            Burn,                                                       // *** DONE ***                                                 Candy
            Thundercloud,                                               // *** DONE ***                                                 Candy
            Devour,                                                     // 0
            ExchangeOfHealth,

            Stretch,                        // Level 5 NFTs
            Siesta,
            Disillusionment,
            Mirror,                         // Level 6 NFTs
            Nightmare,
            Delirium,
            SaganExorcimum,                 // Level 7 NFTs
            Panacea,
            TheDayOfTheBeast,

            //CombatMasterSpells

            MonsterKiller = 1000,           // Cantrip                  // 0
            Frenesi,                                                    // 0
            SandEyes,                                                   // 0
            Disarm,                                                     // 0
            Terror,
            RedAnts,                                                    // 0
            WarScream,                                                  // 0
            EnkiduClaws,                    // Level 3                  // 0
            MetalThorns,                                                // 0
            Bite,                                                       // 0
            DemonicHarakiri,                // Level 4                  // 0
            Immolate,                                                   // 0
            Charge,                                                     // 0

            FreedomOfSade,                  // Level 5 NFTs
            Vulcan,
            MagicMushroom,
            Shadow,                         // Level 6 NFTs
            Rage,
            BoneWall,
            Intimidation,                   // Level 7 NFTs
            RavenWings,
            MarkOfCain,

            //HuntMasterSpells

            VampireHunter = 2000,             // Cantrip                // 0  
            ClimbingPlants,                // Level 1                   // 0 pet blocker
            FullMoon,                                                   // 0
            ForestWind,                                                 // 0
            Meditation,                     // Level 2
            Regenerate,
            RapidArrow,
            Boomerang,                      // Level 3
            Camouflage,
            ToadOil,
            Swirl,                          // Level 4
            ToxicCloud,
            Mud,

            Stampede,                       // Level 5 NFTs
            BearHug,
            StoneAnimal,
            Dust,                           // Level 6 NFTs
            Polymorphy,
            Lycanthrope,
            HeroicTrap,                     // Level 7 NFTs
            CelestialMirror,
            MoonCalendar,

            //FaithMasterSpells

            HammerOfJustice = 3000,              // Cantrip             // 0
            Pray,                  // Level 1                  // 0
            ProtectionSphere,                                           // 0
            Polytheism,
            BlindingLight,                  // Level 2
            IcyShield,
            ArmorOfTheBeast,
            Ray,                            // Level 3
            BloodStorm,
            MeteorShower,
            Purify,                         // Level 4
            Sacrifice,
            Wall,

            ShootingStar,                   // Level 5 NFTs
            Plague,
            Deluge,
            TheInnateLaw,                   // Level 6 NFTs
            Revenge,
            Torment,
            InvertedPossesion,              // Level 7 NFTs
            FireOfTheDamned,
            SwordOfDamocles,

            //MagicMasterSpells

            ArcaneAbyss = 4000,             // Cantrip                  // 0
            Bubble,                         // Level 1                  // 0
            BallOfFire,                                                 // 0
            Levitation,                                                 // 0
            Alchemy,                        // Level 2
            Telekinesis,
            OblivionFog,
            Frost,                          // Level 3
            Desecration,
            Supernova,
            Zombie,                         // Level 4
            Earthquake,
            Eco,

            WhisperOfTheMoon,               // Level 5 NFTs
            Drought,
            Gravity,
            Insomnia,                       // Level 6 NFTs
            Mirage,
            Hex,
            Ashes,                          // Level 7 NFTs
            LastBreath,
            ShinigamiEye,

            //ArtsMasterSpells

            StrongHeart = 5000,             // Cantrip                  // 0 
            CallToArms,                     // Level 1                  // 0
            Masquerade,                                                 // 0
            Lullaby,                                                    // 0
            Mentalism,                      // Level 2
            Juggle,
            Train,
            Epopeya,                           // Level 3
            Willpower,
            Leadership,
            Seduction,                      // Level 4
            WindDance,
            Revolt,

            Hijack,                         // Level 5 NFTs
            Psychedelia,
            Diplomacy,
            SweetIntroductionToChaos,       // Level 6 NFTs
            Polymathia,
            BloodTie,
            VitalTrade,                     // Level 7 NFTs
            TheApothecaryOfVerona,
            Corruption
        }

        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract SpellClassType SpellType { get; }

        protected bool isSpellCondition = false;
        public bool IsSpellCondition { get { return isSpellCondition; } }

        protected virtual TargetType PlayerTargetType => TargetType.OldMethod;

        public Spell()
        {
            GameEvents.SetSpellCondition += SetSpellCondition;

            SetSpellCondition();
        }

        ~Spell()
        {
            GameEvents.SetSpellCondition -= SetSpellCondition;
        }

        protected virtual void SetSpellCondition()
        {
            if (isSpellCondition)
            {
                if (PlayerTargetType != TargetType.OldMethod)
                {
                    isSpellCondition &= PlayerTargetType == TargetType.LocalPlayer ? !Memory.LocalHeroe.RoundSpells.ContainsKey(SpellType) : !Memory.RemoteHeroe.RoundSpells.ContainsKey(SpellType);
                }
            }

            GameEvents.OnSetSpellCondition?.Invoke();
        }

        public IEnumerator RunSpell(int targetActorNumber, string responseData, string tableCards)
        {
            UnityEngine.Debug.Log(System.DateTime.UtcNow.ToString() + " | " + "RunSpell");

            yield return ApplySpell(targetActorNumber, responseData, tableCards);
        }

        protected virtual IEnumerator ApplySpell(int targetActorNumber, string responseData, string sTableCards)
        {
            if (PlayerTargetType != TargetType.OldMethod)
            {
                GameEvents.OnAddRoundSpellAbility?.Invoke(SpellType,
                  MirrorAppClient.Instance.LocalActorNumber == Memory.CurrentActorNumberTurn ?
                  HeroeLocalRemoteType.Local : HeroeLocalRemoteType.Remote);

                if (MirrorAppClient.Instance.LocalActorNumber == targetActorNumber)
                    Memory.LocalHeroe.RoundSpells.Add(SpellType, responseData);
                else
                    Memory.RemoteHeroe.RoundSpells.Add(SpellType, responseData);
            }


            OnApplySpellDone();

            yield break;
        }

        public virtual void RemoveSpell(int targetActorNumber)
        {

        }

        protected void OnApplySpellDone()
        {
            if (MirrorAppClient.Instance.LocalActorNumber == Memory.CurrentActorNumberTurn)
            {
                FSM.Instance.SpellBookFSM.ChangeToNone();

                GameEvents.SetAbilityCondition?.Invoke();
                GameEvents.SetSpellCondition?.Invoke();
                GameEvents.SetHeroeAbilityCondition?.Invoke();
            }

            FSM.Instance.SpellBookFSM.ChangeToEndTurn(false.ToString());
        }

        public virtual void DefineRequest(PowerData powerData)
        {
            OnSpellRequestDefined(powerData);
        }

        protected void OnSpellRequestDefined(PowerData powerData)
        {
            FSM.Instance.SpellBookFSM.ChangeToApplySpell(JsonConvert.SerializeObject(powerData));
        }

        protected IEnumerator ShowTitlePopupMessage(string message)
        {
            yield return GameEvents.OnCandyAbilitySuccess?.Invoke(message);
        }
    }
}
