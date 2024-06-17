using Field;
using Movement;

namespace Engine.Movement.Moves
{
    public class StepBackward : Move
    {
        public override void Run(Agent agent)
        {
            Direction oppositionDir = DirectionUtils.GetOppositeDirection(agent.CurrentDirection);

            IsFinished = false;

            AgentMover mover = agent.gameObject.AddComponent<AgentMover>();
            agent.FixedDirection = true;
            mover.StartMoving(DirectionUtils.DirectionToNormalizedVector(oppositionDir));
            mover.DestinationReached.AddListener(() =>
            {
                agent.FixedDirection = false;
                IsFinished = true;
            });
        }
    }
}
