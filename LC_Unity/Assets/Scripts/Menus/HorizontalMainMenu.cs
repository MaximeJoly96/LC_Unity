using Actors;
using Core;
using UI;
using UnityEngine.Events;

namespace Menus
{
    public class HorizontalMainMenu : HorizontalMenu
    {
        protected UnityEvent _mainMenuRefreshRequested;

        public UnityEvent MainMenuRefreshRequested
        {
            get
            {
                if (_mainMenuRefreshRequested == null)
                    _mainMenuRefreshRequested = new UnityEvent();

                return _mainMenuRefreshRequested;
            }
        }

        #region Methods
        protected override bool CanReceiveInput()
        {
            return GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InMenu;
        }

        public void OpenCharacterTabWithSelectedCharacter(Character character)
        {
            HorizontalMainMenuButton hzButton = _buttons[_cursorPosition] as HorizontalMainMenuButton;

            hzButton.FeedCharacterDataToSubMenu(character);
            hzButton.OpenSubMenu();
        }

        public override void Init()
        {
            base.Init();

            HorizontalMainMenuButton hzButton = _buttons[_cursorPosition] as HorizontalMainMenuButton;
            hzButton.MainMenuRefreshRequested.RemoveAllListeners();
            hzButton.MainMenuRefreshRequested.AddListener(() => MainMenuRefreshRequested.Invoke());
        }
        #endregion
    }
}
