﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Core;
using Utils;

namespace UI
{
    public class SelectableList : MonoBehaviour
    {
        [SerializeField]
        protected SelectableItem _selectableItemPrefab;

        protected List<SelectableItem> _createdItems;
        protected int _cursorPosition;

        public List<SelectableItem> CreatedItems { get { return _createdItems; } }
        public int CursorPosition { get { return _cursorPosition; } }

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

            receiver.OnMoveUp.AddListener(() =>
            {
                CommonSounds.CursorMoved();
                MoveCursorUp();
            });

            receiver.OnMoveDown.AddListener(() =>
            {
                CommonSounds.CursorMoved();
                MoveCursorDown();
            });
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
        }

        protected virtual void MoveCursorUp()
        {
            _cursorPosition = _cursorPosition == 0 ? _createdItems.Count - 1 : --_cursorPosition;
            PlaceCursor();
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
    }
}
