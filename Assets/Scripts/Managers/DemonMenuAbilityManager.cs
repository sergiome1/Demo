using Assets.Scripts.Coroutines;
using Assets.Scripts.DemonAbilities;
using Assets.Scripts.DemonAbilities.Implementations;
using Assets.Scripts.FSMs;
using Assets.Scripts.GameProcessors.DemonAbilities.GameProcessors;
using static Assets.Scripts.DemonAbilities.Ability;

namespace Assets.Scripts.Managers
{
    public class DemonMenuAbilityManager : EventManager
    {

        private static DemonMenuAbilityManager _instance;

        public static DemonMenuAbilityManager Instance
        {
            get
            {
                return _instance;
            }
        }

        protected override void Awake()
        {
            _instance = this;

            base.Awake();
        }

        protected override void AddEvents()
        {
            FSM.Instance.DemonMenuAbilityFSM.OnStartTurn += OnStartTurn;
            FSM.Instance.DemonMenuAbilityFSM.OnApplyAbility += OnApplyAbility;
            FSM.Instance.DemonMenuAbilityFSM.OnEndTurn += OnEndTurn;
        }

        protected override void RemoveEvents()
        {
            FSM.Instance.DemonMenuAbilityFSM.OnStartTurn -= OnStartTurn;
            FSM.Instance.DemonMenuAbilityFSM.OnApplyAbility -= OnApplyAbility;
            FSM.Instance.DemonMenuAbilityFSM.OnEndTurn -= OnEndTurn;
        }

        public Ability GetAbility(AbilityClassType abilityType)
        {
            Ability ability = null;

            switch (abilityType)
            {
                case AbilityClassType.Armageddon:
                    ability = new Armageddon();
                    break;
                case AbilityClassType.HandSwapper:
                    ability = new HandSwapper();
                    break;

                    //NOTE: For this demo purpuse I just add two of them
            }

            return ability;
        }

        private void OnStartTurn(string data)
        {
            OnStartTurn turn = new OnStartTurn();
            CoroutineManager.Instance.RunCoroutine(turn.ProcessAsync(data));
        }

        private void OnApplyAbility(string data)
        {
            OnApplyAbility turn = new OnApplyAbility();
            turn.Process(data);
        }

        private void OnEndTurn(string data)
        {
            OnEndTurn turn = new OnEndTurn();
            CoroutineManager.Instance.RunCoroutine(turn.ProcessAsync(data));
        }
    }
}
