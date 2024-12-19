using UnityEngine;
using System.Collections.Generic;
using Logging;
using Movement;
using System.Linq;
using Field.Routines;
using static UnityEngine.GraphicsBuffer;
using System.Collections;

namespace Field
{
    public enum Animosity
    {
        Passive,
        Aggressive,
        ReturnToRoutine
    }

    public class AggressiveBehaviour : MonoBehaviour
    {
        private const float SPACE_BETWEEN_RAYS = 5.0f; // degrees

        [SerializeField]
        private float _fieldOfView; // degrees
        [SerializeField]
        private float _maxAggroDistance; // world units
        [SerializeField]
        private float _maxChasingDistance; // world units

        private List<float> _anglesForRays;
        private List<Ray2D> _rays;
        private float _currentChaseDistance;
        private Vector3 _previousPosition;
        private Vector3 _interruptionPosition;
        private Transform _target;

        public List<float> AnglesForRays { get { return _anglesForRays; } }
        public float FieldOfView { get { return _fieldOfView; } set { _fieldOfView = value; } }
        public float MaxAggroDistance { get { return _maxAggroDistance; } set { _maxAggroDistance = value; } }
        public Agent Agent { get { return GetComponent<Agent>(); } }
        public Animosity Animosity { get; set; }

        private void Start()
        {
            ComputeRays();
        }

        private void FixedUpdate()
        {
            DrawRays();
            CheckRays();

            switch (Animosity)
            {
                case Animosity.Passive:
                    break;
                case Animosity.Aggressive:
                    ChaseTarget(_target);
                    break;
                case Animosity.ReturnToRoutine:
                    if (Vector2.Distance(transform.position, _interruptionPosition) > 0.05f)
                        ReturnToPosition(_interruptionPosition);
                    else
                        ResumeRoutine();
                    break;
            }
        }

        private void Update()
        {
            if(Animosity == Animosity.Aggressive)
            {
                _currentChaseDistance += Vector2.Distance(_previousPosition, transform.position);
                _previousPosition = transform.position;

                if (_currentChaseDistance >= _maxChasingDistance)
                    StopChasing();
            }
        }

        private void ComputeRays()
        {
            if(_fieldOfView <= 0.0f)
            {
                LogsHandler.Instance.LogError("Provided field of view for " + name + " is 0 or negative. This is not allowed.");
                return;
            }

            int steps = Mathf.RoundToInt(_fieldOfView / SPACE_BETWEEN_RAYS);
            _anglesForRays = new List<float>();

            for(int i = 0; i <= steps; i++)
            {
                _anglesForRays.Add(Mathf.Lerp(0.0f, _fieldOfView, (float)i / steps));
            }
        }

        private void DrawRays()
        {
            float currentDirectionAngle = DirectionUtils.DirectionToAngle(Agent.CurrentDirection);
            float lowerBound = currentDirectionAngle - _fieldOfView / 2.0f;
            _rays = new List<Ray2D>();

            for(int i = 0; i < _anglesForRays.Count; i++)
            {
                Ray2D ray = new Ray2D(transform.position, new Vector2(Mathf.Cos(Mathf.Deg2Rad * (lowerBound + _anglesForRays[i])),
                                                                      Mathf.Sin(Mathf.Deg2Rad * (lowerBound + _anglesForRays[i]))));

                _rays.Add(ray);
                Debug.DrawLine(ray.origin, ray.GetPoint(_maxAggroDistance));
            }
        }

        private void CheckRays()
        {
            RaycastHit2D[] hits = new RaycastHit2D[20];

            for (int i = 0; i < _rays.Count && Animosity == Animosity.Passive; i++)
            {
                if(Physics2D.RaycastNonAlloc(_rays[i].origin, _rays[i].direction, hits, _maxAggroDistance) > 0)
                {
                    RaycastHit2D foundPlayer = hits.FirstOrDefault(h => h.collider && h.collider.GetComponent<PlayerController>());

                    if(foundPlayer.collider)
                    {
                        PlayerController pc = foundPlayer.collider.GetComponent<PlayerController>();
                        if (pc)
                        {
                            Animosity = Animosity.Aggressive;
                            GetComponent<RoutineRunner>().Interrupt();
                            _currentChaseDistance = 0.0f;
                            _previousPosition = transform.position;
                            _interruptionPosition = transform.position;
                            _target = pc.transform;
                        }
                    }
                }
            }
        }

        private void ChaseTarget(Transform target)
        {
            ReturnToPosition(target.position);
        }

        private void StopChasing()
        {
            _target = null;
            _currentChaseDistance = 0.0f;
            Animosity = Animosity.ReturnToRoutine;
            DisableCollider();
        }

        private void ReturnToPosition(Vector3 position)
        {
            Vector2 delta = (position - transform.position).normalized * Time.deltaTime * Agent.Speed * 3.0f;

            GetComponent<Rigidbody2D>().MovePosition(transform.TransformPoint(delta));

            Animator animator = GetComponent<Animator>();
            animator.SetBool("Moving", true);
            animator.SetFloat("X", delta.x);
            animator.SetFloat("Y", delta.y);
        }

        private void ResumeRoutine()
        {
            EnableCollider();
            GetComponent<RoutineRunner>().Resume();

            StartCoroutine(SetAnimosityToPassiveWithDelay(1.0f));
        }

        private IEnumerator SetAnimosityToPassiveWithDelay(float delay)
        {
            yield return new WaitForSeconds(delay);

            Animosity = Animosity.Passive;
        }

        private void DisableCollider()
        {
            GetComponent<Collider2D>().enabled = false;

            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            Color currentColor = sr.color;
            sr.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0.25f);
        }

        private void EnableCollider()
        {
            GetComponent<Collider2D>().enabled = true;

            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            Color currentColor = sr.color;
            sr.color = new Color(currentColor.r, currentColor.g, currentColor.b, 1.0f);
        }
    }
}
