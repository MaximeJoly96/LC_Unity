﻿using NUnit.Framework;
using UnityEngine;
using Engine.Map;
using Field;
using Language;
using UnityEditor;
using Core;
using TMPro;

namespace Testing.Engine.Map
{
    public class ChangeMapNameDisplayTests
    {
        [Test]
        public void ChangeStatusOfMapNameDisplayTest()
        {
            ChangeMapNameDisplay changeEnabled = new ChangeMapNameDisplay { Enabled = true };
            ChangeMapNameDisplay changeDisabled = new ChangeMapNameDisplay { Enabled = false };

            GameObject go = new GameObject("MapNameDisplay");
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
            Localizer component = localizer.AddComponent<Localizer>();

            TextAsset file = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Engine/Map/french.csv");
            component.LoadLanguage(Language.Language.French, file);

            GlobalStateMachine.Instance.CurrentMapId = 0;

            GameObject go = new GameObject("MapNameDisplay");
            MapNameDisplay display = go.AddComponent<MapNameDisplay>();
            TextMeshProUGUI text = go.AddComponent<TextMeshProUGUI>();

            display.Show();

            Assert.AreEqual("Bastion de Haalmikah", text.text);

            GlobalStateMachine.Instance.CurrentMapId = 1;

            display.Show();

            Assert.AreEqual("Région de Haalmikah - Est", text.text);
        }
    }
}