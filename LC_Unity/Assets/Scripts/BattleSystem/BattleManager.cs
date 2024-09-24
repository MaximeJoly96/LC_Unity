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
using Utils;

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
        TargetSelection,
        BattleProcess,
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
        private TextAsset _troops;
        [SerializeField]
        private TextAsset _enemies;
        [SerializeField]
        private TargetManager _targetManager;
        [SerializeField]
        private BattleProcessor _battleProcessor;
        [SerializeField]
        private CursorsManager _cursorsManager;

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

        public List<BattlerBehaviour> CharactersInCombat
        {
            get { return _charactersInCombat; }
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

            TroopParser troopParser = new TroopParser();
            BattleProcessing battle = BattleDataHolder.Instance.BattleData;
            Troop troop = troopParser.ParseTroop(_troops, battle.TroopId);

            LoadBattlers(troop);
            LoadCharacters(troop);

            UpdateState(BattleState.Loaded);
            
            InitTimeline();

            _uiManager.HideAllWindows();

            ShowInstructions();
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
                        CommonSounds.ActionCancelled();
                        CancelButtonPressed();
                        break;
                    case InputAction.Select:
                        CommonSounds.OptionSelected();
                        SelectButtonPressed();
                        break;
                    case InputAction.MoveLeft:
                        CommonSounds.CursorMoved();
                        MoveLeftPressed();
                        break;
                    case InputAction.MoveRight:
                        CommonSounds.CursorMoved();
                        MoveRightPressed();
                        break;
                    case InputAction.MoveUp:
                        CommonSounds.CursorMoved();
                        MoveUpPressed();
                        break;
                    case InputAction.MoveDown:
                        CommonSounds.CursorMoved();
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

            if(CurrentState == BattleState.BattleProcess)
            {
                if(_charactersInCombat.All(c => c.FinishedAction) && _enemiesInCombat.All(c => c.FinishedAction))
                {
                    _charactersInCombat.ForEach(c => c.ResetTurn());
                    _enemiesInCombat.ForEach(e => e.ResetTurn());
                    UpdateState(BattleState.ComputingEnemyTurn);
                }
            }
        }

        private void SelectButtonPressed()
        {
            switch (CurrentState)
            {
                case BattleState.PlacingCharacters:
                    _cursorsManager.StopCurrentCursor();
                    _selectedCharacterForSwap = _charactersInCombat[_characterPlacementCursorPosition];
                    _cursorsManager.CreateCursor(_selectedCharacterForSwap.transform.position);
                    UpdateState(BattleState.SwappingCharacters);
                    UpdateInstructions();
                    break;
                case BattleState.SwappingCharacters:
                    _cursorsManager.StopCurrentCursor();
                    _cursorsManager.ClearCursors();
                    SwapCharacters();
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
                    _cursorsManager.StopCurrentCursor();
                    _cursorsManager.ClearCursors();
                    _cursorsManager.CreateCursor(_charactersInCombat[0].transform.position);
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
                case BattleState.SwappingCharacters:
                    _characterPlacementCursorPosition = _characterPlacementCursorPosition == 0 ?
                                                        _charactersInCombat.Count - 1 : --_characterPlacementCursorPosition;
                    _cursorsManager.UpdateCurrentCursor(_charactersInCombat[_characterPlacementCursorPosition].transform.position);
                    break;
                case BattleState.TargetSelection:
                    _targetManager.PreviousTarget();
                    break;
            }
        }

        private void MoveRightPressed()
        {
            switch (CurrentState)
            {
                case BattleState.PlacingCharacters:
                case BattleState.SwappingCharacters:
                    _characterPlacementCursorPosition = _characterPlacementCursorPosition == _charactersInCombat.Count - 1 ?
                                                        0 : ++_characterPlacementCursorPosition;
                    _cursorsManager.UpdateCurrentCursor(_charactersInCombat[_characterPlacementCursorPosition].transform.position);
                    break;
                case BattleState.TargetSelection:
                    _targetManager.NextTarget();
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
            _cursorsManager.ClearCursors();
            UpdateState(BattleState.BattleStart);
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

        private void LoadBattlers(Troop troopData)
        {
            EnemyParser enemyParser = new EnemyParser();
            
            List<Battler> battlers = new List<Battler>();

            for(int i = 0; i < troopData.Members.Count; i++)
            {
                Battler b = enemyParser.ParseEnemy(_enemies, troopData.Members[i].Id);
                b.InitialPosition = new Vector3(troopData.Members[i].X, troopData.Members[i].Y);
                battlers.Add(b);
            }

            for(int i = 0; i < battlers.Count; i++)
            {
                BattlerBehaviour behaviour = _battlersHolder.InstantiateBattler(battlers[i]);
                behaviour.IsEnemy = true;
                _enemiesInCombat.Add(behaviour);
            }
        }

        private void LoadCharacters(Troop troopData)
        {
            BattleProcessing battle = BattleDataHolder.Instance.BattleData;
            List<Character> characters = PartyManager.Instance.GetParty();

            for(int i = 0; i < characters.Count; i++)
            {
                Battler b = new Battler(characters[i]);
                b.InitialPosition = new Vector3(troopData.PlayerSpots[i].X, troopData.PlayerSpots[i].Y);
                BattlerBehaviour behaviour = _charactersHolder.InstantiateBattler(b);
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

        private void SwapCharacters()
        {
            Vector2 tempPosition = _charactersInCombat[_characterPlacementCursorPosition].transform.position;
            _charactersInCombat[_characterPlacementCursorPosition].transform.position = _selectedCharacterForSwap.transform.position;
            _selectedCharacterForSwap.transform.position = tempPosition;
            _characterPlacementCursorPosition = 0;
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
                    OpenAllCombatWindows();
                    if (_charactersInCombat.All(c => c.LockedInAbility != null))
                        UpdateState(BattleState.BattleProcess);
                    else
                        _uiManager.FeedMoveSelectionWindow(_turnManager.CurrentCharacter);
                    break;
                case BattleState.PlacingCharacters:
                    _cursorsManager.CreateCursor(_charactersInCombat[0].transform.position);
                    break;
                case BattleState.BattleStart:
                    _uiManager.HideInstructionsWindow();
                    _uiManager.ShowBattleStartTag();
                    _uiManager.BattleStartTagClosed.RemoveAllListeners();
                    _uiManager.BattleStartTagClosed.AddListener(BattleStartTagClosed);
                    break;
                case BattleState.ComputingEnemyTurn:
                    _turnManager = new TurnManager(_charactersInCombat);
                    ComputeEnemyTurn();
                    UpdateState(BattleState.PlayerMoveSelection);
                    break;
                case BattleState.BattleProcess:
                    _uiManager.HideMoveSelection();
                    _uiManager.CloseHelpWindow();
                    ProcessBattle();
                    break;
            }
        }

        private void BattleStartTagClosed()
        {
            UpdateState(BattleState.ComputingEnemyTurn);
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
