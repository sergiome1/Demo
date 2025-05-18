using Assets.Scripts.HTTPs;

namespace Assets.Scripts.MatchMessages.Requests
{
    public struct GetPotBetMessage : NetworkMessage
    {
        public string roomId;
    }
}
