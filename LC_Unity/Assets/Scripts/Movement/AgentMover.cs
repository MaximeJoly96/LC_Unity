using UnityEngine;
using UnityEngine.Events;
using Field;

namespace Movement
{
    public class AgentMover : MonoBehaviour
    {
        protected Vector3 _destination;
        protected Vector3 _delta;

        public float Speed { get { return GetComponent<Agent>().Speed; } }
        public bool Moving { get; protected set; }
        public UnityEvent DestinationReached { get; protected set; }

        public void StartMoving(float deltaX, float deltaY)
        {
            StartMoving(new Vector2(deltaX, deltaY));
        }

        public void StartMoving(Vector2 delta)
        {
            _delta = delta;
            _destination = transform.position + _delta;
            UpdateAgentDirection();

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

        protected void UpdateAgentDirection()
        {
            GetComponent<Agent>().UpdateDirection(DirectionUtils.VectorToDirection(_delta));
        }
    }
}
