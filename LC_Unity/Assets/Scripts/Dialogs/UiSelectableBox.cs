using Engine.Events;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogs
{
    public class UiSelectableBox<T> : UiBox<T> where T : IRunnable
    {
        protected int _currentSelectionIndex;
        protected List<SelectableDialogItem> _selectableItems;

        [SerializeField]
        protected SelectableDialogItem _selectableItemPrefab;
        [SerializeField]
        protected Transform _wrapper;

        public override void Feed(T element)
        {
            base.Feed(element);
            _selectableItems = new List<SelectableDialogItem>();
        }

        public virtual void MoveCursorUp()
        {
        }

        public virtual void MoveCursorDown() 
        {
        }

        public virtual void MoveCursorLeft() 
        {
        }
        public virtual void MoveCursorRight() 
        {
        }

        public virtual string Validate() { return ""; }
        protected virtual void CreateItems() { }

        protected void UpdateCursorPosition(int position)
        {
            for (int i = 0; i < _selectableItems.Count; i++)
            {
                _selectableItems[i].ShowCursor(i == position);
            }
        }

        public override void Close()
        {
            for (int i = 0; i < _selectableItems.Count; i++)
                _selectableItems[i].gameObject.SetActive(false);

            base.Close();
        }

        public override void FinishedOpening()
        {
            CreateItems();

            _currentSelectionIndex = 0;
            UpdateCursorPosition(_currentSelectionIndex);
        }
    }
}