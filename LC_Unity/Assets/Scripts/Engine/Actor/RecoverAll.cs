using Engine.Events;
using UnityEngine.Events;

namespace Engine.Actor
{
    public class RecoverAll : IRunnable
    {
        public UnityEvent Finished { get; set; }

        public RecoverAll()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {

        }
    }
}
