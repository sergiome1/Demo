using Assets.MirrorApp;
using Assets.Scripts.GameDatas;

namespace Assets.Scripts.HTTPs.Requests
{
    public class ApplyHeroeAbilityRequest
    {
        public string roomId { get; set; }
        public int sourceActorNumber { get; set; }
        public int targetActorNumber { get; set; }
        public int heroeType { get; set; }
        public int turnStep { get; set; }
        public PowerData powerData { get; set; }

        public ApplyHeroeAbilityRequest(int sourceActorNumber, int targetActorNumber, int heroeType, PowerData powerData)
        {
            roomId = MirrorAppClient.Instance.CurrentRoom;
            this.sourceActorNumber = sourceActorNumber;
            this.targetActorNumber = targetActorNumber;
            this.heroeType = heroeType;
            this.powerData = powerData;
        }
    }
}
