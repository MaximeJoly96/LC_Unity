using Field;
using UnityEngine.Events;
using UnityEngine;
using System;

namespace Movement
{
    public class AgentMoverWithTarget : AgentMover
    {
        private float _distanceToTravel;
        private float _travelledDistance;

        public void StartMoving(Agent target, float distance)
        {
            DestinationReached = new UnityEvent();

            _delta = target.transform.position - transform.position;

            if(distance < 0.0f)
                _delta *= -1.0f;

            _distanceToTravel = Math.Abs(distance);
            _travelledDistance = 0.0f;

            Moving = true;
        }

        protected override void Move()
        {
            Vector3 move = _delta * Time.deltaTime * Speed;
            transform.Translate(move);

            _travelledDistance += move.magnitude;

            Animator.SetFloat("X", _delta.x);
            Animator.SetFloat("Y", _delta.y);

            if (_travelledDistance >= _distanceToTravel)
            {
                DestinationReached.Invoke();
                Moving = false;
                Stop();

                Destroy(this);
            }
            else
                Animator.SetBool("Moving", true);
        }
    }
}
