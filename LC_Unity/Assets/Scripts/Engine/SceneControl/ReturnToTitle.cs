using Engine.Events;
using UnityEngine.Events;

namespace Engine.SceneControl
{
    public class ReturnToTitle : IRunnable
    {
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        public ReturnToTitle()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {

        }
    }
}
