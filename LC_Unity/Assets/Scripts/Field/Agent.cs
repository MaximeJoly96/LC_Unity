using UnityEngine;
using Movement;

namespace Field
{
    public class Agent : MonoBehaviour
    {
        [SerializeField]
        private string _id;
        [SerializeField]
        private float _speed;
        [SerializeField]
        private Direction _currentDirection;

        public string Id { get { return _id; } }
        public float Speed { get { return _speed; } }
        public Direction CurrentDirection { get { return _currentDirection; } }
        public bool FixedDirection { get; set; }
        public bool GoesThrough { get; set; }
        public bool WalkingAnimationActive { get; set; }

        public void UpdateSpeed(float speed)
        {
            _speed = speed;
        }

        public void UpdateDirection(Direction direction)
        {
            if(!FixedDirection)
                _currentDirection = direction;
        }
    }
}
