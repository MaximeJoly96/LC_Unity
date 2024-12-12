using System.Collections.Generic;
using Engine.Movement.Moves;

namespace Field.Routines
{
    public class AgentRoutine : List<Move>
    {
        public AgentRoutine(List<Move> moves)
        {
            foreach(Move move in moves)
            {
                this.Add(move);
            }
        }

        public AgentRoutine() { }
    }
}
