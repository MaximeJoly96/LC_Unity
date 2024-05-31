using Engine.Events;
using UnityEngine.Events;

namespace Engine.GameProgression
{
    public class ControlSwitch : IRunnable
    {
        public bool Value { get; set; }
        public UnityEvent Finished { get; set; }

        public ControlSwitch()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {

        }
    }
}
