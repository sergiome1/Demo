using Assets.Scripts.FSMs;
using Assets.Scripts.GameProcessors.DemonAbilities.GameProcessors;
using Assets.Scripts.HeroAbilities;
using Assets.Scripts.HeroAbilities.GameProcessorHeroAbilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Assets.Scripts.Heroes.Heroe;

namespace Assets.Scripts.Managers
{
    public class HeroeAbilityManager : EventManager
    {
        private List<HeroAbility> heroAbilites = null;

        private static HeroeAbilityManager _instance;

        public static HeroeAbilityManager Instance
        {
            get
            {
                return _instance;
            }
        }

        protected override void Awake()
        {
            heroAbilites = new List<HeroAbility>();

            _instance = this;

            base.Awake();
        }

        public HeroAbility GetHeroeAbility(HeroeType heroeType)
        {
            HeroAbility heroeAbility = null;

            // *** NOTE: Commented on Demo purposes ***

            //    switch (heroeType)
            //    {
            //        case HeroeType.Hector:
            //            heroeAbility = new HectorHeroeAbility();
            //            break;
            //        case HeroeType.Tiresias:
            //            heroeAbility = new TiresiasHeroeAbility();
            //            break;
            //        case HeroeType.Herakles:
            //            heroeAbility = new HeraklesHeroeAbility();
            //            break;
            //        case HeroeType.Odysseus:
            //            heroeAbility = new OdysseusHeroeAbility();
            //            break;
            //        case HeroeType.Theseus:
            //            heroeAbility = new TheseusHeroeAbility();
            //            break;
            //        case HeroeType.Achilles:
            //            heroeAbility = new AchillesHeroeAbility();
            //            break;
            //        case HeroeType.Psyche:
            //            heroeAbility = new PsycheHeroeAbility();
            //            break;
            //        case HeroeType.Eneas:
            //            heroeAbility = new EneasHeroeAbility();
            //            break;
            //        case HeroeType.Orpheus:
            //            heroeAbility = new OrpheusHeroeAbility();
            //            break;
            //        case HeroeType.Atalanta:
            //            heroeAbility = new AtalantaHeroeAbility();
            //            break;
            //        case HeroeType.Perseus:
            //            heroeAbility = new PerseusHeroeAbility();
            //            break;
            //        case HeroeType.Helena:
            //            heroeAbility = new HelenaHeroeAbility();
            //            break;
            //        case HeroeType.Leonidas:
            //            heroeAbility = new LeonidasHeroeAbility();
            //            break;
            //        case HeroeType.Circe:
            //            heroeAbility = new CirceHeroeAbility();
            //            break;
            //        case HeroeType.Jason:
            //            heroeAbility = new JasonHeroeAbility();
            //            break;
            //    }

            heroAbilites.Add(heroeAbility);

            return heroeAbility;
        }

        protected override void AddEvents()
        {
            FSM.Instance.HeroeAbilityTurnFSM.OnStartTurn += OnStartTurn;
            FSM.Instance.HeroeAbilityTurnFSM.OnApplyHeroeAbility += OnApplyHeroeAbility;
            FSM.Instance.HeroeAbilityTurnFSM.OnEndTurn += OnEndTurn;
        }

        protected override void RemoveEvents()
        {
            FSM.Instance.HeroeAbilityTurnFSM.OnStartTurn -= OnStartTurn;
            FSM.Instance.HeroeAbilityTurnFSM.OnApplyHeroeAbility -= OnApplyHeroeAbility;
            FSM.Instance.HeroeAbilityTurnFSM.OnEndTurn -= OnEndTurn;
        }

        private IEnumerator OnStartTurn(string data)
        {
            OnStartTurn turn = new OnStartTurn();
            yield return turn.ProcessAsync(data);
        }

        private void OnApplyHeroeAbility(string data)
        {
            OnApplyHeroeAbility turn = new OnApplyHeroeAbility();
            turn.Process(data);
        }

        private IEnumerator OnEndTurn(string data)
        {
            OnEndTurn turn = new OnEndTurn();
            yield return turn.ProcessAsync(data);
        }
        private void OnNone(string data)
        {
            for (int i = 0; i < heroAbilites.Count; i++)
            {
                heroAbilites[i].Clean();
            }

            heroAbilites.Clear();
        }
    }
}
