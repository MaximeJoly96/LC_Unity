using UnityEngine;

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

        public float Speed { get { return _speed; } }
        public ProjectileTrajectory Trajectory { get { return _trajectory; } }
        public int Id { get { return _id; } }

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

        public void StopMoving()
        {
            _moving = false;
        }

        private void Update()
        {
            if (_moving)
            {
                transform.Translate(_currentCheckpoint * Time.deltaTime * _speed);

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
    }
}
