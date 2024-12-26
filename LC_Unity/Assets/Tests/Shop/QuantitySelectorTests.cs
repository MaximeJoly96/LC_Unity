using NUnit.Framework;
using Shop;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;

namespace Testing.Shop
{
    public class QuantitySelectorTests : TestFoundation
    {
        private QuantitySelector CreateQuantitySelector()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);
            QuantitySelector selector = go.AddComponent<QuantitySelector>();

            GameObject background = new GameObject();
            _usedGameObjects.Add(background);
            selector.SetBackgroundObject(background.transform);

            GameObject upArrow = new GameObject();
            _usedGameObjects.Add(upArrow);
            selector.SetUpArrowObject(upArrow.transform);

            GameObject downArrow = new GameObject();
            _usedGameObjects.Add(downArrow);
            selector.SetDownArrowObject(downArrow.transform);

            return selector;
        }

        [UnityTest]
        public IEnumerator SelectorCanBeSelected()
        {
            QuantitySelector selector = CreateQuantitySelector();
            selector.Select(true);

            yield return null;

            foreach (Transform child in selector.transform)
                Assert.IsTrue(child.gameObject.activeInHierarchy);

            yield return null;

            selector.Select(false);

            yield return null;

            foreach (Transform child in selector.transform)
                Assert.IsFalse(child.gameObject.activeInHierarchy);
        }

        [Test]
        public void QuantityCanBeChanged()
        {
            QuantitySelector selector = CreateQuantitySelector();
            TextMeshProUGUI text = selector.gameObject.AddComponent<TextMeshProUGUI>();

            selector.Increment();

            Assert.AreEqual(1, selector.Value);
            Assert.AreEqual("1", text.text);

            selector.Decrement();

            Assert.AreEqual(0, selector.Value);
            Assert.AreEqual("0", text.text);

            selector.Decrement();

            Assert.AreEqual(9, selector.Value);
            Assert.AreEqual("9", text.text);

            selector.Increment();

            Assert.AreEqual(0, selector.Value);
            Assert.AreEqual("0", text.text);
        }
    }
}
