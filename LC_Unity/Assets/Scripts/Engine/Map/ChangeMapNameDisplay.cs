using Engine.Events;
using UnityEngine.Events;

namespace Engine.Map
{
    public class ChangeMapNameDisplay : IRunnable
    {
        public bool Enabled { get; set; }
        public UnityEvent Finished { get; set; }

        public ChangeMapNameDisplay()
        {

        }

        public void Run()
        {

        }
    }
}
