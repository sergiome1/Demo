using UnityEngine;

namespace Assets.Scripts.Managers
{

    /* NOTE: ObscuredPrefs just commented to avoid errors due to not added to this demo project */

    public class SettingsManager : MonoBehaviour
    {
        private const string MUSICVOLUME = "MusicVolume";
        private const string DIALOGVOLUME = "DialogVolume";
        private const string EFFECTSVOLUME = "EffectsVolume";
        public const string TIPSENABLED = "TipsEnabled";
        public const string LANGUAGE = "Language";
        private const string FPS = "Fps";

        private float musicVolume = 1f;
        private float dialogVolume = 1f;
        private float effectsVolume = 1f;
        private bool isTipsEnabled = true;
        private bool isFPS60 = true;
        private int language = 0;

        public float MusicVolume { get { return musicVolume; } }
        public float DialogVolume { get { return dialogVolume; } }
        public float EffectsVolume { get { return effectsVolume; } }
        public bool IsTipsEnabled { get { return isTipsEnabled; } }
        public bool IsFPS60 { get { return isFPS60; } }
        public int Language { get { return language; } }


        private static SettingsManager _instance;

        public static SettingsManager Instance
        {
            get
            {
                return _instance;
            }
        }

        public void Initialize()
        {
            if (_instance == null)
            {
                _instance = this;

                DontDestroyOnLoad(gameObject);

            //    if (ObscuredPrefs.HasKey(MUSICVOLUME))
            //    {
            //        musicVolume = ObscuredPrefs.Get<float>(MUSICVOLUME);
            //        dialogVolume = ObscuredPrefs.Get<float>(DIALOGVOLUME);
            //        effectsVolume = ObscuredPrefs.Get<float>(EFFECTSVOLUME);
            //    }
            //    else
            //    {
            //        ObscuredPrefs.Set(MUSICVOLUME, musicVolume);
            //        ObscuredPrefs.Set(DIALOGVOLUME, dialogVolume);
            //        ObscuredPrefs.Set(EFFECTSVOLUME, effectsVolume);
            //    }
            //
            //    if (ObscuredPrefs.HasKey(TIPSENABLED))
            //    {
            //        isTipsEnabled = ObscuredPrefs.Get<bool>(TIPSENABLED);
            //    }
            //    else
            //    {
            //        ObscuredPrefs.Set(TIPSENABLED, isTipsEnabled);
            //    }
            //
            //    if (ObscuredPrefs.HasKey(FPS))
            //    {
            //        isFPS60 = ObscuredPrefs.Get<bool>(FPS);
            //    }
            //    else
            //    {
            //        ObscuredPrefs.Set(FPS, isFPS60);
            //    }
            //
            //    if (ObscuredPrefs.HasKey(LANGUAGE))
            //    {
            //        language = ObscuredPrefs.Get<int>(LANGUAGE);
            //    }
            }
        }

        public void UpdateMusicVolume(float volume)
        {
            musicVolume = volume;
        //    ObscuredPrefs.Set(MUSICVOLUME, volume);
        }

        public void UpdateEffectsVolume(float volume)
        {
            effectsVolume = volume;
        //    ObscuredPrefs.Set(EFFECTSVOLUME, volume);
        }

        public void UpdateDialogVolume(float volume)
        {
            dialogVolume = volume;
        //    ObscuredPrefs.Set(DIALOGVOLUME, volume);
        }

        public void UpdateIsTipsEnabled(bool value)
        {
            isTipsEnabled = value;
        //   ObscuredPrefs.Set(TIPSENABLED, value);
        }

        public void UpdateFPS(float value)
        {
            if (Application.targetFrameRate != value)
            {
                isFPS60 = value == 0;
        //        ObscuredPrefs.Set(FPS, isFPS60);

                Application.targetFrameRate = isFPS60 ? 60 : 30;
            }
        }

        public void UpdateLanguage(int value)
        {
            language = value;
        //    ObscuredPrefs.Set(LANGUAGE, value);
        }
    }
}
