
namespace Assets.Scripts.HTTPs.Responses
{
    public class ApplyPowerResponse
    {
        public enum ResponseType { NoPower, Success, IsCounter, IsFirecracker, IsCamouflage, IsShadow }
        public int sourceActorNumber { get; set; }
        public int targetActorNumber { get; set; }
        public int indexClassType { get; set; }
        public string data { get; set; }
        public string tableCards { get; set; }
        public ResponseType responseType { get; set; }
        public int power { get; set; }
        public int manaSpent { get; set; }
        public bool isManaRegenerated { get; set; }
    }
}
