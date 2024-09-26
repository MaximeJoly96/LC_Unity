using Engine.Events;
using Field;
using UnityEngine.Events;

namespace Engine.Character
{
    public class DisableAgent : IRunnable
    {
        public string Target { get; set; }
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        public DisableAgent()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {
            Agent agent = AgentsManager.Instance.GetAgent(Target);
            agent.DisableAgent();

            Finished.Invoke();
            IsFinished = true;
        }
    }
}
