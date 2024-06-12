using Engine.Events;
using UnityEngine.Events;
using UnityEngine;
using Timing;

namespace Engine.Timing
{
    public class Wait : IRunnable
    {
        public float Duration { get; set; }
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        public Wait()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {
            Object.FindObjectOfType<Waiter>().Wait(this);
        }
    }
}
