using Field;
using Movement;

namespace Engine.Movement.Moves
{
    public class StepForward : Move
    {
        public override void Run(Agent agent)
        {
            IsFinished = false;

            AgentMover mover = agent.gameObject.AddComponent<AgentMover>();
            mover.StartMoving(DirectionUtils.DirectionToNormalizedVector(agent.CurrentDirection));
            mover.DestinationReached.AddListener(() => IsFinished = true);
        }
    }
}
