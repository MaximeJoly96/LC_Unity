using NUnit.Framework;
using Engine.Events;
using Engine.Party;

namespace Testing.Engine.Events
{
    public class EventsSequenceTests
    {
        [Test]
        public void CreateEventsSequenceTest()
        {
            EventsSequence sequence = new EventsSequence();

            Assert.NotNull(sequence);
            Assert.NotNull(sequence.Events);
            Assert.AreEqual(0, sequence.Events.Count);
            Assert.NotNull(sequence.Finished);
        }

        [Test]
        public void AddEventToSequenceTest()
        {
            EventsSequence sequence = new EventsSequence();

            ChangeItems change = new ChangeItems
            {
                Id = 1,
                Quantity = 3
            };

            Assert.AreEqual(0, sequence.Events.Count);

            sequence.Add(change);

            Assert.AreEqual(1, sequence.Events.Count);

            Assert.IsTrue(sequence.Events[0] is ChangeItems);
            Assert.AreEqual(3, (sequence.Events[0] as ChangeItems).Quantity);
        }
    }
}
