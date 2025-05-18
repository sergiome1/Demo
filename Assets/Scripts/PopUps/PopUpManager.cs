using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.PopUps
{
    public class PopUpManager : MonoBehaviour
    {
        public enum PopUpType { Info, AcceptCancel }

        [SerializeField]
        private Image transparent = null;
        [SerializeField]
        private UIPopUpInfo uIPopUpInfo = null;
        [SerializeField]
        private UIPopUpAcceptCancel uIPopUpAcceptCancel = null;

        private bool isShow = false;

        public bool IsShow { get { return isShow; } }

        private static PopUpManager _instance;

        public static PopUpManager Instance
        {
            get
            {
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;

            DontDestroyOnLoad(gameObject);
        }

        public IEnumerator Show(PopUpType popUpType, string message, Action callback, Action accept = null, Action cancel = null)
        {
            isShow = true;

            transparent.gameObject.SetActive(true);
            transparent.DOFade(0.85f, 0.125f).SetEase(Ease.InOutCubic);

            Tween useTween = null;
            switch (popUpType)
            {
                case PopUpType.Info:
                    useTween = uIPopUpInfo.Show(message, accept);
                    break;
                case PopUpType.AcceptCancel:
                    useTween = uIPopUpAcceptCancel.Show(message, accept, cancel);
                    break;
            }

            yield return useTween.WaitForCompletion();

            callback?.Invoke();
        }

        public IEnumerator Hide(PopUpType popUpType)
        {
            switch (popUpType)
            {
                case PopUpType.Info:
                    if (!uIPopUpInfo.IsActive)
                    {
                        yield break;
                    }
                    break;
                case PopUpType.AcceptCancel:
                    if (!uIPopUpAcceptCancel.IsActive)
                    {
                        yield break;
                    }
                    break;
            }

            if (isShow)
            {
                isShow = false;

                transparent.DOFade(0, 0.25f).SetEase(Ease.InOutCubic);
                Tween useTween = null;
                switch (popUpType)
                {
                    case PopUpType.Info:
                        useTween = uIPopUpInfo.Hide();
                        break;
                    case PopUpType.AcceptCancel:
                        useTween = uIPopUpAcceptCancel.Hide();
                        break;
                }

                yield return useTween.WaitForCompletion();

                transparent.gameObject.SetActive(false);
            }
        }
    }
}
