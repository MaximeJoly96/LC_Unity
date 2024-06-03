using Engine.Events;
using UnityEngine.Events;

namespace Engine.GameProgression
{
    public abstract class PersistentData : IRunnable
    {
        public string Key { get; set; }
        public string Source { get; set; }
        public UnityEvent Finished { get; set; }

        protected PersistentData()
        {
            Finished = new UnityEvent();
        }

        public abstract void Run();
    }
}
