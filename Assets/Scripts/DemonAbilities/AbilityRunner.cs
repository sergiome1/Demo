using static Assets.Scripts.DemonAbilities.Ability;
using UnityEngine;
using Assets.Scripts.Managers;
using Assets.Scripts.Coroutines;
using static Assets.Scripts.Heroes.Heroe;
using static Assets.Scripts.Spells.Spell;
using Assets.Scripts.HeroAbilities;
using Assets.Scripts.Spells;

namespace Assets.Scripts.DemonAbilities
{
    public class AbilityRunner : MonoBehaviour
    {
        private Ability ability = null;
        private HeroAbility heroeAbility = null;
        private Spell spell = null;

        private static AbilityRunner _instance;

        public static AbilityRunner Instance { get { return _instance; } }

        private void Awake()
        {
            _instance = this;
        }

        public void RunDemonAbility(AbilityClassType abilityClassType, int targetActorNumber, string data, string tableCards)
        {
            ability = DemonMenuAbilityManager.Instance.GetAbility(abilityClassType);

            CoroutineManager.Instance.RunCoroutine(ability.RunAbility(targetActorNumber, data, tableCards));
        }

        public void RunHeroeAbility(int targetActorNumber, string data, HeroeType heroeType, string tableCards)
        {
            heroeAbility = HeroeAbilityManager.Instance.GetHeroeAbility(heroeType);

            CoroutineManager.Instance.RunCoroutine(heroeAbility.RunHeroeAbility(targetActorNumber, data, heroeType, tableCards));
        }

        public void RunSpell(int targetActorNumber, string data, SpellClassType spellClassType, string tableCards)
        {
            spell = SpellBookManager.Instance.GetSpell(spellClassType);

            CoroutineManager.Instance.RunCoroutine(spell.RunSpell(targetActorNumber, data, tableCards));
        }

        public void ClearSpell(int targetActorNumber, SpellClassType spellClassType)
        {
            spell = SpellBookManager.Instance.GetSpell(spellClassType);

            spell.RemoveSpell(targetActorNumber);
        }
    }
}
