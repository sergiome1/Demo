using Assets.MirrorApp;
using Assets.Scripts.GameDatas;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Pokers
{
    public class PotBets : MonoBehaviour
    {
        private PotBetsData potBetsData = null;

        public PotBetsData PotBetsData { get { return potBetsData; } }

        public int LocalAmountSouls
        {
            get
            {
                if (potBetsData == null) return 0;
                return potBetsData.actorNumbers[0] == MirrorAppClient.Instance.LocalActorNumber ? potBetsData.amountSoulsPlayer1 : potBetsData.amountSoulsPlayer2;
            }
        }
        public int RemoteAmountSouls { get { return potBetsData.actorNumbers[0] == MirrorAppClient.Instance.LocalActorNumber ? potBetsData.amountSoulsPlayer2 : potBetsData.amountSoulsPlayer1; } }
        public int LocalAmountBets { get { return potBetsData.actorNumbers[0] == MirrorAppClient.Instance.LocalActorNumber ? potBetsData.amountBetsPlayer1 : potBetsData.amountBetsPlayer2; } }
        public int RemoteAmountBets { get { return potBetsData.actorNumbers[0] == MirrorAppClient.Instance.LocalActorNumber ? potBetsData.amountBetsPlayer2 : potBetsData.amountBetsPlayer1; } }
        public int TotalGems { get { return LocalAmountSouls + RemoteAmountSouls + LocalAmountBets + RemoteAmountBets + potBetsData.pot; } }

        public int BigBlind { get { return potBetsData.bigBlind; } }
        public int SmallBlind { get { return potBetsData.smallBlind; } }

        public bool IsInitialized { get { return potBetsData != null; } }

        public int InitialSouls { get { return potBetsData.INITIAL_SOULS; } }


        private static PotBets _instance;

        public static PotBets Instance
        {
            get
            {
                return _instance;
            }
        }

        protected void Awake()
        {
            _instance = this;

            potBetsData = null;
        }

        public void UpdateBlinds(int smallBlind, int bigBlind)
        {
            potBetsData.smallBlind = smallBlind;
            potBetsData.bigBlind = bigBlind;

            GameEvents.OnBlindUpdate?.Invoke(smallBlind, bigBlind);
        }

        public void SetPotBets(PotBetsData potBetsData)
        {
            this.potBetsData = potBetsData;

            if (potBetsData.actorNumbers[0] == MirrorAppClient.Instance.LocalActorNumber)
            {
                SetPotBetsInternal(MirrorAppClient.Instance.LocalActorNumber, potBetsData.amountBetsPlayer1, potBetsData.amountSoulsPlayer1,
                    MirrorAppClient.Instance.RemoteActorNumber, potBetsData.amountBetsPlayer2, potBetsData.amountSoulsPlayer2);
            }
            else
            {
                SetPotBetsInternal(MirrorAppClient.Instance.LocalActorNumber, potBetsData.amountBetsPlayer2, potBetsData.amountSoulsPlayer2,
                    MirrorAppClient.Instance.RemoteActorNumber, potBetsData.amountBetsPlayer1, potBetsData.amountSoulsPlayer1);
            }
        }

        private void SetPotBetsInternal(int localActorNumber, int localAmountBet, int localAmountSouls, int remoteActorNumber, int remoteAmountBet, int remoteAmountSouls)
        {
            GameEvents.OnUIUpdateSouls?.Invoke(localActorNumber, localAmountSouls);
            GameEvents.OnUIUpdateSouls?.Invoke(remoteActorNumber, remoteAmountSouls);

            GameEvents.OnUIUpdateAmountBet?.Invoke(new object[] { localActorNumber, localAmountBet });
            GameEvents.OnUIUpdateAmountBet?.Invoke(new object[] { remoteActorNumber, remoteAmountBet });
        }
    }
}
