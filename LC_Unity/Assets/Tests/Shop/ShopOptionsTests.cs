using NUnit.Framework;
using Shop;
using System.Collections.Generic;
using UnityEngine;

namespace Testing.Shop
{
    public class ShopOptionsTests
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
        public void ShopOptionCanBeCreated()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);
            ShopOptions options = go.AddComponent<ShopOptions>();

            options.Option = ShopOption.Buy;

            Assert.AreEqual(ShopOption.Buy, options.Option);
        }
    }
}
