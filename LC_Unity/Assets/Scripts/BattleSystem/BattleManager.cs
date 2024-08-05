using UnityEngine;
using UnityEngine.Events;
using Party;
using BattleSystem.Data;
using BattleSystem.Model;
using Engine.SceneControl;
using System.Collections.Generic;
using Actors;
using BattleSystem.UI;
using Inputs;
using System;
using Abilities;
using System.Linq;

namespace BattleSystem
{
    public enum BattleState
    {
        Loading,
        Loaded,
        PlacingCharacters,
        SwappingCharacters,
        BattleStart,
        ComputingEnemyTurn,
        PlayerMoveSelection,
        TargetSelection
    }

    public class BattleManager : MonoBehaviour
    {
        private const float SELECTION_DELAY = 0.2f; // seconds

        private UnityEvent<BattleState> _stateChangedEvent;
        private List<BattlerBehaviour> _enemiesInCombat;
        private List<BattlerBehaviour> _charactersInCombat;

        private bool _delayOn;
        private float _selectionDelay;

        private int _characterPlacementCursorPosition;
        private PlacementCursor _firstPlacementCursor;
        private PlacementCursor _secondPlacementCursor;
        private BattlerBehaviour _selectedCharacterForSwap;
        private EnemiesManager _enemiesManager;
        private List<BattlerBehaviour> _allBattlers;
        private TurnManager _turnManager;

        [SerializeField]
        private BattleUiManager _uiManager;
        [SerializeField]
        private BattlersHolder _battlersHolder;
        [SerializeField]
        private BattlersHolder _charactersHolder;
        [SerializeField]
        private PlacementCursor _characterSelectionCursor;
        [SerializeField]
        private TextAsset _troops;
        [SerializeField]
        private TextAsset _enemies;
        [SerializeField]
        private TargetManager _targetManager;
        [SerializeField]
        private BattleProcessor _battleProcessor;

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

        public List<BattlerBehaviour> AllBattlers
        {
            get
            {
                if(_allBattlers == null)
                {
                    _allBattlers = new List<BattlerBehaviour>();
                    _allBattlers.AddRange(_charactersInCombat);
                    _allBattlers.AddRange(_enemiesInCombat);
                }

                return _allBattlers;
            }
        }

        private void Awake()
        {
            FindObjectOfType<InputController>().ButtonClicked.AddListener(ReceiveInput);

            _selectionDelay = 0.0f;
            _delayOn = false;
            _enemiesInCombat = new List<BattlerBehaviour>();
            _charactersInCombat = new List<BattlerBehaviour>();

            StateChangedEvent.AddListener(FollowStateChange);

            UpdateState(BattleState.Loading);
            LoadPartyData();

            LoadBattlers();
            LoadCharacters();

            UpdateState(BattleState.Loaded);
            
            InitTimeline();

            _uiManager.HideAllWindows();

            ShowInstructions();
            CreateFirstPlacementCursor();

            UpdateState(BattleState.PlacingCharacters);
        }

        #region Inputs
        private void ReceiveInput(InputAction input)
        {
            if(!_delayOn)
            {
                switch (input)
                {
                    case InputAction.Cancel:
                        CancelButtonPressed();
                        break;
                    case InputAction.Select:
                        SelectButtonPressed();
                        break;
                    case InputAction.MoveLeft:
                        MoveLeftPressed();
                        break;
                    case InputAction.MoveRight:
                        MoveRightPressed();
                        break;
                    case InputAction.MoveUp:
                        MoveUpPressed();
                        break;
                    case InputAction.MoveDown:
                        MoveDownPressed();
                        break;
                    case InputAction.OpenMenu:
                        StartButtonPressed();
                        break;
                }

                _delayOn = true;
            }
            
        }

        protected void Update()
        {
            if (_delayOn)
            {
                _selectionDelay += Time.deltaTime;
                if (_selectionDelay > SELECTION_DELAY)
                {
                    _selectionDelay = 0.0f;
                    _delayOn = false;
                }
            }
        }

