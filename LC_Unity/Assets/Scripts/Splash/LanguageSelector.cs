using UnityEngine;
using TMPro;
using Language;
using Core;
using Utils;
using UnityEngine.Events;

namespace Splash
{
    public class LanguageSelector : SplashScreenElement
    {
        [SerializeField]
        private TMP_Text _selectLanguageText;
        [SerializeField]
        private Transform _languagesWrapper;
        [SerializeField]
        private SelectableLanguage[] _languages;

        private InputReceiver _inputReceiver;
        private int _cursorPosition;
        private SplashScreenManager _manager;
        private UnityEvent _languageSelected;

        public UnityEvent LanguageSelected
        {
            get
            {
                if(_languageSelected == null)
                    _languageSelected = new UnityEvent();

                return _languageSelected;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            _inputReceiver = GetComponent<InputReceiver>();
            BindInputs();
        }

        public void Init(SplashScreenManager manager)
        {
            _cursorPosition = 0;
            _manager = manager;

            for (int i = 0; i < Localizer.Instance.Localizations.Length; i++)
            {
                _languages[i].Feed(Localizer.Instance.Localizations[i]);
                _languages[i].ShowCursor(false);
            }

            UpdateLanguage();
            _languages[_cursorPosition].ShowCursor(true);
        }

        #region Inputs
        private void BindInputs()
        {
            _inputReceiver.OnMoveLeft.AddListener(() =>
            {
                if (CanReceiveInput())
                {
                    CommonSounds.CursorMoved();
                    MoveCursorLeft();
                }
            });

            _inputReceiver.OnMoveRight.AddListener(() =>
            {
                if (CanReceiveInput())
                {
                    CommonSounds.CursorMoved();
                    MoveCursorRight();
                }
            });

            _inputReceiver.OnSelect.AddListener(() =>
            {
                if (CanReceiveInput())
                {
                    CommonSounds.OptionSelected();
                    SelectLanguage();
                }
            });
        }

        private void MoveCursorLeft()
        {
            _cursorPosition = _cursorPosition == 0 ? _languages.Length - 1 : --_cursorPosition;
            UpdateCursor();
        }

        private void MoveCursorRight()
        {
            _cursorPosition = _cursorPosition == _languages.Length - 1 ? 0 : ++_cursorPosition;
            UpdateCursor();
        }

        private void UpdateCursor()
        {
            for(int i = 0; i < _languages.Length; i++)
            {
                _languages[i].ShowCursor(_cursorPosition == i);
            }

            UpdateLanguage();
        }

        private void SelectLanguage()
        {
            LanguageSelected.Invoke();
        }

        private void UpdateLanguage()
        {
            Localization localization = Localizer.Instance.Localizations[_cursorPosition];

            Localizer.Instance.LoadLanguage(localization.language);
            PlayerPrefs.SetInt("language", (int)localization.language);
        }

        private bool CanReceiveInput()
        {
            if (!_manager)
                return false;

            return _manager.CurrentStatus == SplashScreenManager.SplashScreenStatus.SelectingLanguage;
        }
        #endregion

    }
}
