using Field;
using Movement;

namespace Engine.Movement.Moves
{
    public class JumpRelative : Move
    {
        public float DeltaX { get; set; }
        public float DeltaY { get; set; }

        public override void Run(Agent agent)
        {
            IsFinished = false;

            AgentJumper jumper = agent.gameObject.AddComponent<AgentJumper>();
            jumper.StartJumping(DeltaX, DeltaY);
            jumper.DestinationReached.AddListener(() => IsFinished = true);
        }
    }
}
