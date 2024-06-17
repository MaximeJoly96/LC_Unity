using Field;
using UnityEngine;
using Timing;

namespace Engine.Movement.Moves
{
    public class Wait : Move
    {
        public float Duration { get; set; }

        public override void Run(Agent agent)
        {
            Timing.Wait wait = new Timing.Wait();
            wait.Duration = Duration;
            wait.Finished.AddListener(() => IsFinished = true);
            Object.FindObjectOfType<Waiter>().Wait(wait);
        }
    }
}
