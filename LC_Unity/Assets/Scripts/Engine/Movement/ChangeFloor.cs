using Engine.Events;
using UnityEngine.Events;
using UnityEngine;
using Field;

namespace Engine.Movement
{
    public class ChangeFloor : IRunnable
    {
        public bool Up { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        public ChangeFloor()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {
            Object.FindObjectOfType<FieldBuilder>().ChangeFloor(this);

            Finished.Invoke();
            IsFinished = true;
        }
    }
}
