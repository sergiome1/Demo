using System;
using System.Collections.Generic;

namespace Assets.Scripts.GameDatas
{
    [Serializable]
    public class PotBetsData
    {
        public int MINUTES_BLIND_UPDATE;
        public int BLIND_STEP;
        public int INITIAL_SOULS;

        public int bigBlind { get; set; }
        public int smallBlind { get; set; }

        public int pot { get; set; }

        public List<int> actorNumbers { get; set; }

        public int amountBetsPlayer1 { get; set; }
        public int amountBetsPlayer2 { get; set; }
        public int amountSoulsPlayer1 { get; set; }
        public int amountSoulsPlayer2 { get; set; }

        public int sourceActorNumber { get; set; }
        public int amount { get; set; }
        public DateTime targetTime { get; set; }
    }
}
