using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.PopUps
{
    public class UIPopUpAcceptCancel : MonoBehaviour
    {
        [SerializeField]
        private RectTransform rectTransform = null;
        [SerializeField]
        private TMP_Text text = null;
        [SerializeField]
        private Button acceptButton = null;
        [SerializeField]
        private Button cancelButton = null;

        private Action OnAccept = null;
        private Action OnCancel = null;

        private bool isReady = false;

        public bool IsActive { get { return rectTransform.gameObject.activeSelf; } }


        public Tween Show(string message, Action accept, Action cancel)
        {
            text.text = message;

            OnAccept = accept;
            OnCancel = cancel;

            rectTransform.gameObject.SetActive(true);

            return rectTransform.DOScale(1, 0.25f).SetEase(Ease.InOutCubic).OnComplete(() =>
            {
                isReady = true;
            });

        }

        public Tween Hide()
        {
            text.text = string.Empty;

            OnAccept = null;
            OnCancel = null;

            return rectTransform.DOScale(0, 0.25f).SetEase(Ease.InOutCubic).OnComplete(() =>
            {
                rectTransform.gameObject.SetActive(false);
            });
        }

        public void Accept()
        {
            if (isReady)
            {
                isReady = false;

                OnAccept?.Invoke();
            }
        }

        public void Cancel()
        {
            if (isReady)
            {
                isReady = false;

                OnCancel?.Invoke();
            }
        }
    }
}
