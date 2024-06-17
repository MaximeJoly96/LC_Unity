using UnityEngine;
using UnityEngine.Events;

namespace Movement
{
    public class AgentMover : MonoBehaviour
    {
        private Vector3 _destination;

        public Vector3 Delta { get; set; }
        public float Speed { get; set; } = 1.0f;
        public bool Moving { get; private set; }
        public UnityEvent DestinationReached { get; private set; }

        public void StartMoving(float deltaX, float deltaY)
        {
            Delta = new Vector3(deltaX, deltaY);
            _destination = transform.position + Delta;
            DestinationReached = new UnityEvent();

            Moving = true;
        }

        private void Update()
        {
            if(Moving)
            {
                transform.Translate(Delta * Time.deltaTime * Speed);

                if (Vector3.Distance(transform.position, _destination) < 0.05f)
                {
                    DestinationReached.Invoke();
                    Moving = false;
                    Destroy(this);
                }
            }
        }
    }
}
