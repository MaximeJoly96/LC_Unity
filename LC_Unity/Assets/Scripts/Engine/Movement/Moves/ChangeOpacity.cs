using Field;
using UnityEngine;

namespace Engine.Movement.Moves
{
    public class ChangeOpacity : Move
    {
        public float Alpha { get; set; }

        public override void Run(Agent agent)
        {
            SpriteRenderer sr = agent.GetComponent<SpriteRenderer>();
            Color color = sr.color;
            color.a = Alpha;
            sr.color = color;

            IsFinished = true;
        }
    }
}
