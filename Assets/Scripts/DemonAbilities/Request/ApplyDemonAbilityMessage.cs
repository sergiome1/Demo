using Assets.Scripts.HTTPs;

namespace Assets.Scripts.DemonAbilities.Request
{
    public struct ApplyDemonAbilityMessage : NetworkMessage
    {
        public string applyAbilityRequest;
    }
}
