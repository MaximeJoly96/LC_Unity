using Field;

namespace Engine.Movement.Moves
{
    public abstract class Move
    {
        public bool IsFinished { get; set; }

        public abstract void Run(Agent agent);
    }
}
