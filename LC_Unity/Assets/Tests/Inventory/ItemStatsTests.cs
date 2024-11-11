using NUnit.Framework;
using Inventory;
using System.Collections.Generic;
using UnityEngine;
using Language;
using UnityEditor;

namespace Testing.Inventory
{
    public class ItemStatsTests
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
        public void ItemStatsCanBeCreated()
        {
            ItemStats stats = new ItemStats(50, 60, 20, -3, 5, 8, 0, 9, 6);

            Assert.AreEqual(50, stats.Health);
            Assert.AreEqual(60, stats.Mana);
            Assert.AreEqual(20, stats.Essence);
            Assert.AreEqual(-3, stats.Strength);
            Assert.AreEqual(5, stats.Defense);
            Assert.AreEqual(8, stats.Magic);
            Assert.AreEqual(0, stats.MagicDefense);
            Assert.AreEqual(9, stats.Agility);
            Assert.AreEqual(6, stats.Luck);
        }

        [Test]
        public void StatsCanBePrinted()
        {
            GameObject localizer = new GameObject("Localizer");
            _usedGameObjects.Add(localizer);
            Localizer component = localizer.AddComponent<Localizer>();

            TextAsset[] files = new TextAsset[] { AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Inventory/french.csv") };
            component.LoadLanguage(global::Language.Language.French, files);

            ItemStats stats = new ItemStats(50, 60, 20, -3, 5, 8, 0, 9, 6);

            Assert.AreEqual("Santé 50\nMana 60\nEssence 20\nForce -3\n" +
                            "Défense 5\nMagie 8\nAgilité 9\nChance 6\n", stats.ToString());
        }
    }
}
