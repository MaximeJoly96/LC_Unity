using Engine.Events;

namespace Engine.GameProgression
{
    public class ControlTimer : IRunnable
    {
        public enum TimerAction { Start, Stop }

        public TimerAction Action { get; set; }
        public int Duration { get; set; } // seconds

        public void Run()
        {

        }
    }
}
