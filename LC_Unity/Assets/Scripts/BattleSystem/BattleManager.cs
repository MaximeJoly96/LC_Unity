using UnityEngine;
using UnityEngine.Events;
using Party;
using BattleSystem.Data;
using BattleSystem.Model;
using Engine.SceneControl;
using System.Collections.Generic;
using Actors;
using BattleSystem.UI;

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
        private List<BattlerBehaviour> _battlersInCombat;

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

            _battlersInCombat = new List<BattlerBehaviour>();

            LoadBattlers();
            LoadCharacters();

            InitTimeline();
            ShowInstructions();
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
                _battlersInCombat.Add(_battlersHolder.InstantiateBattler(battlers[i]));
            }
        }

        private void LoadCharacters()
        {
            List<Character> characters = PartyManager.Instance.GetParty();

            for(int i = 0; i < characters.Count; i++)
            {
                _battlersInCombat.Add(_charactersHolder.InstantiateBattler(new Battler(characters[i])));
            }
        }

        private void InitTimeline()
        {
            _uiManager.InitTimeline(_battlersInCombat);
        }

        private void ShowInstructions()
        {
            _uiManager.ShowInstructionsWindow();
        }
    }
}
