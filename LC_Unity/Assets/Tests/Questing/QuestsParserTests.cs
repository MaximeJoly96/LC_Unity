using NUnit.Framework;
using Questing;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Inventory;

namespace Testing.Questing
{
    public class QuestsParserTests : TestFoundation
    {
        private ItemsWrapper CreateEmptyWrapper()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);
            return go.AddComponent<ItemsWrapper>();
        }

        [Test]
        public void QuestDataCanBeParsed()
        {
            ItemsWrapper wrapper = CreateEmptyWrapper();
            wrapper.FeedConsumables(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Questing/TestData/TestConsumables.xml"));

            TextAsset file = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Questing/TestData/TestQuests.xml");

            Quest quest = QuestsParser.ParseAllQuests(file)[0];

            Assert.AreEqual(0, quest.Id);
            Assert.AreEqual("testQuest0", quest.NameKey);
            Assert.AreEqual("testQuest0Description", quest.DescriptionKey);
            Assert.AreEqual(3, quest.Steps.Count);

            Assert.AreEqual(0, quest.Steps[0].Id);
            Assert.AreEqual("step0", quest.Steps[0].NameKey);
            Assert.AreEqual("step0Description", quest.Steps[0].DescriptionKey);
            Assert.AreEqual(0, quest.Steps[0].Reward.Exp);
            Assert.AreEqual(0, quest.Steps[0].Reward.Gold);
            Assert.AreEqual(0, quest.Steps[0].Reward.Items.Count);

            Assert.AreEqual(1, quest.Steps[1].Id);
            Assert.AreEqual("step1", quest.Steps[1].NameKey);
            Assert.AreEqual("step1Description", quest.Steps[1].DescriptionKey);
            Assert.AreEqual(0, quest.Steps[1].Reward.Exp);
            Assert.AreEqual(0, quest.Steps[1].Reward.Gold);
            Assert.AreEqual(1, quest.Steps[1].Reward.Items.Count);
            Assert.AreEqual(5, quest.Steps[1].Reward.Items[0].ItemData.Id);
            Assert.AreEqual(3, quest.Steps[1].Reward.Items[0].InPossession);

            Assert.AreEqual(2, quest.Steps[2].Id);
            Assert.AreEqual("step2", quest.Steps[2].NameKey);
            Assert.AreEqual("step2Description", quest.Steps[2].DescriptionKey);
            Assert.AreEqual(1500, quest.Steps[2].Reward.Exp);
            Assert.AreEqual(300, quest.Steps[2].Reward.Gold);
            Assert.AreEqual(0, quest.Steps[2].Reward.Items.Count);

            Assert.AreEqual(1000, quest.Reward.Exp);
            Assert.AreEqual(0, quest.Reward.Gold);
            Assert.AreEqual(1, quest.Reward.Items.Count);
            Assert.AreEqual(1000, quest.Reward.Items[0].ItemData.Id);
            Assert.AreEqual(4, quest.Reward.Items[0].InPossession);
        }
    }
}
