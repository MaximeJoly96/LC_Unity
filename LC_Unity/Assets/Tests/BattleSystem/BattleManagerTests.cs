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

            yield return null;
        }

        private BattleManager CreateBattleManager()
        {
            GameObject go = ComponentCreator.CreateEmptyGameObject();
            _usedGameObjects.Add(go);

            go.AddComponent<InputReceiver>();
            BattleManager battleManager = go.AddComponent<BattleManager>();

            battleManager.UiManager = CreateBattleUiManager();
            battleManager.Troops = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/BattleSystem/TestData/TestTroops.xml");

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
    }
}
