using Assets.Scripts.HTTPs;
using System;
using static Assets.Scripts.HTTPs.NetworkMessage;

namespace Assets.Scripts.MatchMessages.Responses
{
    [Serializable]
    public struct GetPotBetResult : NetworkMessage
    {
        public int actorNumber;
        public int betType;
        public string potBetsData;
        public MessageStatus status;
    }
}
