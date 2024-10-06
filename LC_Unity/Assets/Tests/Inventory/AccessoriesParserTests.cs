using NUnit.Framework;
using Inventory;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Effects;

namespace Testing.Inventory
{
    public class AccessoriesParserTests
    {
        [Test]
        public void AccessoriesCanBeParsed()
        {
            List<Accessory> accessories = AccessoriesParser.ParseAccessories(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Inventory/TestData/TestAccessories.xml"));

            Assert.AreEqual(1, accessories.Count);
            Assert.AreEqual(3000, accessories[0].Id);
            Assert.AreEqual("bloodRing", accessories[0].Name);
            Assert.AreEqual("bloodRingDescription", accessories[0].Description);
            Assert.AreEqual(0, accessories[0].Icon);
            Assert.AreEqual(10, accessories[0].Price);
            Assert.AreEqual(3, accessories[0].EnchantmentSlots);
            Assert.AreEqual(1, accessories[0].Effects.Count);
            Assert.IsTrue(accessories[0].Effects[0] is StatBoost);

            StatBoost boost = accessories[0].Effects[0] as StatBoost;

            Assert.AreEqual(Stat.HP, boost.Stat);
            Assert.IsTrue(Mathf.Abs(10.0f - boost.Value) < 0.01f);
        }
    }
}