        private void SelectButtonPressed()
        {
            switch (CurrentState)
            {
                case BattleState.PlacingCharacters:
                    _firstPlacementCursor.StopAnimation();
                    _selectedCharacterForSwap = _charactersInCombat[_characterPlacementCursorPosition];
                    CreateSecondPlacementCursor();
                    UpdateState(BattleState.SwappingCharacters);
                    UpdateInstructions();
                    break;
                case BattleState.SwappingCharacters:
                    _secondPlacementCursor.StopAnimation();
                    ClearPlacementCursors();
                    SwapCharacters();
                    CreateFirstPlacementCursor();
                    UpdateState(BattleState.PlacingCharacters);
                    UpdateInstructions();
                    break;
                case BattleState.PlayerMoveSelection:
                    _uiManager.SelectMove();
                    break;
                case BattleState.TargetSelection:
                    _targetManager.ConfirmTarget(_turnManager.CurrentCharacter);
                    _targetManager.Clear();
                    _uiManager.UpdateTimeline();
                    _turnManager.SwitchToNextCharacter();
                    UpdateState(BattleState.PlayerMoveSelection);
                    break;
            }
        }

        private void CancelButtonPressed()
        {
            switch(CurrentState)
            {
                case BattleState.SwappingCharacters:
                    _secondPlacementCursor.StopAnimation();
                    ClearPlacementCursors();
                    CreateFirstPlacementCursor();
                    UpdateState(BattleState.PlacingCharacters);
                    UpdateInstructions();
                    break;
                case BattleState.TargetSelection:
                    _targetManager.Clear();
                    UpdateState(BattleState.PlayerMoveSelection);
                    break;
            }
        }

        private void MoveLeftPressed()
        {
            switch(CurrentState)
            {
                case BattleState.PlacingCharacters:
                    _characterPlacementCursorPosition = _characterPlacementCursorPosition == 0 ?
                                                        _charactersInCombat.Count - 1 : --_characterPlacementCursorPosition;
                    UpdatePlacementCursor(_firstPlacementCursor);
                    break;
                case BattleState.SwappingCharacters:
                    _characterPlacementCursorPosition = _characterPlacementCursorPosition == 0 ?
                                                        _charactersInCombat.Count - 1 : --_characterPlacementCursorPosition;
                    UpdatePlacementCursor(_secondPlacementCursor);
                    break;
            }
        }

        private void MoveRightPressed()
        {
            switch (CurrentState)
            {
                case BattleState.PlacingCharacters:
                    _characterPlacementCursorPosition = _characterPlacementCursorPosition == _charactersInCombat.Count - 1 ?
                                                        0 : ++_characterPlacementCursorPosition;
                    UpdatePlacementCursor(_firstPlacementCursor);
                    break;
                case BattleState.SwappingCharacters:
                    _characterPlacementCursorPosition = _characterPlacementCursorPosition == _charactersInCombat.Count - 1 ?
                                                        0 : ++_characterPlacementCursorPosition;
                    UpdatePlacementCursor(_secondPlacementCursor);
                    break;
            }
        }

        private void MoveUpPressed()
        {
            switch(CurrentState)
            {
                case BattleState.PlayerMoveSelection:
                    _uiManager.UpPressedOnMoveSelection();
                    break;
            }
        }

        private void MoveDownPressed()
        {
            switch (CurrentState)
            {
                case BattleState.PlayerMoveSelection:
                    _uiManager.DownPressedOnMoveSelection();
                    break;
            }
        }

        private void StartButtonPressed()
        {
            ClearPlacementCursors();
            UpdateState(BattleState.BattleStart);
            _uiManager.HideInstructionsWindow();
            _uiManager.BattleStartTagClosed.RemoveAllListeners();
            _uiManager.BattleStartTagClosed.AddListener(OpenAllCombatWindows);

            _turnManager = new TurnManager(_charactersInCombat);

            UpdateState(BattleState.ComputingEnemyTurn);
            ComputeEnemyTurn();
            UpdateState(BattleState.PlayerMoveSelection);
        }
        #endregion

        public void UpdateState(BattleState state)
        {
            CurrentState = state;
            StateChangedEvent.Invoke(state);
        }

        private void LoadPartyData()
        {
            _uiManager.FeedParty(PartyManager.Instance.GetParty());
        }

        private void OpenHelpWindow()
        {
            _uiManager.OpenHelpWindow();
        }

