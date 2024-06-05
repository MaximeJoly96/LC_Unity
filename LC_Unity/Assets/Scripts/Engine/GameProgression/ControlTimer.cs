using GameProgression;
using UnityEngine;

namespace Engine.GameProgression
{
    public class ControlTimer : PersistentData
    {
        public enum TimerAction { Start, Stop }

        public TimerAction Action { get; set; }
        public int Duration { get; set; } // seconds

        public override void Run()
        {
            TimersManager manager = Object.FindObjectOfType<TimersManager>();

            switch(Action)
            {
                case TimerAction.Start:
                    manager.AddTimer(this);
                    manager.StartTimer(Key);
                    break;
                case TimerAction.Stop:
                    manager.StopTimer(Key);
                    break;
            }

            Finished.Invoke();
        }
    }
}
