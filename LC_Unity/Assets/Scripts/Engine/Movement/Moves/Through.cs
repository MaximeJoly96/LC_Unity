using Field;

namespace Engine.Movement.Moves
{
    public class Through : Move
    {
        public bool On { get; set; }

        public override void Run(Agent agent)
        {
            agent.GoesThrough = On;

            IsFinished = true;
        }
    }
}
