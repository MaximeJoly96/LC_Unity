using Field;
using Movement;
using System;

namespace Engine.Movement.Moves
{
    public class MoveAway : Move
    {
        public float Distance { get; set; }
        public string Target { get; set; }

        public override void Run(Agent agent)
        {
            IsFinished = false;

            Agent target = AgentsManager.Instance.GetAgent(Target);

            AgentMoverWithTarget mover = agent.gameObject.AddComponent<AgentMoverWithTarget>();
            mover.StartMoving(target, Math.Abs(Distance) * -1.0f);
            mover.DestinationReached.AddListener(() => IsFinished = true);
        }
    }
}
