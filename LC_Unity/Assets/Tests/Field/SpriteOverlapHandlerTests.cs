using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;
using Field;
using UnityEngine.TestTools;
using System.Collections;

namespace Testing.Field
{
    public class SpriteOverlapHandlerTests : TestFoundation
    {
        [UnityTest]
        public IEnumerator SortingOrderIsAdjustedWhenYPositionChanges()
        {
            SpriteOverlapHandler handler = CreateDefaultHandler();

            yield return null;

            Assert.AreEqual(0, handler.GetComponent<SpriteRenderer>().sortingOrder);

            handler.transform.Translate(new Vector3(2.5f, -1.8f, 0.0f));

            yield return null;

            Assert.AreEqual(36, handler.GetComponent<SpriteRenderer>().sortingOrder);
        }

        private SpriteOverlapHandler CreateDefaultHandler()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);

            SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
            SpriteOverlapHandler handler = go.AddComponent<SpriteOverlapHandler>();

            renderer.sortingOrder = 0;

            return handler;
        }
    }
}
