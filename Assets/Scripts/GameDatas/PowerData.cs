using System;

namespace Assets.Scripts.GameDatas
{
    [Serializable]
    public struct PowerData
    {
        public int indexClassType { get; set; }
        public int targetActorNumber { get; set; }
        public string extraData { get; set; }
        public int idCard { get; set; }
    }
}
