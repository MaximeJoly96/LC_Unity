using Engine.Events;
using UnityEngine;
using UnityEngine.Events;

namespace Engine.FlowControl
{
    public abstract class BasicCondition : IRunnable
    {
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        public EventsSequence SequenceWhenTrue { get; set; }
        public EventsSequence SequenceWhenFalse { get; set; }

        public abstract void Run();

        protected BasicCondition()
        {
            Finished = new UnityEvent();
        }

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
