using Assets.Scripts.Coroutines;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MatchUI
{
    public class UIAbilityDescriptionsCard : MonoBehaviour
    {
        [SerializeField]
        private UIAbility[] uIAbilities = null;

        private void Awake()
        {
            for (int i = 0; i < uIAbilities.Length; i++)
            {
                uIAbilities[i].Initialize();
            }
            Hide();
        }

        public IEnumerator Show()
        {
            gameObject.SetActive(true);

            for (int i = 0; i < uIAbilities.Length; i++)
            {
                if (i < uIAbilities.Length - 1)
                {
                    CoroutineManager.Instance.RunCoroutine(uIAbilities[i].UIPowerDescription.Show());
                }
                else
                {
                    yield return uIAbilities[i].UIPowerDescription.Show();
                }
            }
        }

        public IEnumerator Hide()
        {
            for (int i = 0; i < uIAbilities.Length; i++)
            {
                if (i < uIAbilities.Length - 1)
                {
                    CoroutineManager.Instance.RunCoroutine(uIAbilities[i].UIPowerDescription.Hide());
                }
                else
                {
                    yield return uIAbilities[i].UIPowerDescription.Hide();
                }
            }

            gameObject.SetActive(false);
        }
    }
}
