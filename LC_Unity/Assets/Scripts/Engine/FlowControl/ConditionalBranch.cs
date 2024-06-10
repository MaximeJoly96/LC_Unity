using Engine.Events;
using UnityEngine.Events;
using UnityEngine;

namespace Engine.FlowControl
{
    public abstract class ConditionalBranch : IRunnable
    {
        public string FirstMember { get; set; }
        public string SecondMember { get; set; }
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        public EventsSequence SequenceWhenTrue { get; set; }
        public EventsSequence SequenceWhenFalse { get; set; }

        protected ConditionalBranch()
        {
            Finished = new UnityEvent();
        }

        public abstract void Run();

        protected void DefineSequences(bool result)
        {
            EventsRunner runner = Object.FindObjectOfType<EventsRunner>();

            if (result)
            {
                SequenceWhenTrue.Finished.AddListener(Conclude);
                runner.RunEvents(SequenceWhenTrue);
            }
            else
            {
                SequenceWhenFalse.Finished.AddListener(Conclude);
                runner.RunEvents(SequenceWhenFalse);
            }
        }

        protected void Conclude()
        {
            Finished.Invoke();
            IsFinished = true;
        }
    }
}
