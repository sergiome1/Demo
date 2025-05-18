
using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.MatchUI
{
    public class UIAbility : EventManager, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private GameObject frameSelectable = null;

        [SerializeField]
        private Image image = null;

        [SerializeField]
        private UIPowerDescription uIAbilityDescription = null;
        public UIPowerDescription UIPowerDescription { get { return uIAbilityDescription; } }

        private string abilityName = string.Empty;
        private string description = string.Empty;

        protected override void Awake() { }

        public void Initialize()
        {
            frameSelectable.SetActive(false);

            base.Awake();
        }

        protected override void AddEvents()
        {
            /*
             * ...
             * 
             */
        }

        protected override void RemoveEvents()
        {
            /*
             * ...
             * 
             */
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            /*
             * ...
             * 
             */
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            /*
             * ...
             * 
             */
        }

        public void SetDemonAbility()
        {
            uIAbilityDescription.SetDescription(image.sprite, abilityName, description);
        }
    }
}
