using Assets.Scripts.HTTPs;

namespace Assets.Scripts.MatchMessages.Requests
{
    public struct ApplySpellMessage : NetworkMessage
    {
        public string applySpellRequest;
    }
}
