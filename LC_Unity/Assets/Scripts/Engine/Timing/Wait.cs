using Engine.Events;
using UnityEngine.Events;

namespace Engine.Timing
{
    public class Wait : IRunnable
    {
        public int Duration { get; set; }
        public UnityEvent Finished { get; set; }

        public Wait()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {

        }
    }
}
