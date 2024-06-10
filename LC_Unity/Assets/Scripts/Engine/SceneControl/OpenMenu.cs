using Engine.Events;
using UnityEngine.Events;

namespace Engine.SceneControl
{
    public class OpenMenu : IRunnable
    {
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        public OpenMenu()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {

        }
    }
}
