using UnityEngine;
using UnityEngine.Audio;

namespace _Game.Scripts.Controllers
{
    public class SoundSettingController : MonoBehaviour
    {
        private static SoundSettingController _instance;
        public static SoundSettingController Instance => _instance;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(this);
            }
        }

        [SerializeField] private AudioMixer mixer;

        private const string MUSIC_VOLUME_KEY = "MusicVolume";
        private const string SFX_VOLUME_KEY = "SfxVolume";

        public float SFXVolume
        {
            get => PlayerPrefs.GetFloat(SFX_VOLUME_KEY, 1);
            set => PlayerPrefs.SetFloat(SFX_VOLUME_KEY, value);
        }

        public float MusicVolume
        {
            get => PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, 1);
            set => PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, value);
        }

        private void Start()
        {
            mixer.SetFloat("MusicVolume", GetDB(MusicVolume));
            mixer.SetFloat("SfxVolume", GetDB(SFXVolume));
        }

        public void OnHideButtonClicked()
        {
            Hide();
        }

        public void Show()
        {
            this.gameObject.SetActive(true);
        }

        public void Hide()
        {
            this.gameObject.SetActive(false);
        }

        public void ChangeMusicVolume(float value)
        {
            MusicVolume = value;
            mixer.SetFloat("MusicVolume", GetDB(value));
            PlayerPrefs.Save();
        }

        public void ChangeSfxVolume(float value)
        {
            SFXVolume = value;
            mixer.SetFloat("SfxVolume", GetDB(value));
            PlayerPrefs.Save();
        }

        private float GetDB(float normalizedValue)
        {
            return Mathf.Log10(Mathf.Max(normalizedValue, 0.0001f)) * 20f;
        }
    }
}