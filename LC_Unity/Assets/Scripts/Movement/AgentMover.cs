using UnityEngine;
using UnityEngine.Events;

namespace Movement
{
    public class AgentMover : MonoBehaviour
    {
        private Vector3 _destination;
        protected Vector3 _delta;

        public float Speed { get; set; } = 1.0f;
        public bool Moving { get; protected set; }
        public UnityEvent DestinationReached { get; protected set; }

        public void StartMoving(float deltaX, float deltaY)
        {
            _delta = new Vector3(deltaX, deltaY);
            _destination = transform.position + _delta;
            DestinationReached = new UnityEvent();

            Moving = true;
        }

        protected virtual void Update()
        {
            if(Moving)
            {
                Move();
            }
        }

        protected virtual void Move()
        {
            transform.Translate(_delta * Time.deltaTime * Speed);

            if (Vector3.Distance(transform.position, _destination) < 0.05f)
            {
                DestinationReached.Invoke();
                Moving = false;
                Destroy(this);
            }
        }
    }
}
