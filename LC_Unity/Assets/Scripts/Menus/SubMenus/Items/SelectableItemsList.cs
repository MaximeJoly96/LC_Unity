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
            Init(PartyManager.Instance.Inventory.Where(i => i.ItemData.Category == category));
        }

        public void Init(IEnumerable<InventoryItem> items)
        {
            Clear();
            _cursorPosition = 0;
            _items = new List<SelectableItem>();

            for (int i = 0; i < items.Count(); i++)
            {
                SelectableItem item = Instantiate(_selectableItemPrefab, transform);
                item.Feed(items.ElementAt(i));

                _items.Add(item);
            }

            PlaceCursor();
        }

        public void Clear()
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
            // I could have made it with a stack of events, which would have been cleaner. BUT I assumed that stack would
            // contain 12 calls and thought a simple Find would be faster and more optimized.
            // If I can prove the clean way is faster, then I will refactor this.
            FindObjectOfType<MainMenuController>().OpenCharacterTargetingWithItem(_items[_cursorPosition]);
        }

        private void PlaceCursor()
        {
            if(_items.Count > 0)
            {
                for (int i = 0; i < _items.Count; i++)
                {
                    _items[i].ShowCursor(_cursorPosition == i);
                }

                ItemHovered.Invoke(_items[_cursorPosition]);
            }
        }
    }
}
