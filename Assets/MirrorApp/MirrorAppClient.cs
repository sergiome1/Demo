using UnityEngine;

namespace Assets.MirrorApp
{
    public partial class MirrorAppClient: MonoBehaviour
    {
        // *** NOTE: This class has been reduced for Demo purposes ***

        private int localActorNumber = -1;
        public int LocalActorNumber { get { return localActorNumber; } }

        private int remoteActorNumber = -1;
        public int RemoteActorNumber { get { return remoteActorNumber; } }

        private string sCurrentRoom = string.Empty;
        public string CurrentRoom { get { return sCurrentRoom; } }

        private static MirrorAppClient _instance;

        public static MirrorAppClient Instance { get { return _instance; } }

        public void Initialize()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
        }
    }
}
