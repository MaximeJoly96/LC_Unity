using Field;
using Movement;
using UnityEngine;

namespace Engine.Movement.Moves
{
    public class TurnTowards : Move
    {
        public string Target { get; set; }

        public override void Run(Agent agent)
        {
            Agent target = AgentsManager.Instance.GetAgent(Target);

            Vector3 difference = (target.transform.position - agent.transform.position).normalized;
            agent.UpdateDirection(DirectionUtils.VectorToDirection(difference));
            IsFinished = true;
        }
    }
}
