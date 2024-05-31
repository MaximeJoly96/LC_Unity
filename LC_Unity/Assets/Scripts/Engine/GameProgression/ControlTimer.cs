using Engine.Events;
using UnityEngine.Events;

namespace Engine.GameProgression
{
    public class ControlTimer : IRunnable
    {
        public enum TimerAction { Start, Stop }

        public TimerAction Action { get; set; }
        public int Duration { get; set; } // seconds
        public UnityEvent Finished { get; set; }

        public ControlTimer()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {

        }
    }
}
