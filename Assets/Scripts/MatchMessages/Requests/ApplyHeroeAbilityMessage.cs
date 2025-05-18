using Assets.Scripts.HTTPs;

namespace Assets.Scripts.MatchMessages.Requests
{
    public struct ApplyHeroeAbilityMessage : NetworkMessage
    {
        public string roomId;
        public int sourceActorNumber;
        public int targetActorNumber;
        public int heroeType;
        public int turnStep;
        public string powerData;
    }
}
