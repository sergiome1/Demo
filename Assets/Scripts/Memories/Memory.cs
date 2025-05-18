
using Assets.Scripts.Heroes;

namespace Assets.Scripts.Memories
{
    public static class Memory
    {

        private static Heroe remoteHeroe = null;
        public static Heroe RemoteHeroe { get { return remoteHeroe; } }

        private static Heroe localHeroe = null;
        public static Heroe LocalHeroe { get { return localHeroe; } }

        private static int currentActorNumberTurn = -1;

        public static int CurrentActorNumberTurn { get { return currentActorNumberTurn; } }
    }
}
