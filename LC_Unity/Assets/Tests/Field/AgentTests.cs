using NUnit.Framework;
using UnityEngine;
using Field;
using System.Collections.Generic;

namespace Testing.Field
{
    public class AgentTests : TestFoundation
    {
        [Test]
        public void UpdateAgentSpeedTest()
        {
            Agent agent = Setup();

            Assert.IsTrue(Mathf.Abs(0.0f - agent.Speed) < 0.001f);

            agent.UpdateSpeed(5.0f);

            Assert.IsTrue(Mathf.Abs(5.0f - agent.Speed) < 0.001f);
        }

        [Test]
        public void UpdateAgentCurrentDirectionTest()
        {
            Agent agent = Setup();

            agent.UpdateDirection(Movement.Direction.Right);

            Assert.AreEqual(Movement.Direction.Right, agent.CurrentDirection);

            agent.FixedDirection = true;
            agent.UpdateDirection(Movement.Direction.Down);

            Assert.AreEqual(Movement.Direction.Right, agent.CurrentDirection);
        }

        private Agent Setup()
        {
            GameObject go = new GameObject("Agent");
            _usedGameObjects.Add(go);
            go.AddComponent<Animator>();
            return go.AddComponent<Agent>();
        }
    }
}
