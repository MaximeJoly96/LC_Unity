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

namespace BattleSystem
{
    public enum BattleState
    {
        Loading,
        Loaded,
        PlacingCharacters,
        SwappingCharacters,
        BattleStart
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
            FindObjectOfType<InputController>().ButtonClicked.AddListener(ReceiveInput);

            _selectionDelay = 0.0f;
            _delayOn = false;
            _enemiesInCombat = new List<BattlerBehaviour>();
            _charactersInCombat = new List<BattlerBehaviour>();

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

        }

        private void MoveDownPressed()
        {

        }

        private void StartButtonPressed()
        {
            ClearPlacementCursors();
            UpdateState(BattleState.BattleStart);
            _uiManager.HideInstructionsWindow();
            _uiManager.BattleStartTagClosed.RemoveAllListeners();
            _uiManager.BattleStartTagClosed.AddListener(OpenAllCombatWindows);
        }
        #endregion

        public void UpdateState(BattleState state)
        {
            CurrentState = state;
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
                _enemiesInCombat.Add(_battlersHolder.InstantiateBattler(battlers[i]));
            }
        }

        private void LoadCharacters()
        {
            List<Character> characters = PartyManager.Instance.GetParty();

            for(int i = 0; i < characters.Count; i++)
            {
                _charactersInCombat.Add(_charactersHolder.InstantiateBattler(new Battler(characters[i])));
            }
        }

        private void InitTimeline()
        {
            List<BattlerBehaviour> allBattlers = new List<BattlerBehaviour>();
            allBattlers.AddRange(_charactersInCombat);
            allBattlers.AddRange(_enemiesInCombat);
            _uiManager.InitTimeline(allBattlers);
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
    }
}
