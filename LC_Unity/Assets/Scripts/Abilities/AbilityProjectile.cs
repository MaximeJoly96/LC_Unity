﻿using BattleSystem;
using BattleSystem.Model;
using MusicAndSounds;
using UnityEngine;
using UnityEngine.Events;

namespace Abilities
{
    public class AbilityProjectile : MonoBehaviour
    {
        private const int MIN_LIFETIME = 3; // frames

        [SerializeField]
        private float _speed;
        [SerializeField]
        private int _id;

        private ProjectileTrajectory _trajectory;
        private bool _moving;
        private int _currentCheckpointId;
        private Vector3 _currentCheckpoint;
        private UnityEvent<BattlerBehaviour> _projectileDestroyed;
        private int _lifeTime;
        private bool _needsDirection;
        private Animator _animator;

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
            _needsDirection = CheckIfDirectionIsNeeded();

            _moving = true;
            _lifeTime = 0;
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
                UpdateProjectileDirection(delta);

                if(Vector3.Distance(transform.position, _currentCheckpoint) < 0.05f && _lifeTime >= MIN_LIFETIME)
                {
                    if (_currentCheckpointId < _trajectory.Checkpoints.Count - 1)
                    {
                        _currentCheckpointId++;
                        _currentCheckpoint = _trajectory.Checkpoints[_currentCheckpointId];
                    }
                    else
                        StopMoving();
                }

                _lifeTime++;
            }
        }

        private bool CheckIfDirectionIsNeeded()
        {
            _animator = GetComponent<Animator>();

            if (_animator)
            {
                AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
                return stateInfo.IsName("Shot");
            }

            return false;
        }

        private void UpdateProjectileDirection(Vector3 delta)
        {
            if (_needsDirection)
            {
                _animator.SetFloat("X", delta.x);
                _animator.SetFloat("Y", delta.y);
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
                    return OriginBattler.LockedInAbility.Targets.Contains(OriginBattler) ? true : target != OriginBattler;
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
