using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using TMPro;
using Language;
using UnityEditor;

namespace Testing.Languages
{
    public class LocalizedTextTests
    {
        [Test]
        public void UpdateTextTest()
        {
            Setup();

            GameObject go = new GameObject("Text");
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
            Localizer localzier = go.AddComponent<Localizer>();

            TextAsset file = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Language/french.csv");
            localzier.LoadLanguage(Language.Language.French, file);
        }
    }
}
