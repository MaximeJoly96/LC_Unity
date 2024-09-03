using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TitleScreen;
using UnityEngine;
using UnityEngine.TestTools;

namespace Testing.TitleScreen
{
    public class TitleScreenTests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void TitleScreenManagerInitializationPasses()
        {
            GameObject go = new GameObject("TitleScreenManager");
            go.AddComponent<TitleScreenManager>();
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator TitleScreenTestsWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
