
using Assets.Scripts.DemonAbilities.Request;
using Assets.Scripts.Managers;

namespace Assets.Scripts.GameProcessors.DemonAbilities.GameProcessors
{
    public class OnApplyAbility : GameProcessor
    {
        public override void Process(string data)
        {
            HTTPManager.Instance.HellmasterData(new ApplyDemonAbilityMessage()
            {
                applyAbilityRequest = data,
            });
        }
    }
}
