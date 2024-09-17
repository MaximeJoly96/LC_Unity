using UnityEngine;
using UnityEngine.Events;

namespace Movement
{
    public class AgentJumper : AgentMover
    {
        public void StartJumping(float deltaX, float deltaY)
        {
            _delta = new Vector3(deltaX, deltaY);
            _destination = transform.position + _delta;

            DestinationReached = new UnityEvent();

            Moving = true;
        }

        protected override void Move()
        {
            // play animation and parameter the destination
        }
    }
}
