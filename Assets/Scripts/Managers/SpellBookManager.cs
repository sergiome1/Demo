using Assets.Scripts.FSMs;
using Assets.Scripts.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Assets.Scripts.GameDatas.NFTData;
using static Assets.Scripts.Spells.Spell;
using static UnityEditor.AddressableAssets.Build.Layout.BuildLayout;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEditor.Localization.Plugins.XLIFF.V20;
using UnityEngine.UI;
using UnityEngine;
using Assets.Scripts.Coroutines;
using Assets.Scripts.SpellBooks.GameProcessorSpellBooks;

namespace Assets.Scripts.Managers
{
    public class SpellBookManager : EventManager
    {
        private static SpellBookManager _instance;

        public static SpellBookManager Instance
        {
            get
            {
                return _instance;
            }
        }

        protected override void Awake()
        {
            _instance = this;

            base.Awake();
        }

        protected override void AddEvents()
        {
            FSM.Instance.SpellBookFSM.OnStartTurn += OnStartSpellBookTurn;
            FSM.Instance.SpellBookFSM.OnApplySpell += OnApplySpell;
            FSM.Instance.SpellBookFSM.OnEndTurn += OnEndSpellTurn;
        }

        protected override void RemoveEvents()
        {
            FSM.Instance.SpellBookFSM.OnStartTurn -= OnStartSpellBookTurn;
            FSM.Instance.SpellBookFSM.OnApplySpell -= OnApplySpell;
            FSM.Instance.SpellBookFSM.OnEndTurn -= OnEndSpellTurn;
        }

        public Spell GetSpell(SpellClassType spellClassType)
        {
            Spell spell = null;

            switch (spellClassType)
            {
                // *** NOTE: Commented on Demo purposes ***
            }

            return spell;
        }

        private void OnStartSpellBookTurn(string data)
        {
            OnStartSpellBookTurn spellBookTurn = new OnStartSpellBookTurn();
            CoroutineManager.Instance.RunCoroutine(spellBookTurn.ProcessAsync(string.Empty));
        }

        private void OnApplySpell(string data)
        {
            OnApplySpell onApplySpell = new OnApplySpell();
            onApplySpell.Process(data);
        }

        private void OnEndSpellTurn(string data)
        {
            OnEndSpellTurn onEndSpellTurn = new OnEndSpellTurn();
            onEndSpellTurn.Process(data);
        }
    }
}
