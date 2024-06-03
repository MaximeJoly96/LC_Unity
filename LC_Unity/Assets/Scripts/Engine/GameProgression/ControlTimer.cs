namespace Engine.GameProgression
{
    public class ControlTimer : PersistentData
    {
        public enum TimerAction { Start, Stop }

        public TimerAction Action { get; set; }
        public int Duration { get; set; } // seconds

        public override void Run()
        {

        }
    }
}
