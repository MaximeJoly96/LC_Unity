using Core;
using TMPro;
using UnityEngine;
using Menus.SubMenus.Items;
using Menus.SubMenus.Status;

namespace Menus.SubMenus
{
    public class EquipmentSubMenu : SubMenu
    {
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
    }
}
