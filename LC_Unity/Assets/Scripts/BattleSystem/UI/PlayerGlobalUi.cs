﻿using UnityEngine;
using System.Collections.Generic;
using Actors;

namespace BattleSystem.UI
{
    public class PlayerGlobalUi : MonoBehaviour
    {
        [SerializeField]
        private MoveSelectionWindow _moveSelectionWindow;
        [SerializeField]
        private Transform _characters;
        [SerializeField]
        private PlayerUiPreview _playerUiPreviewPrefab;

        private List<PlayerUiPreview> _playerUiPreviews;

        public void FeedParty(List<Character> characters)
        {
            _playerUiPreviews = new List<PlayerUiPreview>();

            for (int i = 0; i < characters.Count && i < 3; i++)
            {
                PlayerUiPreview preview = Instantiate(_playerUiPreviewPrefab, _characters);
                preview.Feed(characters[i]);
                _playerUiPreviews.Add(preview);
            }
        }

        private void Clear()
        {
            foreach (Transform child in _characters)
                Destroy(child.gameObject);
        }

        public void OpenMoveSelectionWindow()
        {
            _moveSelectionWindow.Show();
        }

        public void FeedMoveSelectionWindow(BattlerBehaviour character)
        {
            _moveSelectionWindow.Feed(character);
        }

        public void UpPressedOnMoveSelection()
        {
            _moveSelectionWindow.UpPressed();
        }

        public void DownPressedOnMoveSelection()
        {
            _moveSelectionWindow.DownPressed();
        }

        public void SelectMove()
        {
            _moveSelectionWindow.SelectMove();
        }
    }
}
