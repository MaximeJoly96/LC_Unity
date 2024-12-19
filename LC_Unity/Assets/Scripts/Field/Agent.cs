using UnityEngine;
using Movement;
using Engine.Movement;
using System.Linq;

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

        private bool _disabled;

        public bool Disabled
        {
            get { return _disabled; }
            private set
            {
                _disabled = value;

                Renderer renderer = GetComponent<Renderer>();
                if (renderer)
                    renderer.enabled = !value;

                Animator animator = GetComponent<Animator>();
                if(animator)
                    animator.enabled = !value;

                Collider2D collider2D = GetComponent<Collider2D>();
                if (collider2D)
                    collider2D.enabled = !value;
            }
        }
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

        // For unit testing purposes
        public void SetId(string id)
        {
            _id = id;
        }

        protected virtual void Start()
        {
            UpdateDirection(_currentDirection);
        }

        protected virtual void Update()
        {
            UpdateDirectionFromAnimator();
        }

        private void UpdateDirectionFromAnimator()
        {
            Animator animator = GetComponent<Animator>();
            if (animator)
            {
                Vector2 direction = new Vector2(animator.GetFloat("X"), animator.GetFloat("Y"));
                _currentDirection = DirectionUtils.VectorToDirection(direction);
            }
        }

        public void UpdateDirection(Direction direction)
        {
            if(!FixedDirection)
            {
                _currentDirection = direction;
                Animator animator = GetComponent<Animator>();

                if(animator && animator.parameters.Any(p => p.name == "X") && animator.parameters.Any(p => p.name == "Y"))
                {
                    switch (direction)
                    {
                        case Direction.Left:
                            animator.SetFloat("X", -1.0f);
                            animator.SetFloat("Y", 0.0f);
                            break;
                        case Direction.Right:
                            animator.SetFloat("X", 1.0f);
                            animator.SetFloat("Y", 0.0f);
                            break;
                        case Direction.Up:
                            animator.SetFloat("X", 0.0f);
                            animator.SetFloat("Y", 1.0f);
                            break;
                        case Direction.Down:
                            animator.SetFloat("X", 0.0f);
                            animator.SetFloat("Y", -1.0f);
                            break;
                    }
                }
            }  
        }

        public void UpdateDirection(TransferObject.PossibleDirection direction)
        {
            switch(direction)
            {
                case TransferObject.PossibleDirection.Left:
                    UpdateDirection(Direction.Left);
                    break;
                case TransferObject.PossibleDirection.Right:
                    UpdateDirection(Direction.Right);
                    break;
                case TransferObject.PossibleDirection.Top:
                    UpdateDirection(Direction.Up);
                    break;
                case TransferObject.PossibleDirection.Bottom:
                    UpdateDirection(Direction.Down);
                    break;
                case TransferObject.PossibleDirection.Retain:
                    break;
            }
        }

        public void DisableAgent()
        {
            Disabled = true;
        }

        public void EnableAgent()
        {
            Disabled = false;
        }
    }
}
