using NUnit.Framework;
using UnityEngine;
using BattleSystem;
using Engine.SceneControl;
using Movement;
using Save;
using Inputs;

namespace Testing.BattleSystem
{
    public class BattleLoaderTests : TestFoundation
    {
        [SetUp]
        public void Setup()
        {
            InputController inputCtrl = ComponentCreator.CreateInputController();
            _usedGameObjects.Add(inputCtrl.gameObject);
        }

        [Test]
        public void BattleDataCanBeLoaded()
        {
            PlayerController player = ComponentCreator.CreatePlayerController();
            _usedGameObjects.Add(player.gameObject);

            BattleProcessing processing = new BattleProcessing
            {
                TroopId = 3,
                CanEscape = true,
                DefeatAllowed = false,
                FromRandomEncounter = false
            };

            BattleLoader loader = new BattleLoader();
            loader.LoadBattle(processing);

            Assert.AreEqual(3, BattleDataHolder.Instance.BattleData.TroopId);
            Assert.AreEqual(true, BattleDataHolder.Instance.BattleData.CanEscape);
            Assert.AreEqual(false, BattleDataHolder.Instance.BattleData.DefeatAllowed);
            Assert.AreEqual(false, BattleDataHolder.Instance.BattleData.FromRandomEncounter);

            Assert.IsTrue(Mathf.Abs(SaveManager.Instance.Data.PlayerPosition.x - 0.0f) < 0.01f);
            Assert.IsTrue(Mathf.Abs(SaveManager.Instance.Data.PlayerPosition.y - 0.0f) < 0.01f);
        }
    }
}
