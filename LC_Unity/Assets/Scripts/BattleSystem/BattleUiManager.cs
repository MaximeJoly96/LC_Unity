using UnityEngine;
using System.Collections.Generic;
using Actors;
using BattleSystem.UI;
using UnityEngine.Events;

namespace BattleSystem
{
    public class BattleUiManager : MonoBehaviour
    {
        [SerializeField]
        private SimpleTextWindow _helpWindow;
        [SerializeField]
        private SimpleTextWindow _attackLabelWindow;
        [SerializeField]
        private TimelineUiController _timelineUiController;
        [SerializeField]
        private BattleInitInstructionsWindow _battleInitInstructionsWindow;
        [SerializeField]
        private BattleStartTag _battleStartTag;
        [SerializeField]
        private PlayerGlobalUi _playerGlobalUi;
        [SerializeField]
        private DamageDisplay _damageDisplay;
        [SerializeField]
        private TargetInfoCanvas _targetInfoCanvas;

        private List<PlayerUiPreview> _playerUiPreviews;

        private UnityEvent _battleStartTagClosed;
        public UnityEvent BattleStartTagClosed
        {
            get
            {
                if (_battleStartTagClosed == null)
                    _battleStartTagClosed = new UnityEvent();

                return _battleStartTagClosed;
            }
        }

        private UnityEvent _victoryConcluded;
        public UnityEvent VictoryConcluded
        {
            get
            {
                if(_victoryConcluded == null)
                    _victoryConcluded = new UnityEvent();

                return _victoryConcluded;
            }
        }

        private UnityEvent _defeatConcluded;
        public UnityEvent DefeatConcluded
        {
            get
            {
                if (_defeatConcluded == null)
                    _defeatConcluded = new UnityEvent();

                return _defeatConcluded;
            }
        }

        public void FeedParty(List<Character> characters)
        {
            _playerGlobalUi.FeedParty(characters);
        }

        public void ShowPlayerGlobalUi(bool show)
        {
            _playerGlobalUi.gameObject.SetActive(show);
        }

        public void HideMoveSelection()
        {
            _playerGlobalUi.CloseMoveSelectionWindow();
        }

        public void OpenPlayerGlobalUi()
        {
            ShowPlayerGlobalUi(true);
            _playerGlobalUi.OpenMoveSelectionWindow();
        }

        public void ClosePlayerGlobalUi()
        {
            ShowPlayerGlobalUi(false);
        }

        public void ShowHelpDialog(bool show)
        {
            _helpWindow.gameObject.SetActive(show);
        }

        public void OpenHelpWindow()
        {
            ShowHelpDialog(true);
            _helpWindow.Show();
        }

        public void CloseHelpWindow()
        {
            _helpWindow.Hide();
        }

        public void InitTimeline(List<BattlerBehaviour> battlers)
        {
            _timelineUiController.Feed(battlers);
        }

        public void ShowInstructionsWindow()
        {
            _battleInitInstructionsWindow.ShowWindow();
        }

        public void HideInstructionsWindow()
        {
            _battleInitInstructionsWindow.HideWindow();
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

        public void OpenTimeline()
        {
            ShowTimeline(true);
            _timelineUiController.Show();
        }

        public void CloseTimeline()
        {
            ShowTimeline(false);
        }

        public void ShowBattleStartTag()
        {
            _battleStartTag.Show();
            _battleStartTag.FinishedHidingEvent.RemoveAllListeners();
            _battleStartTag.FinishedHidingEvent.AddListener(BattleStartClosed);
        }

        public void ShowVictoryTag()
        {
            _battleStartTag.UpdateForVictory();
            _battleStartTag.FinishedHidingEvent.RemoveAllListeners();
            _battleStartTag.FinishedHidingEvent.AddListener(ConcludeVictory);
        }

        private void ConcludeVictory()
        {
            VictoryConcluded.Invoke();
        }

        public void ShowDefeatTag()
        {
            _battleStartTag.UpdateForDefeat();
            _battleStartTag.FinishedHidingEvent.RemoveAllListeners();
            _battleStartTag.FinishedHidingEvent.AddListener(ConcludeDefeat);
        }

        private void ConcludeDefeat()
        {
            DefeatConcluded.Invoke();
        }

        public void BattleStartClosed()
        {
            BattleStartTagClosed.Invoke();
        }

        public void HideAllWindows()
        {
            ShowTimeline(false);
            ShowAttackLabel(false);
            ShowHelpDialog(false);
            ShowPlayerGlobalUi(false);
        }

        public void UpdateTimeline()
        {
            _timelineUiController.UpdateTimeline();
        }

        public void FeedMoveSelectionWindow(BattlerBehaviour character)
        {
            _playerGlobalUi.FeedMoveSelectionWindow(character);
        }

        public void UpPressedOnMoveSelection()
        {
            _playerGlobalUi.UpPressedOnMoveSelection();
        }

        public void DownPressedOnMoveSelection()
        {
            _playerGlobalUi.DownPressedOnMoveSelection();
        }

        public void SelectMove()
        {
            _playerGlobalUi.SelectMove();
        }

        public List<BattlerTimeline> GetTimelines()
        {
            return _timelineUiController.Timelines;
        }

        public void DisplayDamage(Vector3 worldPosition, int damage)
        {
            _damageDisplay.DisplayDamage(worldPosition, damage);
        }

        public void DisplayStatus(Vector3 worldPosition, EffectType effect)
        {
            _damageDisplay.DisplayStatusChange(worldPosition, effect, true);
        }

        public void RemoveStatus(Vector3 worldPosition, EffectType effect)
        {
            _damageDisplay.DisplayStatusChange(worldPosition, effect, false);
        }

        public void UpdatePlayerGui(Character character)
        {
            _playerGlobalUi.UpdateCharacter(character);
        }

        public void ShowTargetInfo(BattlerBehaviour target)
        {
            _targetInfoCanvas.ShowTargetInfo(target);
        }

        public void CloseTargetInfo()
        {
            _targetInfoCanvas.Clear();
        }
    }
}
