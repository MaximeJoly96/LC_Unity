using UnityEngine;
using Language;
using System;
using TMPro;

namespace TitleScreen
{
    public class LanguageSelectionOption : SpecificTitleScreenOption
    {
        private Language.Language[] _availableLanguages;
        private int _cursorPosition;

        [SerializeField]
        private TMP_Text _currentLanguage;

        private void Start()
        {
            _cursorPosition = PlayerPrefs.GetInt("language");
            _availableLanguages = (Language.Language[])Enum.GetValues(typeof(Language.Language));

            UpdateSelectedLanguage();
        }

        private void UpdateSelectedLanguage()
        {
            _currentLanguage.text = LanguageUtility.TranslateLanguageLabel(_availableLanguages[_cursorPosition]);
            Localizer.Instance.LoadLanguage(_availableLanguages[_cursorPosition]);
            PlayerPrefs.SetInt("language", (int)_availableLanguages[_cursorPosition]);
        }

        public override void MoveCursorLeft()
        {
            _cursorPosition = _cursorPosition == 0 ? _availableLanguages.Length - 1 : --_cursorPosition;
            UpdateSelectedLanguage();
        }

        public override void MoveCursorRight()
        {
            _cursorPosition = _cursorPosition == _availableLanguages.Length - 1 ? 0 : ++_cursorPosition;
            UpdateSelectedLanguage();
        }
    }
}
