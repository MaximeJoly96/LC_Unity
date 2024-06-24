using UnityEngine;
using Menus.SubMenus;
using Core;
using Actors;

namespace Menus
{
    public class SubMenuButton : MonoBehaviour
    {
        [SerializeField]
        private bool _needsTarget;
        [SerializeField]
        private SubMenu _subMenu;

        private Transform Background
        {
            get { return transform.Find("Bg"); }
        }

        private Transform Cursor
        {
            get { return transform.Find("CursorWrapper"); }
        }

        public void DisplayCursor(bool display)
        {
            Background.gameObject.SetActive(display);
            Cursor.gameObject.SetActive(display);
        }

        public void SelectSubMenu()
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

        public void OpenSubMenu()
        {
            _subMenu.Open();
        }

        public void FeedCharacterDataToSubMenu(Character character)
        {
            _subMenu.Feed(character);
        }
    }
}
