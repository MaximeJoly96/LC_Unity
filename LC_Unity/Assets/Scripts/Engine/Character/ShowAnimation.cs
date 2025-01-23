using Engine.Events;
using UnityEngine.Events;
using UnityEngine;
using Field;

namespace Engine.Character
{
    public class ShowAnimation : IRunnable
    {
        public string Target { get; set; }
        public int AnimationId { get; set; }
        public bool WaitForCompletion { get; set; }
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        public ShowAnimation()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {
            AgentsManager.Instance.PlayAnimationOnAgent(this);

            Finished.Invoke();
            IsFinished = true;
        }
    }
}
