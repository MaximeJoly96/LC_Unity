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
        [SerializeField]
        private Transform _balloonIconSpot;

        public string Id { get { return _id; } }
        public float Speed { get { return _speed; } }
        public Direction CurrentDirection { get { return _currentDirection; } }
        public bool FixedDirection { get; set; }
        public bool GoesThrough { get; set; }
        public bool WalkingAnimationActive { get; set; }
        public Vector2 BalloonIconSpot
        {
            get
            {
                return _balloonIconSpot ? _balloonIconSpot.position : Vector2.zero;
            }
        }

        public void UpdateSpeed(float speed)
        {
            _speed = speed;
        }

        public void UpdateDirection(Direction direction)
        {
            if(!FixedDirection)
            {
                _currentDirection = direction;
                Animator animator = GetComponent<Animator>();

                switch(direction)
                {
                    case Direction.Left:
                        animator.SetFloat("X", -1.0f);
                        break;
                    case Direction.Right:
                        animator.SetFloat("X", 1.0f);
                        break;
                    case Direction.Up:
                        animator.SetFloat("Y", 1.0f);
                        break;
                    case Direction.Down:
                        animator.SetFloat("Y", -1.0f);
                        break;
                }
            }  
        }
    }
}
