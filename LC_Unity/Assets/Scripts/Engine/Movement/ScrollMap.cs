using Engine.Events;
using UnityEngine.Events;
using UnityEngine;
using Movement;

namespace Engine.Movement
{
    public class ScrollMap : IRunnable
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Speed { get; set; }
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        public ScrollMap()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {
            Object.FindObjectOfType<CameraFollower>().Move(this);
        }
    }
}
