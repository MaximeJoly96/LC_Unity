using Engine.Events;
using UnityEngine.Events;
using Field;

namespace Engine.Movement
{
    public class EnterBuilding : IRunnable
    {
        public string AgentId { get; set; }
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        public EnterBuilding()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {
            (AgentsManager.Instance.GetAgent(AgentId) as Door).FinishedOpening();

            Finished.Invoke();
            IsFinished = true;
        }
    }
}
