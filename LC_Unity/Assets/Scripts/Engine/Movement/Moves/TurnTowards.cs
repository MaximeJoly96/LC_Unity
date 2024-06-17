using Field;
using Movement;

namespace Engine.Movement.Moves
{
    public class TurnTowards : Move
    {
        public string Target { get; set; }

        public override void Run(Agent agent)
        {
            Agent target = AgentsManager.Instance.GetAgent(Target);
            Direction oppositeDirection = DirectionUtils.GetOppositeDirection(target.CurrentDirection);
            agent.UpdateDirection(oppositeDirection);
            IsFinished = true;
        }
    }
}
