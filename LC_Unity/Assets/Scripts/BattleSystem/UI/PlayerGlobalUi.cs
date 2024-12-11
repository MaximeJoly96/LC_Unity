using UnityEngine;
using System.Collections.Generic;
using Actors;
using System.Linq;

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

        public void FeedParty(List<Actors.Character> characters)
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

        public void CloseMoveSelectionWindow()
        {
            _moveSelectionWindow.Hide();
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

        public void UpdateCharacter(Actors.Character character)
        {
            PlayerUiPreview preview = _playerUiPreviews.FirstOrDefault(p => p.PlayerName == character.Name);

            if (preview)
                preview.Feed(character);
        }

        public void UpdateCharacterAction(BattlerBehaviour battler)
        {
            PlayerUiPreview preview = _playerUiPreviews.FirstOrDefault(p => p.PlayerName == battler.BattlerData.Character.Name);

            if (preview)
                preview.UpdateCharacterAction(battler.LockedInAbility);
        }
    }
}
