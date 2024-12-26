using NUnit.Framework;
using UnityEngine;
using Language;
using UnityEditor;
using System.Collections.Generic;

namespace Testing.Language
{
    public class LocalizerTests : TestFoundation
    {
        [Test]
        public void LoadLanguageTest()
        {
            GameObject localizer = new GameObject("Localizer");
            _usedGameObjects.Add(localizer);
            Localizer component = localizer.AddComponent<Localizer>();

            TextAsset[] files = new TextAsset[] { AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Language/french.csv") };
            component.LoadLanguage(global::Language.Language.French, files);

            Assert.AreEqual("Valeur", component.GetString("key1"));
            Assert.AreEqual("Autre valeur", component.GetString("key2"));
            Assert.AreEqual("Dernière valeur", component.GetString("key3"));

            files = new TextAsset[] { AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Language/english.csv") };
            component.LoadLanguage(global::Language.Language.English, files);

            Assert.AreEqual("Value", component.GetString("key1"));
            Assert.AreEqual("Other value", component.GetString("key2"));
            Assert.AreEqual("Last value", component.GetString("key3"));
        }
    }
}

