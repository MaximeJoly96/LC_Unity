using Field;
using Movement;

namespace Engine.Movement.Moves
{
    public class Turn : Move
    {
        public Direction Direction { get; set; }

        public override void Run(Agent agent)
        {
            agent.UpdateDirection(Direction);
            IsFinished = true;
        }
    }
}
