using NUnit.Framework;
using UnityEngine;
using Engine.Events;
using UnityEditor;

namespace Testing.Engine.Events
{
    public class EventsSequenceParserTests
    {
        [Test]
        public void ParseEventsSequenceTest()
        {
            TextAsset file = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Engine/Events/TestSequence.xml");
            EventsSequence sequence = EventsSequenceParser.ParseEventsSequence(file);

            Assert.IsNotNull(sequence);
            Assert.AreEqual(56, sequence.Events.Count);
        }
    }
}
