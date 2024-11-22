using Actors;
using Core;
using UI;

namespace Menus
{
    public class HorizontalMainMenu : HorizontalMenu
    {
        #region Methods
        protected override bool CanReceiveInput()
        {
            return GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InMenu;
        }

        public void OpenCharacterTabWithSelectedCharacter(Character character)
        {
            (_buttons[_cursorPosition] as HorizontalMainMenuButton).FeedCharacterDataToSubMenu(character);
            (_buttons[_cursorPosition] as HorizontalMainMenuButton).OpenSubMenu();
        }
        #endregion
    }
}
