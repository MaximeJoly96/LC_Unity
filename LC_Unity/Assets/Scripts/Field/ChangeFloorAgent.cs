using Engine.Movement;
using UnityEngine;

namespace Field
{
    public class ChangeFloorAgent : RunnableAgent
    {
        [SerializeField]
        private bool _goesUp;
        [SerializeField]
        private Vector2 _newPosition;

        public override void RunSequence()
        {
            ChangeFloor change = new ChangeFloor { Up = _goesUp, X = _newPosition.x, Y = _newPosition.y };

            change.Run();
            Runner.Finished.Invoke();
        }
    }
}
