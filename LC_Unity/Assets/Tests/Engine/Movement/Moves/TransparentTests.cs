using NUnit.Framework;
using Engine.Movement.Moves;
using Engine.Movement;
using UnityEngine;
using Field;
using Engine.Events;
using System.Collections;
using UnityEngine.TestTools;
using Movement;

namespace Testing.Engine.Movement.Moves
{
    public class TransparentTests : XmlBaseParser
    {
        protected override string TestFilePath { get { return "Assets/Tests/Engine/Movement/Moves/Transparent.xml"; } }

        [UnityTest]
        public IEnumerator TransparentAppliesTransparencyToSpriteRenderer()
        {
            SetMoveRoute route = XmlMovementParser.ParseSetMoveRoute(GetDataToParse("SetMoveRoute"));

            GameObject moveRouteRunner = new GameObject("MoveRouteRunner");
            moveRouteRunner.AddComponent<MoveRouteRunner>();

            GameObject agent = new GameObject("Agent");
            Agent agentComponent = agent.AddComponent<Agent>();
            agentComponent.SetId("testAgent");

            AgentsManager.Instance.RegisterAgent(agentComponent);

            SpriteRenderer sr = agent.AddComponent<SpriteRenderer>();
            sr.color = Color.white;

            EventsRunner runner = agent.AddComponent<EventsRunner>();
            EventsSequence sequence = new EventsSequence();
            sequence.Add(route);

            Assert.IsTrue(Mathf.Abs(1.0f - sr.color.a) < 0.01f);

            runner.RunEvents(sequence);

            yield return new WaitForSeconds(0.1f);

            Assert.IsTrue(Mathf.Abs(0.0f - sr.color.a) < 0.01f);
        }
    }
}
