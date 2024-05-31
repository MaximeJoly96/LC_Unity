using Engine.Events;
using UnityEngine.Events;

namespace Engine.FlowControl
{
    public abstract class ConditionalBranch : IRunnable
    {
        public UnityEvent Finished { get; set; }

        protected ConditionalBranch()
        {
            Finished = new UnityEvent();
        }

        public abstract void Run();
    }
}
