using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using TMPro;
using Language;
using UnityEditor;

namespace Testing.Language
{
    public class LocalizedTextTests
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
        public void UpdateTextTest()
        {
            Setup();

            GameObject go = new GameObject("Text");
            _usedGameObjects.Add(go);
            TMP_Text text = go.AddComponent<TextMeshProUGUI>();

            LocalizedText lt = go.AddComponent<LocalizedText>();
            lt.UpdateKey("key1");

            Assert.AreEqual("Valeur", text.text);

            lt.UpdateKey("key2");

            Assert.AreEqual("Autre valeur", text.text);
        }

        private void Setup()
        {
            GameObject go = new GameObject("Localizer");
            _usedGameObjects.Add(go);
            Localizer localzier = go.AddComponent<Localizer>();

            TextAsset[] files = new TextAsset[] { AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Language/french.csv") };
            localzier.LoadLanguage(global::Language.Language.French, files);
        }
    }
}
