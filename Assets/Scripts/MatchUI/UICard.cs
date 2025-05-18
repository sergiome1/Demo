
using Assets.Scripts.GameplayElements;
using UnityEngine;

namespace Assets.Scripts.MatchUI
{
    public abstract class UICard : MonoBehaviour
    {
        [SerializeField]
        protected GameObject isMouseOverAura = null;

        private int idCard = -1;
        public int IdCard { get { return idCard; } }

        private Card card = null;
        public Card Card { get { return card; } }
    }
}
