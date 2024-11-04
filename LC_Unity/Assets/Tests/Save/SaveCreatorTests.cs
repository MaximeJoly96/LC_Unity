using NUnit.Framework;
using Save;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Core;
using UnityEngine.TestTools;
using Timing;
using System.Collections;
using System.Globalization;
using GameProgression;
using Party;
using Core.Model;
using Utils;
using Actors;
using Inventory;
using Engine.Party;
using Engine.GameProgression;

namespace Testing.Save
{
    public class SaveCreatorTests
    {
        private List<GameObject> _usedGameObjects;

        [TearDown]
        public void TearDown()
        {
            for (int i = 0; i < _usedGameObjects.Count; i++)
            {
                GameObject.Destroy(_usedGameObjects[i]);
            }
        }

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            _usedGameObjects = new List<GameObject>();
        }

        [SetUp]
        public void Setup()
        {
            PersistentDataHolder.Instance.Reset();
            PartyManager.Instance.Clear();
            GlobalStateMachine.Instance.CurrentMapId = -1;
        }

        [Test]
        public void BlankSaveDataCanBeCreated()
        {
            SaveCreator creator = new SaveCreator();
            Dictionary<string, string> save = creator.CreateSaveData();

            Assert.AreEqual(5, save.Count);
            Assert.AreEqual("0", save["positionX"]);
            Assert.AreEqual("0", save["positionY"]);
            Assert.AreEqual("1000", save["mapId"]);
            Assert.AreEqual("0", save["inGameTime"]);
            Assert.AreEqual("0", save["gold"]);
        }

        [Test]
        public void BlankSaveDataCanBeSavedOnDisk()
        {
            SaveCreator creator = new SaveCreator();
            Dictionary<string, string> save = creator.CreateSaveData();

            string path = Application.persistentDataPath + "/test0.data";
            creator.WriteSaveDataToDisk(path, save);

            string content;

            using(StreamReader sr = new StreamReader(path))
            {
                content = sr.ReadToEnd();
            }

            string[] splitContent = content.Split("\r\n");

            Assert.AreEqual("positionX=0", splitContent[0]);
            Assert.AreEqual("positionY=0", splitContent[1]);
            Assert.AreEqual("mapId=1000", splitContent[2]);
            Assert.AreEqual("inGameTime=0", splitContent[3]);
            Assert.AreEqual("gold=0", splitContent[4]);

            File.Delete(path);
        }

        [Test]
        public void SaveDataCanBeCreatedAfterMapTransition()
        {
            GlobalStateMachine.Instance.CurrentMapId = 2500;

            SaveCreator creator = new SaveCreator();
            Dictionary<string, string> save = creator.CreateSaveData();

            Assert.AreEqual("2500", save["mapId"]);
        }

        [UnityTest]
        public IEnumerator InGameTimerCanBeSaved()
        {
            GameObject goTimer = new GameObject();
            _usedGameObjects.Add(goTimer);

            GlobalTimer timer = goTimer.AddComponent<GlobalTimer>();

            yield return null;

            timer.Running = true;

            yield return new WaitForSeconds(0.2f);

            SaveCreator creator = new SaveCreator();
            Dictionary<string, string> save = creator.CreateSaveData();

            float timerValue = float.Parse(save["inGameTime"], CultureInfo.InvariantCulture);

            Assert.IsTrue(Mathf.Abs(timerValue - timer.InGameTimeSeconds) < 0.01f);
        }

        [Test]
        public void CharactersDataCanBeSaved()
        {
            Character character = new Character(new ElementIdentifier(0, "Louga", ""),
                                                new QuadraticFunction(450, 250, 10),
                                                new StatScalingFunction(22, 1.2f, 256),
                                                new StatScalingFunction(6, 0.8f, 25),
                                                new StatScalingFunction(0, 1.0f, 100),
                                                new StatScalingFunction(1.5f, 1.2f, 8),
                                                new StatScalingFunction(1.4f, 1.1f, 7),
                                                new StatScalingFunction(0.9f, 1.2f, 3),
                                                new StatScalingFunction(1.2f, 1.2f, 8),
                                                new StatScalingFunction(1.7f, 0.975f, 5),
                                                new StatScalingFunction(4, 0.85f, 6));

            PartyManager.Instance.GetParty().Add(character);

            SaveCreator creator = new SaveCreator();
            Dictionary<string, string> save = creator.CreateSaveData();

            Assert.AreEqual("10,-1,-1,-1,-1,-1", save["character0"]);

            character.GiveExp(300);

            save = creator.CreateSaveData();
            Assert.AreEqual("310,-1,-1,-1,-1,-1", save["character0"]);

            character.ChangeEquipment(new BaseItem(new ElementIdentifier(1000, "sword", "swordDesc"), 0, 150, ItemCategory.Weapon));

            save = creator.CreateSaveData();
            Assert.AreEqual("310,-1,-1,1000,-1,-1", save["character0"]);
        }

        [Test]
        public void InventoryDataCanBeSaved()
        {
            BaseItem item1 = new BaseItem(new ElementIdentifier(150, "item1", "desc"), 2, 150, ItemCategory.Consumable);
            BaseItem item2 = new BaseItem(new ElementIdentifier(228, "item2", "desc"), 2, 148, ItemCategory.KeyItem);

            PartyManager.Instance.Inventory.Add(new InventoryItem(item1));
            PartyManager.Instance.Inventory.Add(new InventoryItem(item2));

            PartyManager.Instance.ChangeItems(new ChangeItems
            {
                Id = 150,
                Quantity = 7
            });

            PartyManager.Instance.ChangeItems(new ChangeItems
            {
                Id = 228,
                Quantity = 56
            });

            SaveCreator creator = new SaveCreator();
            Dictionary<string, string> save = creator.CreateSaveData();

            Assert.AreEqual("7", save["item150"]);
            Assert.AreEqual("56", save["item228"]);
        }

        [Test]
        public void PersistentDataCanBeSaved()
        {
            PersistentDataHolder.Instance.StoreData("int1", 5);
            PersistentDataHolder.Instance.StoreData("float1", 4.5f);
            PersistentDataHolder.Instance.StoreData("bool1", true);

            SaveCreator creator = new SaveCreator();
            Dictionary<string, string> save = creator.CreateSaveData();

            Assert.AreEqual("5", save["persistData-i-int1"]);
            Assert.AreEqual("4.5", save["persistData-f-float1"]);
            Assert.AreEqual("true", save["persistData-b-bool1"].ToLower());
        }

        [UnityTest]
        public IEnumerator AdditionalTimersCanBeSaved()
        {
            GameObject go = new GameObject();
            TimersManager timersManager = go.AddComponent<TimersManager>();
            _usedGameObjects.Add(go);

            timersManager.AddTimer(new ControlTimer { Action = ControlTimer.TimerAction.Start, Key = "Timer1", Duration = 10 });
            timersManager.StartTimer("Timer1");

            yield return new WaitForSeconds(0.5f);

            SaveCreator creator = new SaveCreator();
            Dictionary<string, string> save = creator.CreateSaveData();

            Assert.AreEqual(timersManager.GetTimer("Timer1").CurrentTime.ToString(CultureInfo.InvariantCulture) + "/10", save["timer-Timer1"]);
        }
    }
}
