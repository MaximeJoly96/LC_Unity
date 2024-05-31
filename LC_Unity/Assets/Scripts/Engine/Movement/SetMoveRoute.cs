using Engine.Events;
using System.Collections.Generic;
using Engine.Movement.Moves;
using UnityEngine.Events;

namespace Engine.Movement
{
    public class SetMoveRoute : IRunnable
    {
        public bool RepeatAction { get; set; }
        public bool SkipIfCannotMove { get; set; }
        public bool WaitForCompletion { get; set; }
        public List<Move> Moves { get; private set; }
        public UnityEvent Finished { get; set; }

        public SetMoveRoute()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {

        }

        public void AddMove(Move move)
        {
            if (Moves == null)
                Moves = new List<Move>();

            Moves.Add(move);
        }
    }
}
