using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using Menus.SubMenus.Quests;

namespace Testing.Menus.SubMenus.Quests
{
    public class QuestRewardDisplayTests : TestFoundation
    {
        [UnityTest]
        public IEnumerator QuestRewardDisplayCanBeCleared()
        {
            QuestRewardDisplay display = CreateEmptyQuestRewardDisplay();

            GameObject child1 = ComponentCreator.CreateEmptyGameObject();
            child1.transform.SetParent(display.transform);
            GameObject child2 = ComponentCreator.CreateEmptyGameObject();
            child2.transform.SetParent(display.transform);
            GameObject child3 = ComponentCreator.CreateEmptyGameObject();
            child3.transform.SetParent(display.transform);

            yield return null;

            Assert.AreEqual(3, display.transform.childCount);

            display.Clear();

            yield return null;

            Assert.AreEqual(0, display.transform.childCount);
            Assert.AreEqual(0, display.RewardComponents.Count);
        }

        private QuestRewardDisplay CreateEmptyQuestRewardDisplay()
        {
            GameObject go = ComponentCreator.CreateEmptyGameObject();
            _usedGameObjects.Add(go);

            return go.AddComponent<QuestRewardDisplay>();
        }
    }
}
