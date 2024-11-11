using NUnit.Framework;
using UnityEngine;
using Engine.Map;
using Field;
using Language;
using UnityEditor;
using Core;
using TMPro;
using System.Collections.Generic;

namespace Testing.Engine.Map
{
    public class ChangeMapNameDisplayTests
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

        [Test]
        public void ChangeStatusOfMapNameDisplayTest()
        {
            ChangeMapNameDisplay changeEnabled = new ChangeMapNameDisplay { Enabled = true };
            ChangeMapNameDisplay changeDisabled = new ChangeMapNameDisplay { Enabled = false };

            GameObject go = new GameObject("MapNameDisplay");
            _usedGameObjects.Add(go);
            MapNameDisplay display = go.AddComponent<MapNameDisplay>();

            Assert.IsTrue(display.DisplayEnabled);

            changeDisabled.Run();

            Assert.IsFalse(display.DisplayEnabled);

            changeEnabled.Run();

            Assert.IsTrue(display.DisplayEnabled);
        }

        [Test]
        public void ShowNameTest()
        {
            GameObject localizer = new GameObject("Localizer");
            _usedGameObjects.Add(localizer);
            Localizer component = localizer.AddComponent<Localizer>();

            TextAsset[] files = new TextAsset[] { AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Engine/Map/french.csv") };
            component.LoadLanguage(global::Language.Language.French, files);

            GlobalStateMachine.Instance.CurrentMapId = 0;

            GameObject go = new GameObject("MapNameDisplay");
            _usedGameObjects.Add(go);
            MapNameDisplay display = go.AddComponent<MapNameDisplay>();
            TextMeshProUGUI text = go.AddComponent<TextMeshProUGUI>();

            display.DisplayEnabled = true;
            display.Show();

            Assert.AreEqual("Bastion de Haalmikah", text.text);

            GlobalStateMachine.Instance.CurrentMapId = 1;

            display.Show();

            Assert.AreEqual("Région de Haalmikah - Est", text.text);
        }
    }
}
