using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Menus;
using Actors;
using System.Collections.Generic;
using Language;
using UnityEditor;
using Core.Model;
using Utils;

namespace Testing.Menus
{
    public class CharacterPreviewTests
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
        public void CharacterDataCanBeFedToPreview()
        {
            CharacterPreview preview = CreateDefaultPreview();
            CreateFrenchLocalizer();

            Character character = new Character(new ElementIdentifier(0, "Louga", ""),
                                                new QuadraticFunction(450, 250, 10),
                                                new StatScalingFunction(22, 1.2f, 256),
                                                new StatScalingFunction(6, 0.8f, 25),
                                                new StatScalingFunction(0, 1.0f, 100),
                                                new StatScalingFunction(1.5f, 1.2f, 8),
                                                new StatScalingFunction(1.4f, 1.1f, 7),
                                                new StatScalingFunction(0.9f, 1.2f, 3),
                                                new StatScalingFunction(1.2f, 1.2f, 8),
                                                new StatScalingFunction(1.7f, 0.975f, 5),
                                                new StatScalingFunction(4, 0.85f, 6));

            preview.Feed(character);

            Assert.AreEqual(character, preview.Character);
            Assert.AreEqual("Louga", preview.Name.text);
            Assert.NotNull(preview.Faceset);

            Assert.AreEqual("256 / 256", preview.HpGauge.Value.text);
            Assert.IsTrue(Mathf.Abs(1.0f - preview.HpGauge.Gauge.fillAmount) < 0.01f);
            Assert.AreEqual("25 / 25", preview.ManaGauge.Value.text);
            Assert.IsTrue(Mathf.Abs(1.0f - preview.ManaGauge.Gauge.fillAmount) < 0.01f);
            Assert.AreEqual("100 / 100", preview.EssenceGauge.Value.text);
            Assert.IsTrue(Mathf.Abs(1.0f - preview.EssenceGauge.Gauge.fillAmount) < 0.01f);

            Assert.AreEqual("0%", preview.XpGauge.Value.text);
            Assert.IsTrue(Mathf.Abs(0.0f - preview.XpGauge.Gauge.fillAmount) < 0.01f);
            Assert.AreEqual("Niv. 1", preview.XpGauge.LevelLabel.text);
        }

        private CharacterPreview CreateDefaultPreview()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);
            CharacterPreview preview = go.AddComponent<CharacterPreview>();

            GameObject faceSetGo = new GameObject();
            _usedGameObjects.Add(faceSetGo);
            Image faceset = faceSetGo.AddComponent<Image>();
            faceSetGo.transform.SetParent(go.transform);

            GameObject nameGo = new GameObject();
            _usedGameObjects.Add(nameGo);
            TextMeshProUGUI nameText = nameGo.AddComponent<TextMeshProUGUI>();
            nameGo.transform.SetParent(go.transform);

            StatGauge hpGauge = CreateDefaultStatGauge();
            hpGauge.transform.SetParent(go.transform);
            StatGauge manaGauge = CreateDefaultStatGauge();
            manaGauge.transform.SetParent(go.transform);
            StatGauge essenceGauge = CreateDefaultStatGauge();
            essenceGauge.transform.SetParent(go.transform);
            XpGauge xpGauge = CreateDefaultXpGauge();
            xpGauge.transform.SetParent(go.transform);

            preview.Init();

            return preview;
        }

        private XpGauge CreateDefaultXpGauge()
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

        private StatGauge CreateDefaultStatGauge()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);

            GameObject childImg = new GameObject();
            childImg.AddComponent<Image>();
            childImg.transform.SetParent(go.transform);
            GameObject childText = new GameObject();
            childText.AddComponent<TextMeshProUGUI>();
            childText.transform.SetParent(go.transform);

            _usedGameObjects.Add(childImg);
            _usedGameObjects.Add(childText);

            StatGauge gauge = go.AddComponent<StatGauge>();
            gauge.Init(childImg.GetComponent<Image>(), childText.GetComponent<TextMeshProUGUI>());

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
