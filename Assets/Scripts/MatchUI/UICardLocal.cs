using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MatchUI
{
    public class UICardLocal : UIDeckCard
    {
        private const float speedMouseEnterExit = 0.0625f;

        [SerializeField]
        private UIAbilityDescriptionsCard uIAbilityDescriptionsCard = null;
        [SerializeField]
        private Transform targetTransformOnMouseEnter = null;

        private bool isShowDemon = false;

        public bool IsShowDemon { get { return isShowDemon; } }

        public IEnumerator OnDemonMenuAbilityEnter()
        {
            yield return OnShowDemonEnter();

            isMouseOverAura.SetActive(true);

            for (int i = 0; i < uIAbilities.Count; i++)
            {
                uIAbilities[i].SetDemonAbility();
            }

            yield return uIAbilityDescriptionsCard.Show();
        }

        public IEnumerator OnShowDemonEnter()
        {
            isShowDemon = true;
            transform.DOMove(targetTransformOnMouseEnter.position, speedMouseEnterExit).SetEase(Ease.InOutCubic);
            yield return transform.DOScale(targetTransformOnMouseEnter.localScale, speedMouseEnterExit).SetEase(Ease.InOutCubic).WaitForCompletion();
        }
    }
}
