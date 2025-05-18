using UnityEngine;

namespace Assets.Scripts.FSMs
{
    [RequireComponent(typeof(DemonAbilityFSM))]
    [RequireComponent(typeof(HeroeAbilityTurnFSM))]
    [RequireComponent(typeof(DeityTurnFSM))]
    [RequireComponent(typeof(SpellBookFSM))]
    [RequireComponent(typeof(HeroeUIActionsFSM))]
    public class FSM : MonoBehaviour
    {
        private DemonAbilityFSM demonMenuAbilityFSM = null;
        private HeroeAbilityTurnFSM heroeAbilityTurnFSM = null;
        private DeityTurnFSM deityTurnFSM = null;
        private SpellBookFSM spellBookFSM = null;
        private HeroeUIActionsFSM heroeUIActionsFSM = null;

        public DemonAbilityFSM DemonMenuAbilityFSM { get { return demonMenuAbilityFSM; } }
        public HeroeAbilityTurnFSM HeroeAbilityTurnFSM { get { return heroeAbilityTurnFSM; } }
        public DeityTurnFSM DeityTurnFSM { get { return deityTurnFSM; } }
        public SpellBookFSM SpellBookFSM { get { return spellBookFSM; } }
        public HeroeUIActionsFSM HeroeUIActionsFSM { get { return heroeUIActionsFSM; } }


        private static FSM _instance;

        public static FSM Instance { get { return _instance; } }

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;

            SetFSMs();

            DontDestroyOnLoad(gameObject);
        }

        private void SetFSMs()
        {
            demonMenuAbilityFSM = FindObjectOfType<DemonAbilityFSM>();
        }
    }
}

