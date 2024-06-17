using Field;
using Movement;

namespace Engine.Movement.Moves
{
    public class TurnRelative : Move
    {
        public int Angle { get; set; }

        public override void Run(Agent agent)
        {
            agent.UpdateDirection(DirectionUtils.ApplyAngleToDirection(Angle, agent.CurrentDirection));
            IsFinished = true;
        }
    }
}
