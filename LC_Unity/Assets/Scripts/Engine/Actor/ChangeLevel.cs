using Engine.Events;
using UnityEngine.Events;

namespace Engine.Actor
{
    public class ChangeLevel : IRunnable
    {
        public int TargetCount { get; set; }
        public int Amount { get; set; }
        public UnityEvent Finished { get; set; }

        public ChangeLevel()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {

        }
    }
}
