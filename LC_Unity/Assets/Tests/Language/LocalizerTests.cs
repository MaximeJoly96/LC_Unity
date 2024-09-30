using NUnit.Framework;
using UnityEngine;
using Language;
using UnityEditor;
using System.Collections.Generic;

namespace Testing.Languages
{
    public class LocalizerTests
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
        public void LoadLanguageTest()
        {
            GameObject localizer = new GameObject("Localizer");
            _usedGameObjects.Add(localizer);
            Localizer component = localizer.AddComponent<Localizer>();

            TextAsset file = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Language/french.csv");
            component.LoadLanguage(Language.Language.French, file);

            Assert.AreEqual("Valeur", component.GetString("key1"));
            Assert.AreEqual("Autre valeur", component.GetString("key2"));
            Assert.AreEqual("Dernière valeur", component.GetString("key3"));

            file = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Language/english.csv");
            component.LoadLanguage(Language.Language.English, file);

            Assert.AreEqual("Value", component.GetString("key1"));
            Assert.AreEqual("Other value", component.GetString("key2"));
            Assert.AreEqual("Last value", component.GetString("key3"));
        }
    }
}

