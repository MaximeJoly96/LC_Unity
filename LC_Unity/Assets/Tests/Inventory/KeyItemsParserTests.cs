using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Inventory;

namespace Testing.Inventory
{
    public class KeyItemsParserTests : TestFoundation
    {
        [Test]
        public void KeyItemsCanBeParsed()
        {
            List<KeyItem> keyItems = KeyItemsParser.ParseKeyItems(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Inventory/TestData/TestKeyItems.xml"));

            Assert.AreEqual(1, keyItems.Count);

            Assert.AreEqual(5000, keyItems[0].Id);
            Assert.AreEqual("sealedMedallion", keyItems[0].Name);
            Assert.AreEqual("sealedMedallionDescription", keyItems[0].Description);
            Assert.AreEqual(0, keyItems[0].Icon);
            Assert.AreEqual(0, keyItems[0].Price);
        }
    }
}
