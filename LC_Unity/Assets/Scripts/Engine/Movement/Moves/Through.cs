using Field;
using UnityEngine;

namespace Engine.Movement.Moves
{
    public class Through : Move
    {
        public bool On { get; set; }

        public override void Run(Agent agent)
        {
            agent.GoesThrough = On;
            agent.GetComponent<Collider2D>().isTrigger = On;

            IsFinished = true;
        }
    }
}
