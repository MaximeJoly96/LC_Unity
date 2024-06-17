using Field;

namespace Engine.Movement.Moves
{
    public class TurnAway : Move
    {
        public string Target { get; set; }

        public override void Run(Agent agent)
        {
            Agent target = AgentsManager.Instance.GetAgent(Target);
            agent.UpdateDirection(target.CurrentDirection);
            IsFinished = true;
        }
    }
}
