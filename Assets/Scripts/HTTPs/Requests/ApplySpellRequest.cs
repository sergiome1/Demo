using Assets.MirrorApp;

namespace Assets.Scripts.HTTPs.Requests
{
    public class ApplySpellRequest
    {
        public string roomId { get; set; }
        public int sourceActorNumber { get; set; }
        public int targetActorNumber { get; set; }
        public int turnStep { get; set; }
        public string powerData { get; set; }

        public ApplySpellRequest(int sourceActorNumber, int targetActorNumber, string powerData)
        {
            roomId = MirrorAppClient.Instance.CurrentRoom;
            this.sourceActorNumber = sourceActorNumber;
            this.targetActorNumber = targetActorNumber;
            this.powerData = powerData;
        }
    }
}
