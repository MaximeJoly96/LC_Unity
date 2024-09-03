using NUnit.Framework;
using UnityEngine;
using Language;
using UnityEditor;

namespace Testing.Languages
{
    public class LocalizerTests
    {
        [Test]
        public void LoadLanguageTest()
        {
            GameObject localizer = new GameObject("Localizer");
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

