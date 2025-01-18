using UnityEngine;
using UnityEngine.SceneManagement;

namespace Splash
{
    public class SplashScreenManager : MonoBehaviour
    {
        public enum SplashScreenStatus
        {
            Loading,
            SelectingLanguage,
            ShowingDisclaimer,
            LoadingNextScene
        }

        private const float DELAY_BEFORE_SHOW = 0.5f;
        private const float DELAY_BEFORE_LOADING_TITLE = 5.0f;

        [SerializeField]
        private LanguageSelector _languageSelector;
        [SerializeField]
        private DemoDisclaimer _disclaimer;

        private float _showElementTimer;
        private SplashScreenStatus _currentStatus;

        public SplashScreenStatus CurrentStatus { get { return _currentStatus; } }

        private void Awake()
        {
            _showElementTimer = 0.0f;
            _currentStatus = SplashScreenStatus.Loading;

            _languageSelector.LanguageSelected.AddListener(LanguageWasSelected);
        }

        private void Update()
        {
            switch(_currentStatus)
            {
                case SplashScreenStatus.Loading:
                    HandleLoading();
                    break;
                case SplashScreenStatus.SelectingLanguage:
                    break;
                case SplashScreenStatus.ShowingDisclaimer:
                    HandleDisclaimer();
                    break;
            }
        }

        private void HandleLoading()
        {
            if (_showElementTimer < DELAY_BEFORE_SHOW)
            {
                _showElementTimer += Time.deltaTime;
                return;
            }

            // We only show the language menu if the game was never played on the device before
            if(PlayerPrefs.HasKey("language"))
            {
                _disclaimer.Show();
                _currentStatus = SplashScreenStatus.ShowingDisclaimer;
                _showElementTimer = 0.0f;
            }
            else
            {
                _languageSelector.Init(this);
                _languageSelector.Show();
                _currentStatus = SplashScreenStatus.SelectingLanguage;
            }
        }

        private void LanguageWasSelected()
        {
            _showElementTimer = 0.0f;
            _currentStatus = SplashScreenStatus.Loading;
            _languageSelector.Hide();
        }

        private void HandleDisclaimer()
        {
            if (_showElementTimer < DELAY_BEFORE_LOADING_TITLE)
            {
                _showElementTimer += Time.deltaTime;
                return;
            }

            _currentStatus = SplashScreenStatus.LoadingNextScene;
            SceneManager.LoadSceneAsync("TitleScreen");
        }
    }
}
