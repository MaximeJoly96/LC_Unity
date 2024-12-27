using Core;
using Menus.SubMenus;
using UI;
using Actors;
using UnityEngine;
using UnityEngine.Events;

namespace Menus
{
    public class HorizontalMainMenuButton : HorizontalMenuButton
    {
        [SerializeField]
        protected SubMenu _subMenu;
        [SerializeField]
        protected bool _needsTarget;

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
            _subMenu.MainMenuRefreshRequested.RemoveAllListeners();
            _subMenu.MainMenuRefreshRequested.AddListener(() => MainMenuRefreshRequested.Invoke());
            _subMenu.Open();
        }
        #endregion
    }
}
