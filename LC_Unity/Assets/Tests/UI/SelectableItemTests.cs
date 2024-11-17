using UnityEngine;
using NUnit.Framework;
using UI;
using UnityEngine.TestTools;
using System.Collections;

namespace Testing.UI
{
    public class SelectableItemTests : TestFoundation
    {
        [UnityTest]
        public IEnumerator CursorCanBeShownAndHidden()
        {
            SelectableItem item = ComponentCreator.CreateSelectableItem();
            _usedGameObjects.Add(item.gameObject);

            yield return null;

            item.ShowCursor(true);

            yield return null;

            Assert.IsTrue(item.Cursor.gameObject.activeInHierarchy);

            item.ShowCursor(false);

            yield return null;

            Assert.IsFalse(item.Cursor.gameObject.activeInHierarchy);
        }
    }
}
