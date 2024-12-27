using UnityEngine;
using Inputs;
using System.Collections;
using Core;
using Party;
using Actors;
using Menus.SubMenus.Items;
using Utils;

namespace Menus
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField]
        protected Canvas _canvas;
        [SerializeField]
        protected CharacterSelector _characterSelector;
        [SerializeField]
        private HorizontalMainMenu _horizontalMainMenu;
        [SerializeField]
        private SpecificCharacterSelectionMenu _characterSelectionMenu;
        [SerializeField]
        private MiscellaneousData _miscData;

        protected InputController _inputController;

        public bool CanOpen { get; private set; } = true;

        private void Start()
        {
            _inputController = FindObjectOfType<InputController>();
            _inputController.ButtonClicked.AddListener(HandleInputs);
        }

        private void Init()
        {
            _horizontalMainMenu.Init();
            _horizontalMainMenu.MainMenuRefreshRequested.RemoveAllListeners();
            _horizontalMainMenu.MainMenuRefreshRequested.AddListener(RefreshCharacters);

            RefreshCharacters();

            _miscData.Open();
        }

        private void HandleInputs(InputAction input)
        {
            switch (input)
            {
                case InputAction.OpenMenu:
                    if (CanOpen && GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.OnField)
                    {
                        CommonSounds.OptionSelected();
                        Open();
                    }
                    break;
                case InputAction.Cancel:
                    if (GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InMenu)
                    {
                        CommonSounds.ActionCancelled();
                        Close();
                    }
                    break;
            }
        }

        public void Open()
        {
            StartCoroutine(OpenMenu());
        }

        public void Close()
        {
            StartCoroutine(CloseMenu());
        }

        protected IEnumerator OpenMenu()
        {
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.OpeningMenu);
            Init();
            CanvasGroup group = _canvas.GetComponent<CanvasGroup>();
            float currentAlpha = group.alpha;
            WaitForFixedUpdate wait = new WaitForFixedUpdate();

            for(float i = currentAlpha; i < 1.0f; i += 0.05f)
            {
                group.alpha = i;
                yield return wait;
            }

            group.interactable = true;
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.InMenu);
        }

        protected IEnumerator CloseMenu()
        {
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.ClosingMenu);
            CanvasGroup group = _canvas.GetComponent<CanvasGroup>();
            group.interactable = false;
            float currentAlpha = group.alpha;
            WaitForFixedUpdate wait = new WaitForFixedUpdate();

            for(float i = currentAlpha; i > 0.0f; i -= 0.05f)
            {
                group.alpha = i;
                yield return wait;
            }

            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.OnField);
        }

        private void OpenCharacterTab(Character character)
        {
            _horizontalMainMenu.OpenCharacterTabWithSelectedCharacter(character);
        }

        public void OpenCharacterTargetingWithItem(SelectableInventoryItem item)
        {
            _characterSelectionMenu.FeedItem(item);
            _characterSelectionMenu.Open();
        }

        public void ToggleAccess(bool canAccess)
        {
            CanOpen = canAccess;
        }

        private void RefreshCharacters()
        {
            _characterSelector.Clear();
            _characterSelector.Feed(PartyManager.Instance.GetParty());
            _characterSelector.CharacterSelected.RemoveAllListeners();
            _characterSelector.CharacterSelected.AddListener(OpenCharacterTab);
        }
    }
}
