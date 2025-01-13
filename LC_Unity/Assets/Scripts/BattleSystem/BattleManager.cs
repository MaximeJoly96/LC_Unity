using UnityEngine;
using UnityEngine.Events;
using Party;
using BattleSystem.Data;
using BattleSystem.Model;
using Engine.SceneControl;
using System.Collections.Generic;
using Actors;
using BattleSystem.UI;
using Abilities;
using System.Linq;
using Utils;
using UnityEngine.SceneManagement;
using MusicAndSounds;
using System.Collections;
using Core;
using BattleSystem.Fields;

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
        BattleVictory,
        BattleDefeat,
    }

    public class BattleManager : MonoBehaviour
    {
        private UnityEvent<BattleState> _stateChangedEvent;
        private List<BattlerBehaviour> _enemiesInCombat;
        private List<BattlerBehaviour> _charactersInCombat;

        private int _characterPlacementCursorPosition;
        private PlacementCursor _firstPlacementCursor;
        private PlacementCursor _secondPlacementCursor;
        private BattlerBehaviour _selectedCharacterForSwap;
        private EnemiesManager _enemiesManager;
        private List<BattlerBehaviour> _allBattlers;
        private TurnManager _turnManager;
        private InputReceiver _inputReceiver;

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
        [SerializeField]
        private BattlefieldsHolder _battlefieldsHolder;

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

        #region Properties
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

        public BattleUiManager UiManager { get { return _uiManager; } set { _uiManager = value; } }
        public TextAsset Troops { get { return _troops; } set { _troops = value; } }
        public TextAsset Enemies { get { return _enemies; } set { _enemies = value; } }
        public BattlersHolder CharactersHolder { get { return _charactersHolder; } set { _charactersHolder = value; } }
        public BattlersHolder BattlersHolder { get { return _battlersHolder; } set { _battlersHolder = value; } }
        public BattlefieldsHolder BattlefieldsHolder { get  { return _battlefieldsHolder; } set { _battlefieldsHolder = value; } }
        public TargetManager TargetManager { get { return _targetManager; } }
        #endregion

        private void Start()
        {
            BattleDataHolder.Instance.Loading = false;
            BindInputs();

            StopAllAudio();
            StartCombatBgm();

            _enemiesInCombat = new List<BattlerBehaviour>();
            _charactersInCombat = new List<BattlerBehaviour>();

            _uiManager.VictoryConcluded.AddListener(Victory);
            _uiManager.DefeatConcluded.AddListener(Defeat);

            StateChangedEvent.AddListener(FollowStateChange);

            UpdateState(BattleState.Loading);
            LoadPartyData();

            TroopParser troopParser = new TroopParser();
            BattleProcessing battle = BattleDataHolder.Instance.BattleData;
            Troop troop = troopParser.ParseTroop(_troops, battle.TroopId);

            LoadBattlers(troop);
            LoadCharacters(troop);
            LoadBattlefield(troop);

            UpdateBattlersOrientation();

            UpdateState(BattleState.Loaded);
            
            InitTimeline();

            _uiManager.HideAllWindows();

            ShowInstructions();
            if (_charactersInCombat.Count > 1)
                UpdateState(BattleState.PlacingCharacters);
            else
                UpdateState(BattleState.BattleStart);
        }

        #region Inputs
        private void BindInputs()
        {
            _inputReceiver = GetComponent<InputReceiver>();
            _inputReceiver.OnCancel.AddListener(CancelButtonPressed);
            _inputReceiver.OnSelect.AddListener(SelectButtonPressed);
            _inputReceiver.OnOpenMenu.AddListener(StartButtonPressed);
            _inputReceiver.OnMoveLeft.AddListener(MoveLeftPressed);
            _inputReceiver.OnMoveRight.AddListener(MoveRightPressed);
            _inputReceiver.OnMoveDown.AddListener(MoveDownPressed);
            _inputReceiver.OnMoveUp.AddListener(MoveUpPressed);
        }

        protected void Update()
        {
            if(CurrentState == BattleState.BattleProcess)
            {
                if(_charactersInCombat.All(c => c.FinishedAction) && _enemiesInCombat.All(c => c.FinishedAction))
                {
                    _charactersInCombat.ForEach(c => c.ResetTurn());
                    _enemiesInCombat.ForEach(e => e.ResetTurn());
                    UpdateState(BattleState.ComputingEnemyTurn);
                }

                CheckForDeadParticipants();
            }
        }

        private void SelectButtonPressed()
        {
            switch (CurrentState)
            {
                case BattleState.PlacingCharacters:
                    CommonSounds.OptionSelected();
                    _cursorsManager.StopCurrentCursor();
                    _selectedCharacterForSwap = _charactersInCombat[_characterPlacementCursorPosition];
                    _cursorsManager.CreateCursor(_selectedCharacterForSwap.transform.position);
                    UpdateState(BattleState.SwappingCharacters);
                    UpdateInstructions();
                    break;
                case BattleState.SwappingCharacters:
                    CommonSounds.OptionSelected();
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
                    CommonSounds.OptionSelected();
                    _targetManager.ConfirmTarget(_turnManager.CurrentCharacter);
                    _targetManager.Clear();
                    _uiManager.CloseTargetInfo();
                    _uiManager.UpdateTimeline();
                    _uiManager.UpdateAction(_turnManager.CurrentCharacter);
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
                    CommonSounds.ActionCancelled();
                    _cursorsManager.StopCurrentCursor();
                    _cursorsManager.ClearCursors();
                    _cursorsManager.CreateCursor(_charactersInCombat[0].transform.position);
                    UpdateState(BattleState.PlacingCharacters);
                    UpdateInstructions();
                    break;
                case BattleState.TargetSelection:
                    CommonSounds.ActionCancelled();
                    _targetManager.Clear();
                    _uiManager.CloseTargetInfo();
                    UpdateState(BattleState.PlayerMoveSelection);
                    break;
                case BattleState.PlayerMoveSelection:
                    _uiManager.CancelSelection();
                    break;
            }
        }

        private void MoveLeftPressed()
        {
            switch(CurrentState)
            {
                case BattleState.PlacingCharacters:
                case BattleState.SwappingCharacters:
                    CommonSounds.CursorMoved();
                    _characterPlacementCursorPosition = _characterPlacementCursorPosition == 0 ?
                                                        _charactersInCombat.Count - 1 : --_characterPlacementCursorPosition;
                    _cursorsManager.UpdateCurrentCursor(_charactersInCombat[_characterPlacementCursorPosition].transform.position);
                    break;
                case BattleState.TargetSelection:
                    CommonSounds.CursorMoved();
                    _targetManager.PreviousTarget();
                    _uiManager.ShowTargetInfo(_targetManager.CurrentlySelectedTarget);
                    break;
            }
        }

        private void MoveRightPressed()
        {
            switch (CurrentState)
            {
                case BattleState.PlacingCharacters:
                case BattleState.SwappingCharacters:
                    CommonSounds.CursorMoved();
                    _characterPlacementCursorPosition = _characterPlacementCursorPosition == _charactersInCombat.Count - 1 ?
                                                        0 : ++_characterPlacementCursorPosition;
                    _cursorsManager.UpdateCurrentCursor(_charactersInCombat[_characterPlacementCursorPosition].transform.position);
                    break;
                case BattleState.TargetSelection:
                    CommonSounds.CursorMoved();
                    _targetManager.NextTarget();
                    _uiManager.ShowTargetInfo(_targetManager.CurrentlySelectedTarget);
                    break;
            }
        }

        private void MoveUpPressed()
        {
            switch(CurrentState)
            {
                case BattleState.PlayerMoveSelection:
                    CommonSounds.CursorMoved();
                    _uiManager.UpPressedOnMoveSelection();
                    break;
            }
        }

        private void MoveDownPressed()
        {
            switch (CurrentState)
            {
                case BattleState.PlayerMoveSelection:
                    CommonSounds.CursorMoved();
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

        public void UpdateBattlersOrientation()
        {
            float minX = float.MaxValue;
            float minY = float.MaxValue;
            float maxX = float.MinValue;
            float maxY = float.MinValue;

            for(int i = 0; i < AllBattlers.Count; i++)
            {
                if (AllBattlers[i].transform.position.x < minX)
                    minX = AllBattlers[i].transform.position.x;

                if (AllBattlers[i].transform.position.y < minY)
                    minY = AllBattlers[i].transform.position.y;

                if (AllBattlers[i].transform.position.x > maxX)
                    maxX = AllBattlers[i].transform.position.y;

                if (AllBattlers[i].transform.position.y > maxY)
                    maxY = AllBattlers[i].transform.position.y;
            }

            Vector3 center = new Vector2(Mathf.Lerp(minX, maxX, 0.5f), Mathf.Lerp(minY, maxY, 0.5f));

            for (int i = 0; i < AllBattlers.Count; i++)
            {
                Vector3 lookAt = (center - AllBattlers[i].transform.position).normalized;
                AllBattlers[i].GetComponent<Animator>().SetFloat("X", lookAt.x);
                AllBattlers[i].GetComponent<Animator>().SetFloat("Y", lookAt.y);
            }
        }

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

        private void CloseAllCombatWindows()
        {
            _uiManager.CloseHelpWindow();
            _uiManager.ClosePlayerGlobalUi();
            _uiManager.CloseTimeline();
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
                    _uiManager.UpdateTimeline();
                    ProcessBattle();
                    break;
                case BattleState.BattleVictory:
                    StopAllAudio();
                    CloseAllCombatWindows();
                    _uiManager.ShowVictoryTag();
                    CommonSounds.Victory();
                    break;
                case BattleState.BattleDefeat:
                    StopAllAudio();
                    CloseAllCombatWindows();
                    _uiManager.ShowDefeatTag();
                    CommonSounds.Defeat();
                    break;
            }
        }

        private void BattleStartTagClosed()
        {
            UpdateState(BattleState.ComputingEnemyTurn);
        }

        public void SelectTargetWithAbility(Ability ability, BattlerBehaviour caster)
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
                case TargetEligibility.Any:
                    targets.AddRange(_charactersInCombat);
                    targets.AddRange(_enemiesInCombat);
                    break;
                case TargetEligibility.AnyExceptSelf:
                    targets.AddRange(_charactersInCombat);
                    targets.AddRange(_enemiesInCombat);
                    targets.Remove(targets.FirstOrDefault(t => t.BattlerData.Character.Name == caster.BattlerData.Character.Name));
                    break;
                case TargetEligibility.AllEnemies:
                    targets.AddRange(_enemiesInCombat);
                    break;
                case TargetEligibility.AllAllies:
                    targets.AddRange(_charactersInCombat);
                    break;
                case TargetEligibility.All:
                    targets.AddRange(_charactersInCombat);
                    targets.AddRange(_enemiesInCombat);
                    break;
                case TargetEligibility.Self:
                    targets.Add(caster);
                    break;
                default:
                    targets.AddRange(_enemiesInCombat);
                    break;
            }

            _targetManager.LoadTargets(targets, ability);
            _uiManager.ShowTargetInfo(_targetManager.CurrentlySelectedTarget);
        }

        private void ProcessBattle()
        {
            _battleProcessor.ProcessBattle(_uiManager.GetTimelines());
        }

        private void CheckForDeadParticipants()
        {
            List<BattlerBehaviour> deadEnemies = _enemiesInCombat.Where(x => x.IsDead).ToList();
            List<BattlerBehaviour> deadAllies = _charactersInCombat.Where(x => x.IsDead).ToList();

            for(int i = 0; i < deadEnemies.Count; i++)
                _enemiesInCombat.Remove(deadEnemies[i]);

            for(int i = 0; i < deadAllies.Count; i++)
                _charactersInCombat.Remove(deadAllies[i]);

            if (_charactersInCombat.Count == 0)
                UpdateState(BattleState.BattleDefeat);
            else if (_enemiesInCombat.Count == 0)
                UpdateState(BattleState.BattleVictory);
        }

        public void Victory()
        {
            SceneManager.LoadScene("Field");
        }

        public void Defeat()
        {
            StartCoroutine(DoDefeat());
        }

        private IEnumerator DoDefeat()
        {
            yield return new WaitForSeconds(10.0f);
            SceneManager.LoadScene("TitleScreen");
        }

        private void StopAllAudio()
        {
            FindObjectOfType<AudioPlayer>().StopAllAudio();
        }

        private void StartCombatBgm()
        {
            FindObjectOfType<AudioPlayer>().PlayBgm(new Engine.MusicAndSounds.PlayBgm
            {
                Name = "battleTheme1",
                Volume = 0.75f,
                Pitch = 1.0f
            });
        }

        private void LoadBattlefield(Troop troop)
        {
            Battlefield field = _battlefieldsHolder.GetField(troop.BattlefieldId);

            Instantiate(field);
        }
    }
}
