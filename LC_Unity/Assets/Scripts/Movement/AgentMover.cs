using UnityEngine;
using UnityEngine.Events;
using Field;

namespace Movement
{
    public class AgentMover : MonoBehaviour
    {
        protected Vector3 _destination;
        protected Vector3 _delta;
        protected Animator _animator;

        public float Speed { get { return GetComponent<Agent>().Speed; } }
        public bool Moving { get; protected set; }
        public UnityEvent DestinationReached { get; protected set; }
        protected Animator Animator
        {
            get
            {
                if(!_animator)
                    _animator = GetComponent<Animator>();

                return _animator;
            }
        }

        public void StartMoving(float deltaX, float deltaY)
        {
            StartMoving(new Vector2(deltaX, deltaY));
        }

        public void StartMoving(Vector2 delta)
        {
            _delta = delta;
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
            transform.Translate(_delta.normalized * Time.deltaTime * Speed);

            Animator.SetFloat("X", _delta.x);
            Animator.SetFloat("Y", _delta.y);

            if (Vector3.Distance(transform.position, _destination) < 0.05f)
            {
                DestinationReached.Invoke();
                Moving = false;
                Stop();
               
                Destroy(this);
            }
            else
                Animator.SetBool("Moving", true);
        }

        protected void Stop()
        {
            Animator.SetBool("Moving", false);
            Animator.Play("Idle");
        }
    }
}
