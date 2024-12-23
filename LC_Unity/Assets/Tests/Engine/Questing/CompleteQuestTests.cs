﻿using Questing;
using Engine.Questing;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Inventory;
using UnityEditor;

namespace Testing.Engine.Questing
{
    public class CompleteQuestTests
    {
        private List<GameObject> _usedGameObjects;

        [SetUp]
        public void Setup()
        {
            QuestManager.Instance.Reset();
        }

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
        public void CompleteQuestCanRun()
        {
            CreateQuestsWrapper();

            ItemsWrapper wrapper = CreateEmptyWrapper();
            wrapper.FeedConsumables(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Questing/TestData/TestConsumables.xml"));

            StartQuest start = new StartQuest
            {
                Id = 0
            };

            start.Run();

            CompleteQuest complete = new CompleteQuest
            {
                Id = 0
            };

            complete.Run();

            Assert.IsTrue(QuestManager.Instance.CompletedQuests.Count(q => q.Id == complete.Id) == 1);
            Assert.AreEqual(QuestStatus.Completed, QuestManager.Instance.GetQuest(complete.Id).Status);
        }

        private QuestsWrapper CreateQuestsWrapper()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);

            QuestsWrapper wrapper = go.AddComponent<QuestsWrapper>();
            wrapper.Feed(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Questing/TestData/MainQuests.xml"),
                         AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Questing/TestData/SideQuests.xml"),
                         AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Questing/TestData/Bounties.xml"),
                         AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Questing/TestData/ProfessionQuests.xml"));

            return wrapper;
        }

        private ItemsWrapper CreateEmptyWrapper()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);
            return go.AddComponent<ItemsWrapper>();
        }
    }
}
