using Engine.Events;
using UnityEngine.Events;
using Actors;

namespace Engine.Actor
{
    public class RecoverAll : IRunnable
    {
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        public RecoverAll()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {
            CharactersManager.Instance.RecoverAll(this);
            Finished.Invoke();
            IsFinished = true;
        }
    }
}
