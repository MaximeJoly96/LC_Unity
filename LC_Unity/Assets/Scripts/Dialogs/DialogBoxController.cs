﻿using UnityEngine;
using Engine.Events;
using Inputs;
using Engine.Message;

namespace Dialogs
{
    public class DialogBoxController : CanvasMessageController
    {
        private DialogBox _currentDialogBox;

        [SerializeField]
        private DialogBox _dialogBoxPrefab;

        protected override void Start()
        {
            base.Start();
            _inputController.LeftClick.AddListener(TryToCloseDialog);
        }

        public void CreateDialog(DisplayDialog dialog)
        {
            _currentDialogBox = Instantiate(_dialogBoxPrefab, _canvas.transform);

            _currentDialogBox.Feed(dialog);
            _currentDialogBox.Open();
            _currentDialogBox.HasClosed.AddListener(DestroyCurrentDialog);
        }

        protected override void ReceiveInput(InputAction input)
        {
            TryToCloseDialog(input);
        }

        private void TryToCloseDialog(Vector2 mousePosition)
        {
            CloseDialog();
        }

        private void TryToCloseDialog(InputAction button)
        {
            if (button == InputAction.Select)
                CloseDialog();
        }

        private void CloseDialog()
        {
            if (_currentDialogBox)
                _currentDialogBox.Close();
        }

        private void DestroyCurrentDialog()
        {
            Destroy(_currentDialogBox.gameObject);
        }
    }
}