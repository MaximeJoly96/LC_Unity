using NUnit.Framework;
using Menus;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Language;
using UnityEditor;

namespace Testing.Menus
{
    public class XpGaugeTests
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
        public void LevelCanBeSet()
        {
            XpGauge gauge = CreateDefaultGauge();
            CreateFrenchLocalizer();

            gauge.SetLevel(250.0f, 750.0f, 5);

            Assert.AreEqual("33%", gauge.Value.text);
            Assert.IsTrue(Mathf.Abs(0.333f - gauge.Gauge.fillAmount) < 0.01f);
            Assert.AreEqual("Niv. 6", gauge.LevelLabel.text);

            gauge.SetLevel(0.0f, 750.0f, 9);

            Assert.AreEqual("0%", gauge.Value.text);
            Assert.IsTrue(Mathf.Abs(0.0f - gauge.Gauge.fillAmount) < 0.01f);
            Assert.AreEqual("Niv. 10", gauge.LevelLabel.text);

            gauge.SetLevel(0.0f, 0.0f, 8);

            Assert.AreEqual("0%", gauge.Value.text);
            Assert.IsTrue(Mathf.Abs(0.0f - gauge.Gauge.fillAmount) < 0.01f);
            Assert.AreEqual("Niv. 9", gauge.LevelLabel.text);

            gauge.SetLevel(1000.0f, 520.0f, 40);

            Assert.AreEqual("100%", gauge.Value.text);
            Assert.IsTrue(Mathf.Abs(1.0f - gauge.Gauge.fillAmount) < 0.01f);
            Assert.AreEqual("Niv. 41", gauge.LevelLabel.text);
        }

        private XpGauge CreateDefaultGauge()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);

            GameObject childImg = new GameObject();
            childImg.AddComponent<Image>();
            childImg.transform.SetParent(go.transform);
            GameObject childText = new GameObject();
            TextMeshProUGUI valueTxt = childText.AddComponent<TextMeshProUGUI>();
            childText.transform.SetParent(go.transform);
            GameObject childTextLvl = new GameObject();
            TextMeshProUGUI levelTxt = childTextLvl.AddComponent<TextMeshProUGUI>();
            childTextLvl.transform.SetParent(go.transform);

            _usedGameObjects.Add(childImg);
            _usedGameObjects.Add(childText);
            _usedGameObjects.Add(childTextLvl);

            XpGauge gauge = go.AddComponent<XpGauge>();
            gauge.Init(childImg.GetComponent<Image>(), valueTxt, levelTxt);

            return gauge;
        }

        private Localizer CreateFrenchLocalizer()
        {
            GameObject localizer = new GameObject("Localizer");
            _usedGameObjects.Add(localizer);
            Localizer component = localizer.AddComponent<Localizer>();

            TextAsset file = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Menus/french.csv");
            component.LoadLanguage(Language.Language.French, file);

            return component;
        }
    }
}
