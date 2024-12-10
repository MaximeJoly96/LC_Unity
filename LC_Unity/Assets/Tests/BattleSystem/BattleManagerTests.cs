using NUnit.Framework;
using BattleSystem;
using Inputs;
using UnityEngine;
using Core;
using MusicAndSounds;
using System.Collections;
using UnityEngine.TestTools;
using Engine.SceneControl;
using UnityEditor;
using BattleSystem.Model;
using System.Collections.Generic;
using Actors;
using BattleSystem.Fields;

namespace Testing.BattleSystem
{
    public class BattleManagerTests : TestFoundation
    {
        private InputController _inputCtrl;
        private AudioPlayer _audioPlayer;

        [SetUp]
        public void Setup()
        {
            _inputCtrl = ComponentCreator.CreateInputController();
            _usedGameObjects.Add(_inputCtrl.gameObject);

            _audioPlayer = ComponentCreator.CreateAudioPlayer();
            _usedGameObjects.Add(_audioPlayer.gameObject);
        }

        [UnityTest]
        public IEnumerator BattleManagerCanBeInitialized()
        {
            BattleDataHolder.Instance.BattleData = new BattleProcessing
            {
                TroopId = 1,
                FromRandomEncounter = false,
                CanEscape = false,
                DefeatAllowed = false
            };

            BattleManager manager = CreateBattleManager();
            _usedGameObjects.Add(manager.gameObject);

            yield return null;
        }

        [UnityTest]
        public IEnumerator BattleStateCanBeUpdated()
        {
            BattleDataHolder.Instance.BattleData = new BattleProcessing
            {
                TroopId = 1,
                FromRandomEncounter = false,
                CanEscape = false,
                DefeatAllowed = false
            };

            BattleManager manager = CreateBattleManager();
            _usedGameObjects.Add(manager.gameObject);

            yield return null;

            int var = 0;
            manager.StateChangedEvent.AddListener((x) => var++);
            manager.UpdateState(BattleState.BattleStart);

            Assert.AreEqual(manager.CurrentState, BattleState.BattleStart);
            Assert.AreEqual(1, var);
        }

        private BattleManager CreateBattleManager()
        {
            GameObject go = ComponentCreator.CreateEmptyGameObject();
            _usedGameObjects.Add(go);

            go.AddComponent<InputReceiver>();
            BattleManager battleManager = go.AddComponent<BattleManager>();

            battleManager.UiManager = CreateBattleUiManager();
            _usedGameObjects.Add(battleManager.UiManager.gameObject);
            battleManager.Troops = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/BattleSystem/TestData/TestTroops.xml");
            battleManager.Enemies = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/BattleSystem/TestData/TestEnemies.xml");

            battleManager.CharactersHolder = CreateBattlersHolder();
            _usedGameObjects.Add(battleManager.CharactersHolder.gameObject);
            battleManager.BattlersHolder = CreateBattlersHolder();
            _usedGameObjects.Add(battleManager.BattlersHolder.gameObject);

            Character c1 = ComponentCreator.CreateDummyCharacter(0);
            Character c2 = ComponentCreator.CreateDummyCharacter(1);
            Character c3 = ComponentCreator.CreateDummyCharacter(2);

            Character c4 = ComponentCreator.CreateDummyCharacter(3);
            Character c5 = ComponentCreator.CreateDummyCharacter(4);
            Character c6 = ComponentCreator.CreateDummyCharacter(5);

            List<BattlerBehaviour> battlers = new List<BattlerBehaviour>
            {
                ComponentCreator.CreateBattlerBehaviour(),
                ComponentCreator.CreateBattlerBehaviour(),
                ComponentCreator.CreateBattlerBehaviour()
            };

            battlers[0].Feed(new Battler(c1));
            battlers[1].Feed(new Battler(c2));
            battlers[2].Feed(new Battler(c3));
            _usedGameObjects.Add(battlers[0].gameObject);
            _usedGameObjects.Add(battlers[1].gameObject);
            _usedGameObjects.Add(battlers[2].gameObject);

            battleManager.BattlersHolder.Feed(battlers);

            List<BattlerBehaviour> characters = new List<BattlerBehaviour>
            {
                ComponentCreator.CreateBattlerBehaviour(),
                ComponentCreator.CreateBattlerBehaviour(),
                ComponentCreator.CreateBattlerBehaviour()
            };

            characters[0].Feed(new Battler(c4));
            characters[1].Feed(new Battler(c5));
            characters[2].Feed(new Battler(c6));
            _usedGameObjects.Add(characters[0].gameObject);
            _usedGameObjects.Add(characters[1].gameObject);
            _usedGameObjects.Add(characters[2].gameObject);

            battleManager.CharactersHolder.Feed(characters);

            Battlefield[] fields = new Battlefield[]
            {
                ComponentCreator.CreateBattlefield(0),
                ComponentCreator.CreateBattlefield(1),
                ComponentCreator.CreateBattlefield(2)
            };

            foreach (Battlefield bf in fields)
            {
                _usedGameObjects.Add(bf.gameObject);
            }

            GameObject holderGo = ComponentCreator.CreateEmptyGameObject();
            _usedGameObjects.Add(holderGo);
            BattlefieldsHolder holder = holderGo.AddComponent<BattlefieldsHolder>();
            holder.FeedFields(fields);

            battleManager.BattlefieldsHolder = holder;

            return battleManager;
        }

        private BattleUiManager CreateBattleUiManager()
        {
            BattleUiManager uiManager = ComponentCreator.CreateBattleUiManager();
            _usedGameObjects.Add(uiManager.HelpWindow.gameObject);
            _usedGameObjects.Add(uiManager.AttackLabelWindow.gameObject);
            _usedGameObjects.Add(uiManager.gameObject);

            return uiManager;
        }

        private BattlersHolder CreateBattlersHolder()
        {
            GameObject go = ComponentCreator.CreateEmptyGameObject();
            _usedGameObjects.Add(go.gameObject);
            return go.AddComponent<BattlersHolder>();
        }
    }
}
