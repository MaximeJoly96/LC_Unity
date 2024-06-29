using UnityEngine;
using UnityEngine.Events;
using Party;

namespace BattleSystem
{
    public enum BattleState
    {
        Loading,
        Loaded
    }

    public class BattleManager : MonoBehaviour
    {
        private UnityEvent<BattleState> _stateChangedEvent;

        [SerializeField]
        private BattleUiManager _uiManager;

        public BattleState CurrentState { get; private set; }
        public UnityEvent<BattleState> StateChangedEvent
        {
            get
            {
                if (_stateChangedEvent == null)
                    _stateChangedEvent = new UnityEvent<BattleState>();

                return _stateChangedEvent;
            }
        }

        private void Awake()
        {
            UpdateState(BattleState.Loading);
            LoadPartyData();
            LoadActionMenu();
            OpenHelpWindow();
        }

        public void UpdateState(BattleState state)
        {
            CurrentState = state;
        }

        private void LoadPartyData()
        {
            _uiManager.FeedParty(PartyManager.Instance.GetParty());
        }

        private void LoadActionMenu()
        {
            _uiManager.ShowMoveSelection();
        }

        private void OpenHelpWindow()
        {
            _uiManager.OpenHelpWindow();
        }
    }
}
