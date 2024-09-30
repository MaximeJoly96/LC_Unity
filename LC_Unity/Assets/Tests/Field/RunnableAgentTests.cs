﻿using NUnit.Framework;
using UnityEngine;
using Field;
using Engine.Events;
using UnityEngine.TestTools;
using System.Collections;
using UnityEditor;
using Timing;
using System.Collections.Generic;

namespace Testing.Field
{
    public class RunnableAgentTests
    {
        private List<GameObject> _usedGameObjects;

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

        private GameObject Setup()
        {
            GameObject agentGo = new GameObject("Agent");
            _usedGameObjects.Add(agentGo);
            agentGo.AddComponent<EventsRunner>();
            agentGo.AddComponent<RunnableAgent>();

            return agentGo;
        }

        [UnityTest]
        public IEnumerator RunSequenceOfRunnableAgentTest()
        {
            GameObject agent = Setup();

            GameObject waiterGo = new GameObject("Waiter");
            _usedGameObjects.Add(waiterGo);
            waiterGo.AddComponent<Waiter>();

            TextAsset file = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Field/RunnableAgentTestSequence.xml");
            agent.GetComponent<RunnableAgent>().SetSequence(EventsSequenceParser.ParseEventsSequence(file));

            bool finishedSequence = false;
            agent.GetComponent<RunnableAgent>().FinishedSequence.AddListener(() => finishedSequence = true);

            agent.GetComponent<RunnableAgent>().RunSequence();

            yield return new WaitUntil(() => finishedSequence);
        }
    }
}
