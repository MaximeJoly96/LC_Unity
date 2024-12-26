using NUnit.Framework;
using UnityEngine;
using Engine.Events;
using UnityEditor;

namespace Testing.Engine.Events
{
    public class EventsSequenceParserTests : TestFoundation
    {
        [Test]
        public void ParseEventsSequenceTest()
        {
            TextAsset file = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Engine/Events/TestSequence.xml");
            EventsSequence sequence = EventsSequenceParser.ParseEventsSequence(file);

            Assert.IsNotNull(sequence);
            Assert.AreEqual(58, sequence.Events.Count);
        }
    }
}
