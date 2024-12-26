using NUnit.Framework;
using Shop;
using System.Collections.Generic;
using UnityEngine;

namespace Testing.Shop
{
    public class ShopOptionsTests : TestFoundation
    {
        [Test]
        public void ShopOptionCanBeCreated()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);
            ShopHorizontalMenuButton options = go.AddComponent<ShopHorizontalMenuButton>();

            options.Option = ShopOption.Buy;

            Assert.AreEqual(ShopOption.Buy, options.Option);
        }
    }
}
