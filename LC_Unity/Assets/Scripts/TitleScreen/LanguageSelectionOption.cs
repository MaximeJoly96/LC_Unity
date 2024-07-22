using UnityEngine;
using Utils;
using System;
using TMPro;

namespace TitleScreen
{
    public class LanguageSelectionOption : SpecificTitleScreenOption
    {
        private Language[] _availableLanguages;
        private int _cursor;

        [SerializeField]
        private TMP_Text _currentLanguage;

        private void Start()
        {
            _cursor = 0;
            _availableLanguages = (Language[])Enum.GetValues(typeof(Language));

            UpdateSelectedLanguage();
        }

        private void UpdateSelectedLanguage()
        {
            _currentLanguage.text = _availableLanguages[_cursor].ToString();
        }

        public override void MoveCursorLeft()
        {
            _cursor = _cursor == 0 ? _availableLanguages.Length - 1 : --_cursor;
            UpdateSelectedLanguage();
        }

        public override void MoveCursorRight()
        {
            _cursor = _cursor == _availableLanguages.Length - 1 ? 0 : ++_cursor;
            UpdateSelectedLanguage();
        }
    }
}
