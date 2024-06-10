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
        }

        public void Add(IRunnable evt)
        {
            if (Events == null)
                Events = new List<IRunnable>();

            Events.Add(evt);
        }
    }
}
