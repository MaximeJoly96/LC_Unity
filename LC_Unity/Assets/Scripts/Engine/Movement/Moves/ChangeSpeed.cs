using Field;

namespace Engine.Movement.Moves
{
    public class ChangeSpeed : Move
    {
        public float Speed { get; set; }

        public override void Run(Agent agent)
        {
            agent.UpdateSpeed(Speed);
            IsFinished = true;
        }
    }
}
