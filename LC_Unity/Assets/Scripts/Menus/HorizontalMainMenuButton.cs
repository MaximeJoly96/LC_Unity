using Core;
using Menus.SubMenus;
using UI;
using Actors;
using UnityEngine;

namespace Menus
{
    public class HorizontalMainMenuButton : HorizontalMenuButton
    {
        [SerializeField]
        protected SubMenu _subMenu;
        [SerializeField]
        protected bool _needsTarget;

        #region Methods
        public override void SelectButton()
        {
            if (_needsTarget)
            {
                PromptCharacterSelection();
            }
            else
                OpenSubMenu();
        }

        private void PromptCharacterSelection()
        {
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.SelectingCharacterPreview);
            FindObjectOfType<CharacterSelector>().HoverCharacters();
        }

        public void FeedCharacterDataToSubMenu(Character character)
        {
            _subMenu.Feed(character);
        }

        public void OpenSubMenu()
        {
            _subMenu.Open();
        }
        #endregion
    }
}
