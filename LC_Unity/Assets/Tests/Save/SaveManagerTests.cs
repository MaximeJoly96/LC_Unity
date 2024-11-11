using UnityEngine;
using NUnit.Framework;
using Save;
using System.IO;
using Core;
using GameProgression;
using Party;
using Inputs;
using Language;
using TMPro;
using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.TestTools;
using System.Collections;

namespace Testing.Save
{
    public class SaveManagerTests
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

        [UnityTest]
        public IEnumerator SaveCanBeCreated()
        {
            CreateFrenchLocalizer();
            CreateInputController();
            CreateDefaultCanvas();

            yield return null;

            SaveManager.Instance.CreateSaveFile(0);
            SavedData data = SaveManager.Instance.Data;

            Assert.IsTrue(Vector2.Distance(data.PlayerPosition, Vector2.zero) < 0.01f);
            Assert.AreEqual(1000, data.MapID);
            Assert.IsTrue(Mathf.Abs(data.InGameTimeSeconds - 0.0f) < 0.01f);
            Assert.AreEqual(0, data.Party.Count);
            Assert.AreEqual(0, data.Gold);
            Assert.AreEqual(0, data.Inventory.Count);

            File.Delete(Application.persistentDataPath + "/save0.data");
        }

        [UnityTest]
        public IEnumerator SaveCanBeLoaded()
        {
            CreateFrenchLocalizer();
            CreateInputController();
            CreateDefaultCanvas();

            yield return null;

            SaveManager.Instance.CreateSaveFile(0);
            SavedData data = SaveManager.Instance.Data;

            yield return null;

            SaveManager.Instance.LoadSaveFile(0);

            Assert.IsTrue(Vector2.Distance(data.PlayerPosition, Vector2.zero) < 0.01f);
            Assert.AreEqual(1000, data.MapID);
            Assert.IsTrue(Mathf.Abs(data.InGameTimeSeconds - 0.0f) < 0.01f);
            Assert.AreEqual(0, data.Party.Count);
            Assert.AreEqual(0, data.Gold);
            Assert.AreEqual(0, data.Inventory.Count);

            File.Delete(Application.persistentDataPath + "/save0.data");
        }

        private SaveCanvas CreateDefaultCanvas()
        {
            GameObject go = new GameObject();
            SaveCanvas canvas = go.AddComponent<SaveCanvas>();

            GameObject tooltipGo = new GameObject();
            TextMeshProUGUI tooltip = tooltipGo.AddComponent<TextMeshProUGUI>();
            tooltipGo.transform.SetParent(go.transform);

            GameObject wrapperGo = new GameObject();
            wrapperGo.AddComponent<RectTransform>();
            wrapperGo.transform.SetParent(go.transform);

            GameObject scrollRectGo = new GameObject();
            ScrollRect scrollRect = scrollRectGo.AddComponent<ScrollRect>();
            scrollRect.content = wrapperGo.GetComponent<RectTransform>();
            scrollRectGo.transform.SetParent(go.transform);

            SaveSlot saveSlotPrefab = CreateDefaultSlot();

            canvas.SetComponents(tooltip, wrapperGo.transform, saveSlotPrefab, scrollRect);

            go.AddComponent<InputReceiver>();
            _usedGameObjects.Add(go);

            return canvas;
        }

        private InputController CreateInputController()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);
            return go.AddComponent<InputController>();
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
    }
}
