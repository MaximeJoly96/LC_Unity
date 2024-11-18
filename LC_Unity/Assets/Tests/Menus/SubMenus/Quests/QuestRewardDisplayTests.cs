using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using Menus.SubMenus.Quests;
using Questing;
using System.Collections.Generic;
using Inventory;
using Core.Model;
using Language;

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

        [UnityTest]
        public IEnumerator QuestRewardDisplayCanBeInited()
        {
            RewardComponentDisplay rewardComponent = ComponentCreator.CreateRewardComponentDisplay();
            _usedGameObjects.Add(rewardComponent.gameObject);

            Localizer localizer = ComponentCreator.CreateLocalizer("Menus/SubMenus/Quests/french.csv", global::Language.Language.French);
            _usedGameObjects.Add(localizer.gameObject);

            QuestRewardDisplay display = CreateEmptyQuestRewardDisplay();
            _usedGameObjects.Add(display.gameObject);

            List<InventoryItem> items = new List<InventoryItem>
            { 
                new InventoryItem(new BaseItem(new ElementIdentifier(0, "potion", "potionDescription"), 0, 3, ItemCategory.Consumable))
            };
            items[0].ChangeAmount(3);

            QuestReward reward = new QuestReward(250, 300, items);
            display.SetRewardComponentPrefab(rewardComponent);
            display.Init(reward);

            yield return null;

            Assert.AreEqual(3, display.transform.childCount);

            Assert.AreEqual("XP", display.transform.GetChild(0).GetComponent<RewardComponentDisplay>().Label.text);
            Assert.AreEqual("250", display.transform.GetChild(0).GetComponent<RewardComponentDisplay>().Quantity.text);

            Assert.AreEqual("Okanes", display.transform.GetChild(1).GetComponent<RewardComponentDisplay>().Label.text);
            Assert.AreEqual("300", display.transform.GetChild(1).GetComponent<RewardComponentDisplay>().Quantity.text);

            Assert.AreEqual("Potion", display.transform.GetChild(2).GetComponent<RewardComponentDisplay>().Label.text);
            Assert.AreEqual("3", display.transform.GetChild(2).GetComponent<RewardComponentDisplay>().Quantity.text);
        }

        [UnityTest]
        public IEnumerator VisualStatusCanBeUpdated()
        {
            RewardComponentDisplay rewardComponent = ComponentCreator.CreateRewardComponentDisplay();
            _usedGameObjects.Add(rewardComponent.gameObject);

            Localizer localizer = ComponentCreator.CreateLocalizer("Menus/SubMenus/Quests/french.csv", global::Language.Language.French);
            _usedGameObjects.Add(localizer.gameObject);

            QuestRewardDisplay display = CreateEmptyQuestRewardDisplay();
            _usedGameObjects.Add(display.gameObject);

            QuestReward reward = new QuestReward(250, 300, new List<InventoryItem>());

            display.SetRewardComponentPrefab(rewardComponent);
            display.Init(reward);

            yield return null;

            for (int i = 0; i < display.RewardComponents.Count; i++)
            {
                AssertRewardComponentColor(display.RewardComponents[i], Color.white);
            }

            display.UpdateVisualStatus(Color.red);

            for(int i = 0; i < display.RewardComponents.Count; i++)
            {
                AssertRewardComponentColor(display.RewardComponents[i], Color.red);
            }
        }

        private void AssertRewardComponentColor(RewardComponentDisplay rewardComponent, Color color)
        {
            Assert.IsTrue(Mathf.Abs(color.r - rewardComponent.Label.color.r) < 0.01f);
            Assert.IsTrue(Mathf.Abs(color.g - rewardComponent.Label.color.g) < 0.01f);
            Assert.IsTrue(Mathf.Abs(color.b - rewardComponent.Label.color.b) < 0.01f);
            Assert.IsTrue(Mathf.Abs(color.a - rewardComponent.Label.color.a) < 0.01f);

            Assert.IsTrue(Mathf.Abs(color.r - rewardComponent.Quantity.color.r) < 0.01f);
            Assert.IsTrue(Mathf.Abs(color.g - rewardComponent.Quantity.color.g) < 0.01f);
            Assert.IsTrue(Mathf.Abs(color.b - rewardComponent.Quantity.color.b) < 0.01f);
            Assert.IsTrue(Mathf.Abs(color.a - rewardComponent.Quantity.color.a) < 0.01f);
        }

        private QuestRewardDisplay CreateEmptyQuestRewardDisplay()
        {
            GameObject go = ComponentCreator.CreateEmptyGameObject();
            _usedGameObjects.Add(go);

            return go.AddComponent<QuestRewardDisplay>();
        }
    }
}
