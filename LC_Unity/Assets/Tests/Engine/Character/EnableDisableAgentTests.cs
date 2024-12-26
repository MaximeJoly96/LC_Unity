using Engine.Character;
using Engine.Events;
using Field;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

namespace Testing.Engine.Character
{
    public class EnableDisableAgentTests : XmlBaseParser
    {
        protected override string TestFilePath { get { return "Assets/Tests/Engine/Character/EnableDisableAgent.xml"; } }

        [UnityTest]
        public IEnumerator EnableAgentTest()
        {
            GameObject agent = CreateAgentGameObject("agentToEnable");
            _usedGameObjects.Add(agent);

            SpriteRenderer sr = agent.AddComponent<SpriteRenderer>();
            Animator animator = agent.AddComponent<Animator>();
            Collider2D col = agent.AddComponent<BoxCollider2D>();

            agent.GetComponent<Agent>().DisableAgent();

            yield return null;

            Assert.IsTrue(agent.GetComponent<Agent>().Disabled);
            Assert.IsFalse(sr.enabled);
            Assert.IsFalse(animator.enabled);
            Assert.IsFalse(col.enabled);

            EventsRunner runner = agent.AddComponent<EventsRunner>();
            EventsSequence sequence = new EventsSequence();

            EnableAgent enableAgent = XmlCharacterParser.ParseEnableAgent(GetDataToParse("EnableAgent"));
            sequence.Add(enableAgent);
            runner.RunEvents(sequence);

            yield return null;

            Assert.IsFalse(agent.GetComponent<Agent>().Disabled);
            Assert.IsTrue(sr.enabled);
            Assert.IsTrue(animator.enabled);
            Assert.IsTrue(col.enabled);
        }

        [UnityTest]
        public IEnumerator DisableAgentTest()
        {
            GameObject agent = CreateAgentGameObject("agentToDisable");
            _usedGameObjects.Add(agent);

            SpriteRenderer sr = agent.AddComponent<SpriteRenderer>();
            Animator animator = agent.AddComponent<Animator>();
            Collider2D col = agent.AddComponent<BoxCollider2D>();

            agent.GetComponent<Agent>().EnableAgent();

            yield return null;

            Assert.IsFalse(agent.GetComponent<Agent>().Disabled);
            Assert.IsTrue(sr.enabled);
            Assert.IsTrue(animator.enabled);
            Assert.IsTrue(col.enabled);

            EventsRunner runner = agent.AddComponent<EventsRunner>();
            EventsSequence sequence = new EventsSequence();

            DisableAgent disableAgent = XmlCharacterParser.ParseDisableAgent(GetDataToParse("DisableAgent"));
            sequence.Add(disableAgent);
            runner.RunEvents(sequence);

            yield return null;

            Assert.IsTrue(agent.GetComponent<Agent>().Disabled);
            Assert.IsFalse(sr.enabled);
            Assert.IsFalse(animator.enabled);
            Assert.IsFalse(col.enabled);
        }

        private GameObject CreateAgentGameObject(string name)
        {
            AgentsManager.Instance.Reset();

            GameObject agent = new GameObject(name);
            Agent agentComponent = agent.AddComponent<Agent>();
            agentComponent.SetId(name);

            AgentsManager.Instance.RegisterAgent(agentComponent);

            return agent;
        }
    }
}
