using Core;
using TMPro;
using UnityEngine;
using Menus.SubMenus.Items;
using Menus.SubMenus.Status;
using Inputs;
using log4net.Util;

namespace Menus.SubMenus
{
    public class EquipmentSubMenu : SubMenu
    {
        private const float SELECTION_DELAY = 0.2f;

        private int _cursorPosition;
        private float _delay;

        [SerializeField]
        private TMP_Text _characterName;
        [SerializeField]
        private SelectableItemsList _itemsList;
        [SerializeField]
        private TMP_Text _currentItemDescription;
        [SerializeField]
        private StatsPanel _stats;
        [SerializeField]
        private EquipmentPanel _equipment;
        [SerializeField]
        private EffectsPanel _effects;

        public override void Open()
        {
            _cursorPosition = 0;
            _delay = 0.0f;

            Init();

            _itemsList.ItemHovered.RemoveAllListeners();
            _itemsList.ItemHovered.AddListener(UpdateItemDescription);

            StartCoroutine(DoOpen());
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.InMenuEquipmentTab);

            UpdateCursor();
        }

        public override void Close()
        {
            StartCoroutine(DoClose());
        }

        protected override void FinishedClosing()
        {
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.SelectingCharacterPreview);
        }

        private void UpdateItemDescription(SelectableItem item)
        {
            _currentItemDescription.text = item.Item.ItemData.Description;
        }

        private void Init()
        {
            if(_fedCharacter != null)
            {
                _characterName.text = _fedCharacter.Name;
                _stats.Feed(_fedCharacter);
                _equipment.Feed(_fedCharacter);
            }
        }

        private void Update()
        {
            if (_busy)
            {
                _delay += Time.deltaTime;

                if (_delay >= SELECTION_DELAY)
                {
                    _delay = 0.0f;
                    _busy = false;
                }
            }
        }

        protected override void HandleInputs(InputAction input)
        {
            if(!_busy)
            {
                switch(input)
                {
                    case InputAction.Select:
                        Select();
                        break;
                    case InputAction.Cancel:
                        Close();
                        break;
                    case InputAction.MoveUp:
                        MoveUp();
                        break;
                    case InputAction.MoveDown:MoveDown();
                        break;
                }

                _busy = true;
            }
        }

        private void UpdateCursor()
        {
            _equipment.UpdateCursor(_cursorPosition);
        }

        private void MoveUp()
        {
            _cursorPosition = _cursorPosition == 0 ? 4 : --_cursorPosition;
            UpdateCursor();
        }

        private void MoveDown()
        {
            _cursorPosition = _cursorPosition >= 4 ? 0 : ++_cursorPosition;
            UpdateCursor();
        }

        private void Select()
        {

        }
    }
}
