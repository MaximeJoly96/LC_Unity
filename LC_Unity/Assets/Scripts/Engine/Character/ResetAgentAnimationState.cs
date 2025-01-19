using Engine.Events;
using Field;
using UnityEngine.Events;

namespace Engine.Character
{
    public class ResetAgentAnimationState : IRunnable
    {
        public bool IsFinished { get; set; }
        public UnityEvent Finished { get; set; }
        public string Target { get; set; }

        public ResetAgentAnimationState()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {
            AgentsManager.Instance.ResetAgentAnimation(this);
            Finished.Invoke();
            IsFinished = true;
        }
    }
}
