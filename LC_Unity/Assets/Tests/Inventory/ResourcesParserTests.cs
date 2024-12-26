using NUnit.Framework;
using System.Collections.Generic;
using Inventory;
using UnityEngine;
using UnityEditor;

namespace Testing.Inventory
{
    public class ResourcesParserTests : TestFoundation
    {
        [Test]
        public void ResourcesCanBeParsed()
        {
            List<Resource> resources = ResourcesParser.ParseResources(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Inventory/TestData/TestResources.xml"));

            Assert.AreEqual(1, resources.Count);

            Assert.AreEqual(4000, resources[0].Id);
            Assert.AreEqual("ironOre", resources[0].Name);
            Assert.AreEqual("ironOreDescription", resources[0].Description);
            Assert.AreEqual(0, resources[0].Icon);
            Assert.AreEqual(10, resources[0].Price);
        }
    }
}
