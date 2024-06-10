using Engine.Events;
using UnityEngine.Events;

namespace Engine.SceneControl
{
    public class OpenSave : IRunnable
    {
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        public OpenSave()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {

        }

    }
}
