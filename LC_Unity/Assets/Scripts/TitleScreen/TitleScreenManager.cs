using UnityEngine;
using Save;
using System.Collections;
using Party;
using Language;
using Core;

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
            Localizer localizer = FindObjectOfType<Localizer>();

            if(localizer != null)
                localizer.LoadLanguage((Language.Language)PlayerPrefs.GetInt("language"));
        }

        private void Start()
        {
            PartyManager.Instance.LoadPartyFromBaseFile(_charactersData);
            SaveManager.Instance.SaveCancelledEvent.AddListener(() => ShowMainPanel());

            _mainPanel.OptionSelected.AddListener(HandleOptionSelection);
            _optionsMenu.BackButtonEvent.AddListener(ShowMainPanel);

            ShowMainPanel();
        }

        private void ShowSaveCreationScreen()
        {
            _mainPanel.Show(false);

            SaveManager.Instance.InitSaveCreation();
        }

        private void ShowSaveLoadScreen()
        {
            _mainPanel.Show(false);

            SaveManager.Instance.InitSaveLoad();
        }

        private IEnumerator Quit()
        {
            yield return new WaitForSeconds(LOAD_DELAY);
            Application.Quit();
        }

        private void ShowMainPanel()
        {
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.TitleScreen);
            _mainPanel.Show(true);
            _optionsMenu.Show(false);

            _mainPanel.Unlock();
        }

        private void ShowOptions()
        {
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.TitleScreenOptions);
            _mainPanel.Show(false);
            _optionsMenu.Show(true);

            _optionsMenu.Unlock();
        }

        private void HandleOptionSelection(MainMenuPanel.MainMenuOptions option)
        {
            switch(option)
            {
                case MainMenuPanel.MainMenuOptions.NewGame:
                    ShowSaveCreationScreen();
                    break;
                case MainMenuPanel.MainMenuOptions.LoadGame:
                    ShowSaveLoadScreen();
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
