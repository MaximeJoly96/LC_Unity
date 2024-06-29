using UnityEngine;
using System.Collections.Generic;
using Actors;
using BattleSystem.UI;

namespace BattleSystem
{
    public class BattleUiManager : MonoBehaviour
    {
        [SerializeField]
        private PlayerUiPreview _playerUiPreviewPrefab;
        [SerializeField]
        private Transform _playerPreviewsWrapper;
        [SerializeField]
        private MoveSelectionWindow _moveSelectionWindow;
        [SerializeField]
        private SimpleTextWindow _helpWindow;
        [SerializeField]
        private SimpleTextWindow _attackLabelWindow;

        private List<PlayerUiPreview> _playerUiPreviews;

        public void FeedParty(List<Character> characters)
        {
            Clear();

            _playerUiPreviews = new List<PlayerUiPreview>();

            for (int i = 0; i < characters.Count && i < 3; i++)
            {
                PlayerUiPreview preview = Instantiate(_playerUiPreviewPrefab, _playerPreviewsWrapper);

                _playerUiPreviews.Add(preview);
            }
        }

        private void Clear()
        {
            foreach (Transform child in _playerPreviewsWrapper)
                Destroy(child.gameObject);
        }

        public void ShowMoveSelection()
        {
            _moveSelectionWindow.UpdateInstructions(_playerUiPreviews[0].PlayerName);
            _moveSelectionWindow.Show();
        }

        public void OpenHelpWindow()
        {
            _helpWindow.Show();
            _attackLabelWindow.gameObject.SetActive(false);
        }
    }
}
