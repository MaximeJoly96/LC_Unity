using UnityEngine;

namespace Movement
{
    [RequireComponent(typeof (Animator))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class PlayerFollower : MonoBehaviour
    {
        public enum FollowerState
        {
            InRange,
            Following
        }

        private FollowerState _currentState;
        private PlayerController _playerController;
        private Animator _animator;

        [SerializeField]
        private float _maxDistanceBeforeFollowing; // world units
        [SerializeField]
        private float _minDistanceToStopFollowing; // world units
        [SerializeField]
        private float _followerSpeed = 2.0f;

        private float DistanceToPlayer
        {
            get { return Vector3.Distance(transform.position, _playerController.transform.position); }
        }

        private void Start ()
        {
            _currentState = FollowerState.InRange;
            _playerController = FindObjectOfType<PlayerController>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            switch(_currentState)
            {
                case FollowerState.InRange:
                    CheckDistanceFromPlayer();
                    break;
                case FollowerState.Following:
                    FollowPlayer();
                    break;
            }
        }

        private void CheckDistanceFromPlayer()
        {
            if(DistanceToPlayer > _maxDistanceBeforeFollowing)
            {
                _animator.SetBool("Moving", true);
                _currentState = FollowerState.Following;
            }   
        }

        private void FollowPlayer()
        {
            if(DistanceToPlayer > _minDistanceToStopFollowing)
            {
                Vector3 delta = (_playerController.transform.position - transform.position).normalized;
                transform.Translate(_followerSpeed * Time.deltaTime * delta);

                _animator.SetFloat("X", delta.x);
                _animator.SetFloat("Y", delta.y);
            }
            else
            {
                _animator.SetBool("Moving", false);
                _animator.SetFloat("X", 0.0f);
                _animator.SetFloat("Y", 0.0f);

                _currentState = FollowerState.InRange;
            }
        }
    }
}
