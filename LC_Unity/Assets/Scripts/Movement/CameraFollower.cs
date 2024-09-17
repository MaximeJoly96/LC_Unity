using UnityEngine;
using Logging;

namespace Movement
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField]
        private PlayerController _playerController;
        [SerializeField]
        private Camera _camera;

        private void Awake()
        {
            if (!_playerController)
            {
                _playerController = FindObjectOfType<PlayerController>();
            }

            if(!_camera)
                LogsHandler.Instance.LogError("No Camera attached to CameraFollower. Camera will not follow the player.");
        }

        private void Update()
        {
            if(_playerController && _camera)
            {
                _camera.transform.position = new Vector3(_playerController.transform.position.x,
                                                         _playerController.transform.position.y,
                                                         _camera.transform.position.z);
            }
        }
    }
}
