using Engine.Events;
using UnityEngine.Events;

namespace Engine.SystemSettings
{
    public abstract class ChangeAccess : IRunnable
    {
        public bool Enabled { get; set; }
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        protected ChangeAccess()
        {
            Finished = new UnityEvent();
        }

        public abstract void Run();
    }
}
