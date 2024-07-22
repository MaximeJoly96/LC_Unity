using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Party;
using Language;

namespace TitleScreen
{
    public class TitleScreenManager : MonoBehaviour
    {
        private const float LOAD_DELAY = 1.0f;

        [SerializeField]
        private TextAsset _charactersData;
        
        [SerializeField]
        private MainMenuPanel _mainPanel;
        [SerializeField]
        private OptionsMenuPanel _optionsMenu;

        private void Awake()
        {
            FindObjectOfType<Localizer>().LoadLanguage(Language.Language.English);
        }

        private void Start()
        {
            PartyManager.Instance.LoadPartyFromBaseFile(_charactersData);

            _mainPanel.OptionSelected.AddListener(HandleOptionSelection);
            _optionsMenu.BackButtonEvent.AddListener(ShowMainPanel);

            ShowMainPanel();
        }

        private IEnumerator LoadNextScene()
        {
            yield return new WaitForSeconds(LOAD_DELAY);
            SceneManager.LoadScene("Field");
        }

        private IEnumerator Quit()
        {
            yield return new WaitForSeconds(LOAD_DELAY);
            Application.Quit();
        }

        private void ShowMainPanel()
        {
            _mainPanel.Show(true);
            _optionsMenu.Show(false);

            _mainPanel.Unlock();
        }

        private void ShowOptions()
        {
            _mainPanel.Show(false);
            _optionsMenu.Show(true);

            _optionsMenu.Unlock();
        }

        private void HandleOptionSelection(MainMenuPanel.MainMenuOptions option)
        {
            switch(option)
            {
                case MainMenuPanel.MainMenuOptions.NewGame:
                    StartCoroutine(LoadNextScene());
                    break;
                case MainMenuPanel.MainMenuOptions.LoadGame:
                    break;
                case MainMenuPanel.MainMenuOptions.ChangeOptions:
                    ShowOptions();
                    break;
                case MainMenuPanel.MainMenuOptions.Quit:
                    StartCoroutine(Quit());
                    break;
            }
        }
    }
}
