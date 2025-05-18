using Assets.Scripts.Coroutines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using UnityEngine.Localization;
using UnityEngine.SceneManagement;
using UnityEngine;
using Assets.Scripts.PopUps;

namespace Assets.Scripts.Managers
{
    public class LocalizationManager : MonoBehaviour
    {
        public const string HeroeClass = "heroeClass";
        public const string RegisterButton = "RegisterButton";
        public const string sumPlayers = "sumPlayers";
        public const string totalPlayers = "totalPlayers";
        public const string SelectRemove = "selectRemove";
        public const string Select = "Select";
        public const string Remove = "Remove";
        public const string TitleHeroClass = "TitleHeroClass";

        public const string SelectOneBoardCard = "SelectOneBoardCard";
        public const string SelectOneHandCard = "SelectOneHandCard";
        public const string SelectTwoBoardCards = "SelectTwoBoardCards";
        public const string SelectOneRivalCard = "SelectOneRivalCard";
        public const string SelectTargetBoardCard = "SelectTargetBoardCard";
        public const string SelectFirstBoardCard = "SelectFirstBoardCard";
        public const string SelectSecondBoardCard = "SelectSecondBoardCard";
        public const string SelectThirdBoardCard = "SelectThirdBoardCard";

        public const string MAINMENU = "MainMenu";
        public const string LANGUAGECHANGESTOAPPLY = "LanguageChangesToApply";

        public const string InGameLocalization = "InGameLocalization";

        public const string HEROES_TABLE = "Heroes";
        public const string SPELLS_TABLE = "Spells";
        public const string DEMONS_TABLE = "Demons";
        public const string DEMONS_ABILITY_TABLE = "Demon Abilities";
        public const string DEITIES_TABLE = "Deities";
        public const string PASSIVES_TABLE = "Passives";
        public const string ITEMS_TABLE = "Items";
        public const string PETS_TABLE = "Pets";
        public const string HEROE_LORE_TABLE = "HeroesLore";
        public const string TIPSENTENCES = "TipSentences";
        public const string CIRCLELEVELS = "CircleLevels";
        public const string RARITY = "Rarity";

        public const string QUESTTASKS = "QuestTasks";
        public const string DAILYQUESTTASKS = "DailyQuestTasks";

        public const string REWARDTYPE = "RewardTypes";

        public const string TIPS = "Tips";

        public const string DoYouWantToDeleteYourHero = "DoYouWantToDeleteYourHero";

        private static LocalizationManager _instance;

        public static LocalizationManager Instance { get { return _instance; } }

        private Dictionary<string, int> languages = null;
        public Dictionary<string, int> Languages { get { return languages; } }

        private bool isInitialized = false;
        public bool IsInitialized { get { return isInitialized; } }

        public IEnumerator Initialize()
        {
            if (_instance == null)
            {
                _instance = this;

                DontDestroyOnLoad(gameObject);

                languages = new Dictionary<string, int>();

                yield return LocalizationSettings.InitializationOperation;

                for (int i = 0; i < LocalizationSettings.AvailableLocales.Locales.Count; i++)
                {
                    languages.Add(LocalizationSettings.AvailableLocales.Locales[i].name, i);
                }

                void Callback(Locale locale)
                {
                    GameEvents.OnChangeLanguageDone?.Invoke();
                };

                LocalizationSettings.SelectedLocaleChanged += Callback;
            }

            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[SettingsManager.Instance.Language];

            isInitialized = true;
        }

        public void ChangeLanguage(int option)
        {
            SettingsManager.Instance.UpdateLanguage(option);

            if (SceneManager.GetActiveScene().name != MAINMENU)
            {
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[option];
            }
            else
            {
                CoroutineManager.Instance.RunCoroutine(PopUpManager.Instance.Show(PopUpManager.PopUpType.Info, GetEntryValue(LANGUAGECHANGESTOAPPLY), null, OnCloseChangeLanguage));
            }
        }

        protected void OnCloseChangeLanguage()
        {
            CoroutineManager.Instance.RunCoroutine(PopUpManager.Instance.Hide(PopUpManager.PopUpType.Info));
        }

        public void SetString(LocalizeStringEvent localizeStringEvent, string key, string value)
        {
            StringVariable s = localizeStringEvent.StringReference[key] as StringVariable;
            s.Value = value;
        }

        public void SetInt(LocalizeStringEvent localizeStringEvent, string key, int value)
        {
            IntVariable intVariable = localizeStringEvent.StringReference[key] as IntVariable;
            intVariable.Value = value;
        }

        public string GetEntryValue(string entry)
        {
            return LocalizationSettings.StringDatabase.GetTable(InGameLocalization).GetEntry(entry).LocalizedValue;
        }

        public static string GetLocalizedString(string table, string entry)
        {
            return LocalizationSettings.StringDatabase.GetLocalizedString(table, entry);
        }
    }
}
