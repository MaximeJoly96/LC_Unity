using UnityEngine;
using Logging;
using Engine.Movement;
using System.Collections;

namespace Movement
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField]
        private PlayerController _playerController;
        [SerializeField]
        private Camera _camera;

        private bool _followingPlayer;

        private void Awake()
        {
            if (!_playerController)
            {
                _playerController = FindObjectOfType<PlayerController>();
            }

            if(!_camera)
                LogsHandler.Instance.LogError("No Camera attached to CameraFollower. Camera will not follow the player.");

            _followingPlayer = true;
        }

        private void Update()
        {
            if(_followingPlayer && _playerController && _camera)
            {
                _camera.transform.position = new Vector3(_playerController.transform.position.x,
                                                         _playerController.transform.position.y,
                                                         _camera.transform.position.z);
            }
        }

        public void Move(ScrollMap scrollData)
        {
            _followingPlayer = false;

            StartCoroutine(DoScrollMap(scrollData));
        }

        private IEnumerator DoScrollMap(ScrollMap scrollData)
        {
            Vector3 direction = new Vector3(scrollData.X, scrollData.Y);
            Vector2 target = _camera.transform.position + direction;

            scrollData.Finished.Invoke();
            scrollData.IsFinished = true;

            while (Vector2.Distance(_camera.transform.position, target) > 0.01f)
            {
                _camera.transform.Translate(direction * Time.deltaTime * scrollData.Speed);
                yield return null;
            }
        }

        public void FollowPlayer(bool follow)
        {
            _followingPlayer = follow;
        }
    }
}
