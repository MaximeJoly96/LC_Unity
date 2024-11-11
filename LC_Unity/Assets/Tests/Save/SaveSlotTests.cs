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
using UnityEditor;

namespace Testing.Save
{
    public class SaveSlotTests
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

        private Localizer CreateFrenchLocalizer()
        {
            GameObject localizer = new GameObject("Localizer");
            _usedGameObjects.Add(localizer);
            Localizer component = localizer.AddComponent<Localizer>();

            TextAsset[] files = new TextAsset[] { AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Save/french.csv") };
            component.LoadLanguage(global::Language.Language.French, files);

            return component;
        }

        private SaveSlot CreateDefaultSlot()
        {
            GameObject slotGo = new GameObject();
            SaveSlot slot = slotGo.AddComponent<SaveSlot>();
            slotGo.AddComponent<RectTransform>();

            GameObject blankSaveGo = new GameObject();
            blankSaveGo.transform.SetParent(slotGo.transform);

            GameObject saveWithDataGo = new GameObject();
            saveWithDataGo.transform.SetParent(slotGo.transform);

            GameObject inGameTimeGo = new GameObject();
            TextMeshProUGUI inGameTime = inGameTimeGo.AddComponent<TextMeshProUGUI>();
            inGameTimeGo.transform.SetParent(slotGo.transform);

            GameObject locationGo = new GameObject();
            TextMeshProUGUI location = locationGo.AddComponent<TextMeshProUGUI>();
            locationGo.transform.SetParent(slotGo.transform);

            GameObject characterImgGo1 = new GameObject();
            Image characterImg1 = characterImgGo1.AddComponent<Image>();
            characterImgGo1.transform.SetParent(slotGo.transform);

            GameObject characterImgGo2 = new GameObject();
            Image characterImg2 = characterImgGo2.AddComponent<Image>();
            characterImgGo2.transform.SetParent(slotGo.transform);

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
            CreateFrenchLocalizer();

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
