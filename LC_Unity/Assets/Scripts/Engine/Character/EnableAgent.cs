using UnityEngine.Events;
using Engine.Events;
using Field;

namespace Engine.Character
{
    public class EnableAgent : IRunnable
    {
        public string Target { get; set; }
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        public EnableAgent()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {
            Agent agent = AgentsManager.Instance.GetAgent(Target);
            agent.EnableAgent();

            Finished.Invoke();
            IsFinished = true;
        }
    }
}
