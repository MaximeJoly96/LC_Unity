using UnityEngine;
using UnityEngine.Events;
using Party;
using BattleSystem.Data;
using BattleSystem.Model;
using Engine.SceneControl;
using System.Collections.Generic;
using Actors;

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
            LoadActionMenu();
            OpenHelpWindow();

            LoadBattlers();
            LoadCharacters();
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
                _battlersHolder.InstantiateBattler(battlers[i]);
            }
        }

        private void LoadCharacters()
        {
            List<Character> characters = PartyManager.Instance.GetParty();

            for(int i = 0; i < characters.Count; i++)
            {
                _charactersHolder.InstantiateBattler(new Battler(characters[i]));
            }
        }
    }
}
