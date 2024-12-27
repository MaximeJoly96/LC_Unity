using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Core;
using Utils;
using UnityEngine.Events;

namespace UI
{
    public class SelectableList : MonoBehaviour
    {
        [SerializeField]
        protected SelectableItem _selectableItemPrefab;

        protected List<SelectableItem> _createdItems;
        protected int _cursorPosition;

        protected UnityEvent _selectionCancelled;
        protected UnityEvent _selectionChanged;
        protected UnityEvent _itemSelected;

        public List<SelectableItem> CreatedItems { get { return _createdItems; } }
        public int CursorPosition { get { return _cursorPosition; } }
        public SelectableItem SelectedItem { get { return _createdItems[_cursorPosition]; } }
        public UnityEvent SelectionCancelled
        {
            get
            {
                if(_selectionCancelled == null)
                    _selectionCancelled = new UnityEvent();

                return _selectionCancelled;
            }
        }
        public UnityEvent SelectionChanged
        {
            get
            {
                if (_selectionChanged == null)
                    _selectionChanged = new UnityEvent();

                return _selectionChanged;
            }
        }

        public UnityEvent ItemSelected
        {
            get
            {
                if(_itemSelected == null)
                    _itemSelected = new UnityEvent();

                return _itemSelected;
            }
        }

        protected virtual void Awake()
        {
            _createdItems = new List<SelectableItem>();
        }

        public virtual void Init()
        {
            _cursorPosition = 0;
            BindInputs();
        }

        protected virtual void BindInputs()
        {
            InputReceiver receiver = GetComponent<InputReceiver>();

            receiver.OnMoveUp.RemoveAllListeners();
            receiver.OnMoveDown.RemoveAllListeners();
            receiver.OnCancel.RemoveAllListeners();
            receiver.OnSelect.RemoveAllListeners();

            receiver.OnMoveUp.AddListener(() =>
            {
                if(CanReceiveInputs())
                {
                    CommonSounds.CursorMoved();
                    MoveCursorUp();
                }
            });

            receiver.OnMoveDown.AddListener(() =>
            {
                if(CanReceiveInputs())
                {
                    CommonSounds.CursorMoved();
                    MoveCursorDown();
                }
            });

            receiver.OnCancel.AddListener(() =>
            {
                if (CanReceiveInputs())
                {
                    CommonSounds.ActionCancelled();
                    Cancel();
                }
            });

            receiver.OnSelect.AddListener(() =>
            {
                if (CanReceiveInputs())
                {
                    SelectItem();
                }
            });
        }

        protected virtual bool CanReceiveInputs()
        {
            return true;
        }

        public SelectableItem AddItem()
        {
            SelectableItem item = Instantiate(_selectableItemPrefab, transform);
            _createdItems.Add(item);

            item.ShowCursor(false);

            return item;
        }

        public void SetItemPrefab(SelectableItem prefab)
        {
            _selectableItemPrefab = prefab;
        }

        protected virtual void MoveCursorDown()
        {
            _cursorPosition = _cursorPosition == _createdItems.Count - 1 ? 0 : ++_cursorPosition;
            PlaceCursor();
            SelectionChanged.Invoke();
        }

        protected virtual void MoveCursorUp()
        {
            _cursorPosition = _cursorPosition == 0 ? _createdItems.Count - 1 : --_cursorPosition;
            PlaceCursor();
            SelectionChanged.Invoke();
        }

        protected virtual void PlaceCursor()
        {
            for(int i = 0; i < _createdItems.Count; i++)
            {
                _createdItems[i].ShowCursor(i == _cursorPosition);
            }
        }

        public void HoverFirstItem()
        {
            _cursorPosition = 0;
            PlaceCursor();
        }

        protected virtual void SelectItem()
        {
            ItemSelected.Invoke();
        }

        public virtual void Clear()
        {
            _createdItems = new List<SelectableItem>();

            foreach (Transform child in transform)
                Destroy(child.gameObject);
        }

        protected virtual void Cancel()
        {
            SelectionCancelled.Invoke();
        }
    }
}
