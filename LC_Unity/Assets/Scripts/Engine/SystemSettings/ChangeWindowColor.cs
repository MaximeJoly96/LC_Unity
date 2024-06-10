using Engine.Events;
using UnityEngine;
using UnityEngine.Events;

namespace Engine.SystemSettings
{
    public class ChangeWindowColor : IRunnable
    {
        public Color TargetColor { get; set; }
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        public ChangeWindowColor()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {

        }
    }
}
