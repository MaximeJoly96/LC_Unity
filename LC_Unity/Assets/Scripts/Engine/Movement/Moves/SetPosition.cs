using Field;
using UnityEngine;

namespace Engine.Movement.Moves
{
    public class SetPosition : Move
    {
        public float X { get; set; }
        public float Y { get; set; }

        public override void Run(Agent agent)
        {
            agent.transform.localPosition = new Vector3(X, Y);

            IsFinished = true;
        }
    }
}
