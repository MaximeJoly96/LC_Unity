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
        [SerializeField]
        private TimelineUiController _timelineUiController;
        [SerializeField]
        private BattleInitInstructionsWindow _battleInitInstructionsWindow;

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

        public void OpenMoveSelection()
        {
            _moveSelectionWindow.UpdateInstructions(_playerUiPreviews[0].PlayerName);
            _moveSelectionWindow.Show();
        }

        public void ShowMoveSelection(bool show)
        {
            _moveSelectionWindow.gameObject.SetActive(show);
        }

        public void ShowHelpDialog(bool show)
        {
            _helpWindow.gameObject.SetActive(show);
        }

        public void OpenHelpWindow()
        {
            _helpWindow.Show();
            _attackLabelWindow.gameObject.SetActive(false);
        }

        public void InitTimeline(List<BattlerBehaviour> battlers)
        {
            _timelineUiController.Feed(battlers);
        }

        public void ShowInstructionsWindow()
        {
            _battleInitInstructionsWindow.ShowWindow();
        }

        public void UpdateInstructions(BattleState state)
        {
            string key = "";

            switch(state)
            {
                case BattleState.PlacingCharacters:
                    key = "battleInitInstructionsSelectionPhase";
                    break;
                case BattleState.SwappingCharacters:
                    key = "battleInitInstructionsConfirmPositionChange";
                    break;
                default:
                    return;
            }

            _battleInitInstructionsWindow.UpdateText(key);
        }

        public void ShowAttackLabel(bool show)
        {
            _attackLabelWindow.gameObject.SetActive(show);
        }

        public void ShowTimeline(bool show)
        {
            _timelineUiController.gameObject.SetActive(show);
        }
    }
}
