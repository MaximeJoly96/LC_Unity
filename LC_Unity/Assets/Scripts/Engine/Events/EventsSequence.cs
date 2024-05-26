using System.Collections.Generic;

namespace Engine.Events
{
    public class EventsSequence
    {
        public List<IRunnable> Events { get; private set; }

        public void Add(IRunnable evt)
        {
            if (Events == null)
                Events = new List<IRunnable>();

            Events.Add(evt);
        }

        public void Run()
        {
            for (int i = 0; i < Events.Count; i++)
                Events[i].Run();
        }
    }
}
