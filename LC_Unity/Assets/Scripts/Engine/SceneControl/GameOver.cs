using Engine.Events;
using UnityEngine.Events;

namespace Engine.SceneControl
{
    public class GameOver : IRunnable
    {
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        public GameOver()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {

        }
    }
}
