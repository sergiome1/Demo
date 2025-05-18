using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MatchUI
{
    public class UIPowerDescription : MonoBehaviour
    {
        private const float speedFace = 0.0625f;

        [SerializeField]
        private Button button = null;
        [SerializeField]
        private Image image = null;
        [SerializeField]
        private TMP_Text manaCost = null;
        [SerializeField]
        private TMP_Text abilityName = null;
        [SerializeField]
        private TMP_Text description = null;

        private Vector3 initialScale = Vector3.zero;
        private Vector3 buttonInitialScale = Vector3.zero;

        private bool isVisible = false;
        public bool IsVisible { get { return isVisible; } }


        private void Awake()
        {
            initialScale = transform.localScale;
            buttonInitialScale = button.transform.localScale;

            transform.DOScale(Vector3.zero, 0).SetEase(Ease.InOutCubic);
            button.transform.DOScale(Vector3.zero, 0).SetEase(Ease.InOutCubic);

            gameObject.SetActive(false);
        }

        public void SetDescription(Sprite sprite, string abilityName, string description)
        {
            if (sprite != null)
            {
                image.sprite = sprite;
            }

            this.abilityName.text = abilityName;
            this.description.text = description;
        }

        public IEnumerator Show()
        {
            isVisible = true;
            gameObject.SetActive(isVisible);

            button.transform.DOScale(buttonInitialScale, speedFace).SetEase(Ease.InOutCubic).OnUpdate(() =>
            {
                abilityName.ForceMeshUpdate();
                description.ForceMeshUpdate();
                manaCost.ForceMeshUpdate();
            });

            yield return transform.DOScale(initialScale, speedFace).SetEase(Ease.InOutCubic).WaitForCompletion();
        }

        public IEnumerator Hide()
        {
            button.transform.DOScale(Vector3.zero, speedFace).SetEase(Ease.InOutCubic);
            yield return transform.DOScale(Vector3.zero, speedFace).SetEase(Ease.InOutCubic).WaitForCompletion();

            isVisible = false;
            gameObject.SetActive(isVisible);
        }
    }
}
