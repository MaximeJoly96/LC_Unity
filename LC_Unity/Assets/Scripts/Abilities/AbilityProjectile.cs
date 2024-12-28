using BattleSystem;
using BattleSystem.Model;
using MusicAndSounds;
using UnityEngine;
using UnityEngine.Events;

namespace Abilities
{
    public class AbilityProjectile : MonoBehaviour
    {
        [SerializeField]
        private float _speed;
        [SerializeField]
        private int _id;

        private ProjectileTrajectory _trajectory;
        private bool _moving;
        private int _currentCheckpointId;
        private Vector3 _currentCheckpoint;
        private UnityEvent<BattlerBehaviour> _projectileDestroyed;

        public float Speed { get { return _speed; } }
        public ProjectileTrajectory Trajectory { get { return _trajectory; } }
        public int Id { get { return _id; } }
        public BattlerBehaviour OriginBattler { get; set; }
        public TargetEligibility TargetEligibility { get; set; }
        public UnityEvent<BattlerBehaviour> ProjectileDestroyed
        {
            get
            {
                if (_projectileDestroyed == null)
                    _projectileDestroyed = new UnityEvent<BattlerBehaviour>();

                return _projectileDestroyed;
            }
        }

        public void SetSpeed(float speed)
        {
            _speed = speed;
        }

        public void SetTrajectory(ProjectileTrajectory trajectory)
        {
            _trajectory = trajectory;
            _currentCheckpoint = _trajectory.Checkpoints[_currentCheckpointId];
        }

        public void StartMoving()
        {
            _moving = true;
        }

        public void StopMoving(BattlerBehaviour target)
        {
            ProjectileDestroyed.Invoke(target);
            _moving = false;
            Destroy(gameObject);
        }

        public void StopMoving()
        {
            StopMoving(null);
        }

        private void Update()
        {
            if (_moving)
            {
                Vector3 delta = (_currentCheckpoint - transform.position).normalized;
                transform.Translate(delta * Time.deltaTime * _speed);

                if(Vector3.Distance(transform.position, _currentCheckpoint) < 0.05f)
                {
                    if (_currentCheckpointId < _trajectory.Checkpoints.Count - 1)
                    {
                        _currentCheckpointId++;
                        _currentCheckpoint = _trajectory.Checkpoints[_currentCheckpointId];
                    }
                    else
                        StopMoving();
                }
            }
        }

        protected virtual void FixedUpdate()
        {
            Collider2D[] hits = new Collider2D[10];

            if(Physics2D.OverlapCircleNonAlloc(gameObject.transform.position, GetComponent<CircleCollider2D>().radius, hits) > 0)
            {
                for(int i = 0; i < hits.Length; i++)
                {
                    if(hits[i] != null)
                    {
                        BattlerBehaviour battler = hits[i].transform.GetComponent<BattlerBehaviour>();

                        if(battler && IsTargetEligibleForProjectile(battler))
                        {
                            StopMoving(battler);
                        }
                    }
                }
            }
        }

        private bool IsTargetEligibleForProjectile(BattlerBehaviour target)
        {
            switch(TargetEligibility)
            {
                case TargetEligibility.Any:
                case TargetEligibility.All:
                    return true;
                case TargetEligibility.Self:
                    break;
                case TargetEligibility.Ally:
                case TargetEligibility.AllAllies:
                    return target.IsEnemy == OriginBattler.IsEnemy;
                case TargetEligibility.Enemy:
                case TargetEligibility.AllEnemies:
                    return target.IsEnemy != OriginBattler.IsEnemy;
                case TargetEligibility.AnyExceptSelf:
                    return target != OriginBattler;
            }

            return false;
        }
    }
}
