using Field;
using Movement;

namespace Engine.Movement.Moves
{
    public class MoveRelative : Move
    {
        public float DeltaX { get; set; }
        public float DeltaY { get; set; }

        public override void Run(Agent agent)
        {
            IsFinished = false;

            AgentMover mover = agent.gameObject.AddComponent<AgentMover>();
            mover.StartMoving(DeltaX, DeltaY);
            mover.DestinationReached.AddListener(() => IsFinished = true);
        }
    }
}
