using BattleSystem.Model;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

namespace Testing.BattleSystem.Model
{
    public class TimelineSegmentTests : TestFoundation
    {
        [Test]
        public void TimelineSegmentCanBeCreated()
        {
            TimelineSegment segment = new TimelineSegment
            {
                Priority = 3,
                Actions = new List<TimelineAction>()
            };

            Assert.AreEqual(3, segment.Priority);
            Assert.AreEqual(0, segment.Actions.Count);
            Assert.IsTrue(Mathf.Abs(segment.Length - 0.0f) < 0.01f);
        }

        [Test]
        public void SegmentLengthCanBeComputed()
        {
            TimelineSegment segment = new TimelineSegment
            {
                Priority = 3,
                Actions = new List<TimelineAction>()
            };

            segment.Actions.Add(new TimelineAction(4.0f, 0.0f, 3));
            segment.Actions.Add(new TimelineAction(2.5f, 1.2f, 3));

            Assert.IsTrue(Mathf.Abs(segment.Length - 4.0f) < 0.01f);
        }
    }
}
