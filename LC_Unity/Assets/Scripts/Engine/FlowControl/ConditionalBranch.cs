﻿using Engine.Events;
using UnityEngine.Events;

namespace Engine.FlowControl
{
    public abstract class ConditionalBranch : IRunnable
    {
        public string FirstMember { get; set; }
        public string SecondMember { get; set; }
        public UnityEvent Finished { get; set; }

        public EventsSequence SequenceWhenTrue { get; set; }
        public EventsSequence SequenceWhenFalse { get; set; }

        protected ConditionalBranch()
        {
            Finished = new UnityEvent();
        }

        public abstract void Run();
    }
}
