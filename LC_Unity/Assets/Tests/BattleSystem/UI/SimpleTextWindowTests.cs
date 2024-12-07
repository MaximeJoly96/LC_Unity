using BattleSystem.UI;
using NUnit.Framework;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.TestTools;
using System.Collections;

namespace Testing.BattleSystem.UI
{
    public class SimpleTextWindowTests : TestFoundation
    {
        [UnityTest]
        public IEnumerator TextCanBeUpdated()
        {
            SimpleTextWindow window = ComponentCreator.CreateSimpleTextWindow("BattleSystem/UI/TestAnimations/SimpleWindowController.controller");
            _usedGameObjects.Add(window.gameObject);

            window.UpdateText("my new string");

            yield return null;

            Assert.AreEqual("my new string", window.GetComponent<TextMeshProUGUI>().text);
        }
    }
}
