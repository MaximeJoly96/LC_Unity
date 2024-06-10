using Engine.Events;
using UnityEngine.Events;

namespace Engine.Map
{
    public class ChangeMapNameDisplay : IRunnable
    {
        public bool Enabled { get; set; }
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        public ChangeMapNameDisplay()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {

        }
    }
}
