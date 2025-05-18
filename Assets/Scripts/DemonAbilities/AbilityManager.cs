using Assets.Scripts.Coroutines;
using UnityEngine;
using Assets.Scripts.DemonAbilities.Implementations;
using static Assets.Scripts.DemonAbilities.Ability;
using Assets.Scripts.Managers;

namespace Assets.Scripts.DemonAbilities
{
    public class AbilityManager : MonoBehaviour
    {
        public void RunDemonAbility(AbilityClassType abilityClassType, int targetActorNumber, string data, string tableCards)
        {
            Ability ability = DemonMenuAbilityManager.Instance.GetAbility(abilityClassType);

            CoroutineManager.Instance.RunCoroutine(ability.RunAbility(targetActorNumber, data, tableCards));
        }
    }
}
