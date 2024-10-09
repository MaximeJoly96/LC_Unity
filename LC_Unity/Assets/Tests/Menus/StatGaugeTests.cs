using NUnit.Framework;
using Menus;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace Testing.Menus
{
    public class StatGaugeTests
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
        public void GaugeCanBeSet()
        {
            StatGauge gauge = CreateDefaultGauge();

            gauge.SetGauge(25.0f, 250.0f);

            Assert.AreEqual("25 / 250", gauge.Value.text);
            Assert.IsTrue(Mathf.Abs(0.1f - gauge.Gauge.fillAmount) < 0.01f);

            gauge.SetGauge(100.0f, 100.0f);

            Assert.AreEqual("100 / 100", gauge.Value.text);
            Assert.IsTrue(Mathf.Abs(1.0f - gauge.Gauge.fillAmount) < 0.01f);

            gauge.SetGauge(0.0f, 0.0f);

            Assert.AreEqual("0 / 0", gauge.Value.text);
            Assert.IsTrue(Mathf.Abs(0.0f - gauge.Gauge.fillAmount) < 0.01f);

            gauge.SetGauge(10.0f, 1.0f);

            Assert.AreEqual("1 / 1", gauge.Value.text);
            Assert.IsTrue(Mathf.Abs(1.0f - gauge.Gauge.fillAmount) < 0.01f);
        }

        private StatGauge CreateDefaultGauge()
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
    }
}
