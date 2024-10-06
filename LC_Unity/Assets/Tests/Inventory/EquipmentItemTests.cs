using Core.Model;
using NUnit.Framework;
using Inventory;
using Effects;
using System.Collections.Generic;
using UnityEngine;
using Language;
using UnityEditor;

namespace Testing.Inventory
{
    public class EquipmentItemTests
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
        public void EquipmentItemCanBeCreated()
        {
            EquipmentItem item = new EquipmentItem(new ElementIdentifier(5, "equipment", "equipmentDesc"), 6, 7, ItemCategory.Armour, 10);

            Assert.AreEqual(5, item.Id);
            Assert.AreEqual("equipment", item.Name);
            Assert.AreEqual("equipmentDesc", item.Description);
            Assert.AreEqual(6, item.Icon);
            Assert.AreEqual(7, item.Price);
            Assert.AreEqual(ItemCategory.Armour, item.Category);
            Assert.AreEqual(10, item.EnchantmentSlots);
        }

        [Test]
        public void DetailedDescriptionCanBeObtained()
        {
            GameObject localizer = new GameObject("Localizer");
            _usedGameObjects.Add(localizer);
            Localizer component = localizer.AddComponent<Localizer>();

            TextAsset file = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Inventory/french.csv");
            component.LoadLanguage(Language.Language.French, file);

            EquipmentItem item = new EquipmentItem(new ElementIdentifier(5, "equipment", "equipmentDesc"), 6, 7, ItemCategory.Armour, 10);
            ItemStats stats = new ItemStats(10, 0, 0, 8, 4, 0, 0, 2, -5);
            StatBoost boost = new StatBoost { Stat = Stat.Accuracy, Value = 15.5f };

            item.Stats = stats;
            item.AddEffect(boost);

            string detailedDescription = item.DetailedDescription();

            Assert.AreEqual("description de l'équipement\n" + 
                            "Santé 10\nForce 8\nDéfense 4\nAgilité 2\nChance -5\n" +
                            "Emplacements pour enchant. : 10\n" +
                            "15,5% bonus Précision\n", detailedDescription);
        }
    }
}
