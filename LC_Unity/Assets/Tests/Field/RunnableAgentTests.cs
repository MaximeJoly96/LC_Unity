﻿using NUnit.Framework;
using UnityEngine;
using Field;
using Engine.Events;
using UnityEngine.TestTools;
using System.Collections;
using UnityEditor;
using Timing;

namespace Testing.Field
{
    public class RunnableAgentTests
    {
        private GameObject Setup()
        {
            GameObject agentGo = new GameObject("Agent");
            agentGo.AddComponent<EventsRunner>();
            agentGo.AddComponent<RunnableAgent>();

            return agentGo;
        }

        [UnityTest]
        public IEnumerator RunSequenceOfRunnableAgentTest()
        {
            GameObject agent = Setup();

            GameObject waiterGo = new GameObject("Waiter");
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