        private void LoadBattlers()
        {
            BattleProcessing battle = BattleDataHolder.Instance.BattleData;

            TroopParser troopParser = new TroopParser();
            EnemyParser enemyParser = new EnemyParser();
            Troop troop = troopParser.ParseTroop(_troops, battle.TroopId);
            List<Battler> battlers = new List<Battler>();

            for(int i = 0; i < troop.Members.Count; i++)
            {
                battlers.Add(enemyParser.ParseEnemy(_enemies, troop.Members[i]));
            }

            for(int i = 0; i < battlers.Count; i++)
            {
                BattlerBehaviour behaviour = _battlersHolder.InstantiateBattler(battlers[i]);
                behaviour.IsEnemy = true;
                _enemiesInCombat.Add(behaviour);
            }
        }

        private void LoadCharacters()
        {
            List<Character> characters = PartyManager.Instance.GetParty();

            for(int i = 0; i < characters.Count; i++)
            {
                BattlerBehaviour behaviour = _charactersHolder.InstantiateBattler(new Battler(characters[i]));
                behaviour.IsEnemy = false;
                _charactersInCombat.Add(behaviour);
            }
        }

        private void InitTimeline()
        {
            _uiManager.InitTimeline(AllBattlers);
        }

        private void ShowInstructions()
        {
            _uiManager.ShowInstructionsWindow();
        }

        private void UpdateInstructions()
        {
            _uiManager.UpdateInstructions(CurrentState);
        }

        private void CreateFirstPlacementCursor()
        {
            _characterPlacementCursorPosition = 0;
            _firstPlacementCursor = Instantiate(_characterSelectionCursor);
            UpdatePlacementCursor(_firstPlacementCursor);
        }

        private void UpdatePlacementCursor(PlacementCursor cursor)
        {
            cursor.transform.position = _charactersInCombat[_characterPlacementCursorPosition].transform.position;
        }

        private void CreateSecondPlacementCursor()
        {
            _secondPlacementCursor = Instantiate(_characterSelectionCursor);
            UpdatePlacementCursor(_secondPlacementCursor);
        }

        private void ClearPlacementCursors()
        {
            if(_firstPlacementCursor)
                Destroy(_firstPlacementCursor.gameObject);

            if(_secondPlacementCursor)
                Destroy(_secondPlacementCursor.gameObject);
        }

        private void SwapCharacters()
        {
            Vector2 tempPosition = _charactersInCombat[_characterPlacementCursorPosition].transform.position;
            _charactersInCombat[_characterPlacementCursorPosition].transform.position = _selectedCharacterForSwap.transform.position;
            _selectedCharacterForSwap.transform.position = tempPosition;
        }

        private void OpenAllCombatWindows()
        {
            _uiManager.OpenPlayerGlobalUi();
            _uiManager.OpenHelpWindow();
            _uiManager.OpenTimeline();
        }

        private void ComputeEnemyTurn()
        {
            if (_enemiesManager == null)
                _enemiesManager = new EnemiesManager(_enemiesInCombat);

            _enemiesManager.LockAbilities(AllBattlers);
            _uiManager.UpdateTimeline();
        }

        private void FollowStateChange(BattleState state)
        {
            switch(state)
            {
                case BattleState.PlayerMoveSelection:
                    if (_charactersInCombat.All(c => c.LockedInAbility != null))
                        ProcessBattle();
                    else
                        _uiManager.FeedMoveSelectionWindow(_turnManager.CurrentCharacter);
                    break;
            }
        }

        public void SelectTargetWithAbility(Ability ability)
        {
            UpdateState(BattleState.TargetSelection);

            List<BattlerBehaviour> targets = new List<BattlerBehaviour>();

            switch(ability.TargetEligibility)
            {
                case TargetEligibility.Ally:
                    targets.AddRange(_charactersInCombat);
                    break;
                case TargetEligibility.Enemy:
                    targets.AddRange(_enemiesInCombat);
                    break;
                default:
                    targets.AddRange(_enemiesInCombat);
                    break;
            }

            _targetManager.LoadTargets(targets, ability);
        }

        private void ProcessBattle()
        {
            _battleProcessor.ProcessBattle(_uiManager.GetTimelines());
        }
    }
}
