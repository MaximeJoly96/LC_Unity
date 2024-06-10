using System.Collections.Generic;
using UnityEngine.Events;

namespace Engine.Events
{
    public class EventsSequence
    {
        public List<IRunnable> Events { get; private set; }
        public UnityEvent Finished { get; set; }

        public EventsSequence()
        {
            Finished = new UnityEvent();
            Events = new List<IRunnable>();
        }

        public void Add(IRunnable evt)
        {
            Events.Add(evt);
        }
    }
}
