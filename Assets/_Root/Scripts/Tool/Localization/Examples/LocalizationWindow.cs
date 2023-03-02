using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;

namespace Tool.Localization.Examples
{
    internal abstract class LocalizationWindow : MonoBehaviour
    {
        private const int RussianIndex = 2;
        private const int EnglishIndex = 1;
        private const int CzechIndex = 0;

        [Header("Scene Components")]
        [SerializeField] private Button _russianButton;
        [SerializeField] private Button _englishButton;
        [SerializeField] private Button _czechButton;


        private void Start()
        {
            _russianButton.onClick.AddListener(() => ChangeLanguage(RussianIndex));
            _englishButton.onClick.AddListener(() => ChangeLanguage(EnglishIndex));
            _czechButton.onClick.AddListener(() => ChangeLanguage(CzechIndex));
            OnStarted();
        }

        private void OnDestroy()
        {
            _russianButton.onClick.RemoveAllListeners();
            _englishButton.onClick.RemoveAllListeners();
            _czechButton.onClick.RemoveAllListeners();
            OnDestroyed();
        }


        protected virtual void OnStarted() { }
        protected virtual void OnDestroyed() { }


        private void ChangeLanguage(int index) =>
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
    }
}
