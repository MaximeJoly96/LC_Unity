using UnityEngine;
using Utils;
using System;
using TMPro;

namespace TitleScreen
{
    public class LanguageSelectionOption : SpecificTitleScreenOption
    {
        private Language[] _availableLanguages;
        private int _cursorPosition;

        [SerializeField]
        private TMP_Text _currentLanguage;

        private void Start()
        {
            _cursorPosition = 0;
            _availableLanguages = (Language[])Enum.GetValues(typeof(Language));

            UpdateSelectedLanguage();
        }

        private void UpdateSelectedLanguage()
        {
            _currentLanguage.text = _availableLanguages[_cursorPosition].ToString();
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
