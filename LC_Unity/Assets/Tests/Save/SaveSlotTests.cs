using UnityEngine;
using UnityEngine.UI;
using NUnit.Framework;
using TMPro;
using Save;
using System.Collections.Generic;
using Core;
using GameProgression;
using Party;
using Actors;
using Inventory;
using Language;

namespace Testing.Save
{
    public class SaveSlotTests : TestFoundation
    {
        [SetUp]
        public void Setup()
        {
            PersistentDataHolder.Instance.Reset();
            PartyManager.Instance.Clear();
            GlobalStateMachine.Instance.CurrentMapId = -1;
        }

        private SaveSlot CreateDefaultSlot()
        {
            GameObject slotGo = ComponentCreator.CreateEmptyGameObject();
            SaveSlot slot = slotGo.AddComponent<SaveSlot>();
            slotGo.AddComponent<RectTransform>();

            GameObject blankSaveGo = ComponentCreator.CreateEmptyGameObject();
            blankSaveGo.transform.SetParent(slotGo.transform);

            GameObject saveWithDataGo = ComponentCreator.CreateEmptyGameObject();
            saveWithDataGo.transform.SetParent(slotGo.transform);

            TextMeshProUGUI inGameTime = ComponentCreator.CreateText();
            inGameTime.transform.SetParent(slotGo.transform);

            TextMeshProUGUI location = ComponentCreator.CreateText();
            location.transform.SetParent(slotGo.transform);

            Image characterImg1 = ComponentCreator.CreateImage();
            characterImg1.transform.SetParent(slotGo.transform);

            Image characterImg2 = ComponentCreator.CreateImage();
            characterImg2.transform.SetParent(slotGo.transform);

            _usedGameObjects.Add(slotGo);

            slot.SetComponents(blankSaveGo.transform,
                               saveWithDataGo.transform,
                               inGameTime,
                               location,
                               new Image[]
                               {
                                   characterImg1 , characterImg2
                               });
            return slot;
        }

        [Test]
        public void SlotCanBeInited()
        {
            Localizer localizer = ComponentCreator.CreateLocalizer("Save/french.csv", global::Language.Language.French);
            _usedGameObjects.Add(localizer.gameObject);

            SavedData data = new SavedData
            {
                PlayerPosition = new Vector2(),
                MapID = 1000,
                Gold = 500,
                InGameTimeSeconds = 10.0f,
                Party = new List<Character>(),
                Inventory = new List<InventoryItem>()
            };

            SaveSlot slot = CreateDefaultSlot();
            slot.Init(data);

            Assert.IsFalse(slot.BlankSave.gameObject.activeInHierarchy);
            Assert.IsTrue(slot.SaveWithData.gameObject.activeInHierarchy);
            Assert.AreEqual("Introduction", slot.Location.text);
            Assert.AreEqual("00:00:10", slot.InGameTime.text);

            foreach(Image img in slot.Characters)
                Assert.IsFalse(img.gameObject.activeInHierarchy);
        }
    }
}
