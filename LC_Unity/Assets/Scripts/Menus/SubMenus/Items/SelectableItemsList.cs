using UnityEngine;
using Inventory;
using Party;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

namespace Menus.SubMenus.Items
{
    public class SelectableItemsList : MonoBehaviour
    {
        [SerializeField]
        private SelectableItem _selectableItemPrefab;

        private int _cursorPosition;
        private List<SelectableItem> _items;
        private UnityEvent<SelectableItem> _itemHovered;

        public UnityEvent<SelectableItem> ItemHovered
        {
            get
            {
                if (_itemHovered == null)
                    _itemHovered = new UnityEvent<SelectableItem>();

                return _itemHovered;
            }
        }


        public void Init(ItemCategory category)
        {
            Clear();
            _cursorPosition = 0;
            _items = new List<SelectableItem>();

            IEnumerable<InventoryItem> inventory = PartyManager.Instance.Inventory.Where(i => i.Category == category);

            for(int i = 0; i < inventory.Count(); i++)
            {
                SelectableItem item = Instantiate(_selectableItemPrefab, transform);
                item.Feed(inventory.ElementAt(i));

                _items.Add(item);
            }

            PlaceCursor();
        }

        private void Clear()
        {
            foreach (Transform child in transform)
                Destroy(child.gameObject);
        }

        public void MoveCursorUp()
        {
            _cursorPosition = _cursorPosition == 0 ? _items.Count - 1 : --_cursorPosition;
            PlaceCursor();
        }

        public void MoveCursorDown()
        {
            _cursorPosition = _cursorPosition == _items.Count - 1 ? 0 : ++_cursorPosition;
            PlaceCursor();
        }

        public void Select()
        {

        }

        private void PlaceCursor()
        {
            for(int i = 0; i < _items.Count; i++)
            {
                _items[i].ShowCursor(_cursorPosition == i);
            }

            ItemHovered.Invoke(_items[_cursorPosition]);
        }
    }
}
