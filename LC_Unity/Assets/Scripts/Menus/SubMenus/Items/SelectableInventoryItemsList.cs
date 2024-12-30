using Inventory;
using Party;
using System.Collections.Generic;
using System.Linq;
using UI;
using Core;

namespace Menus.SubMenus.Items
{
    public class SelectableInventoryItemsList : SelectableList
    {
        private IEnumerable<InventoryItem> _items;

        private List<List<SelectableInventoryItem>> _pages;
        private int _currentPageIndex = 0;

        public SelectableInventoryItem CurrentItem
        {
            get { return _createdItems[_currentPageIndex * _maxItemsToDisplay + _cursorPosition] as SelectableInventoryItem; }
        }

        public void ShowContent(ItemCategory category)
        {
            ShowContent(PartyManager.Instance.Inventory.Where(i => i.ItemData.Category == category));
        }

        public void ShowContent(IEnumerable<InventoryItem> items)
        {
            _items = items;

            Clear();

            if (_maxItemsToDisplay > 0)
                CreatePages(items);

            ShowPage(_currentPageIndex);

            PlaceCursor();
        }

        protected override bool CanReceiveInputs()
        {
            return GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.BrowsingInventory;
        }

        protected void CreatePages(IEnumerable<InventoryItem> items)
        {
            if (_pages == null)
                _pages = new List<List<SelectableInventoryItem>>();

            int totalPages = items.Count() / _maxItemsToDisplay;
            int lastPageAmount = items.Count() % _maxItemsToDisplay;
            int itemsAdded = 0;

            for(int i = 0; i < totalPages; i++)
            {
                List<SelectableInventoryItem> list = new List<SelectableInventoryItem>();

                for(int j = 0; j < _maxItemsToDisplay; j++)
                {
                    SelectableInventoryItem item = AddItem() as SelectableInventoryItem;
                    item.Feed(items.ElementAt(itemsAdded));

                    item.ShowCursor(false);
                    item.gameObject.SetActive(false);

                    list.Add(item);
                    itemsAdded++;
                }

                _pages.Add(list);
            }

            List<SelectableInventoryItem> leftovers = new List<SelectableInventoryItem>();

            for(int i = 0; i < lastPageAmount; i++)
            {
                SelectableInventoryItem item = AddItem() as SelectableInventoryItem;
                item.Feed(items.ElementAt(itemsAdded));

                item.ShowCursor(false);
                item.gameObject.SetActive(false);

                leftovers.Add(item);
                itemsAdded++;
            }

            _pages.Add(leftovers);
        }

        protected void ShowPage(int pageIndex)
        {
            for(int i = 0; i < _pages[pageIndex].Count; i++)
            {
                _pages[pageIndex][i].gameObject.SetActive(true);
            }
        }

        protected void HidePage(int pageIndex)
        {
            for (int i = 0; i < _pages[pageIndex].Count; i++)
            {
                _pages[pageIndex][i].gameObject.SetActive(false);
            }
        }

        protected override void MoveCursorDown()
        {
            if(_cursorPosition == _pages[_currentPageIndex].Count - 1)
            {
                HidePage(_currentPageIndex);

                _currentPageIndex = _currentPageIndex == _pages.Count - 1 ? 0 : ++_currentPageIndex;
                _cursorPosition = 0;

                ShowPage(_currentPageIndex);
            }
            else
            {
                _cursorPosition++;
            }

            PlaceCursor();
            SelectionChanged.Invoke();
        }

        protected override void MoveCursorUp()
        {
            if(_cursorPosition == 0)
            {
                HidePage(_currentPageIndex);

                _currentPageIndex = _currentPageIndex == 0 ? _pages.Count - 1 : --_currentPageIndex;
                _cursorPosition = 0;

                ShowPage(_currentPageIndex);
            }
            else
            {
                _cursorPosition--;
            }

            PlaceCursor();
            SelectionChanged.Invoke();
        }

        protected override void PlaceCursor()
        {
            for (int i = 0; i < _createdItems.Count; i++)
            {
                _createdItems[i].ShowCursor(i == (_currentPageIndex * _maxItemsToDisplay) + _cursorPosition);
            }
        }

        public override void Clear()
        {
            base.Clear();

            if(_pages != null)
            {
                _pages.ForEach(p => p.Clear());
                _pages.Clear();
            }
        }
    }
}
