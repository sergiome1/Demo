using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.PopUps
{
    public class UIPopUpInfo : MonoBehaviour
    {
        [SerializeField]
        private RectTransform rectTransform = null;
        [SerializeField]
        private TMP_Text text = null;
        [SerializeField]
        private Button acceptButton = null;

        private Action OnAccept = null;

        public bool IsActive { get { return rectTransform.gameObject.activeSelf; } }

        public Tween Show(string message, Action accept)
        {
            OnAccept = accept;

            text.text = message;

            rectTransform.gameObject.SetActive(true);

            return rectTransform.DOScale(1, 0.25f).SetEase(Ease.InOutCubic);
        }

        public Tween Hide()
        {
            text.text = string.Empty;

            OnAccept = null;

            return rectTransform.DOScale(0, 0.25f).SetEase(Ease.InOutCubic).OnComplete(() =>
            {
                rectTransform.gameObject.SetActive(false);
            });
        }

        public void Accept()
        {
            OnAccept?.Invoke();
        }
    }
}
