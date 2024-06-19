using Engine.Events;
using UnityEngine.Events;

namespace Engine.Actor
{
    public class ChangeExp : IRunnable
    {
        public int CharacterId { get; set; }
        public int Amount { get; set; }
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        public ChangeExp()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {
            Finished.Invoke();
            IsFinished = true;
        }
    }
}
