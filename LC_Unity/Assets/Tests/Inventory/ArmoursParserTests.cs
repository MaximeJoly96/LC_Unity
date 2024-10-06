using NUnit.Framework;
using Inventory;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Testing.Inventory
{
    public class ArmoursParserTests
    {
        [Test]
        public void ArmoursCanBeParsed()
        {
            List<Armour> armours = ArmoursParser.ParseArmours(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Inventory/TestData/TestArmours.xml"));

            Assert.AreEqual(3, armours.Count);

            Assert.AreEqual(2000, armours[0].Id);
            Assert.AreEqual("leatherDoublet", armours[0].Name);
            Assert.AreEqual("leatherDoubletDescription", armours[0].Description);
            Assert.AreEqual(0, armours[0].Icon);
            Assert.AreEqual(10, armours[0].Price);
            Assert.AreEqual(ArmourType.Body, armours[0].Type);
            Assert.AreEqual(3, armours[0].EnchantmentSlots);

            Assert.AreEqual(2001, armours[1].Id);
            Assert.AreEqual("leatherHelmet", armours[1].Name);
            Assert.AreEqual("leatherHelmetDescription", armours[1].Description);
            Assert.AreEqual(0, armours[1].Icon);
            Assert.AreEqual(10, armours[1].Price);
            Assert.AreEqual(ArmourType.Head, armours[1].Type);
            Assert.AreEqual(3, armours[1].EnchantmentSlots);

            Assert.AreEqual(2002, armours[2].Id);
            Assert.AreEqual("woodenShield", armours[2].Name);
            Assert.AreEqual("woodenShieldDescription", armours[2].Description);
            Assert.AreEqual(0, armours[2].Icon);
            Assert.AreEqual(10, armours[2].Price);
            Assert.AreEqual(ArmourType.Shield, armours[2].Type);
            Assert.AreEqual(3, armours[2].EnchantmentSlots);
        }
    }
}
