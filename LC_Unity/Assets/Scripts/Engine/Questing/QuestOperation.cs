using Engine.Events;
using UnityEngine.Events;

namespace Engine.Questing
{
    public abstract class QuestOperation : IRunnable
    {
        public int Id { get; set; }
        public bool IsFinished { get; set; }
        public UnityEvent Finished { get; set; }

        public abstract void Run();

        protected QuestOperation()
        {
            Finished = new UnityEvent();
        }
    }
}
