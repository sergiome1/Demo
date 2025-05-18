using Assets.MirrorApp;
using Assets.Scripts.GameDatas;
using static Assets.Scripts.Deities.Deity;

namespace Assets.Scripts.HTTPs.Requests
{
    public class ApplyDeityAbilityRequest
    {
        public string roomId { get; set; }
        public int sourceActorNumber { get; set; }
        public int targetActorNumber { get; set; }
        public DeityClassType deityType { get; set; }
        public int turnStep { get; set; }
        public PowerData powerData { get; set; }

        public ApplyDeityAbilityRequest(int sourceActorNumber, int targetActorNumber, DeityClassType deityType, PowerData powerData)
        {
            roomId = MirrorAppClient.Instance.CurrentRoom;
            this.sourceActorNumber = sourceActorNumber;
            this.targetActorNumber = targetActorNumber;
            this.deityType = deityType;
            this.powerData = powerData;
        }
    }
}
