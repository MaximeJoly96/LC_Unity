using UnityEngine;
using LC_Unity.Movement;

namespace LC_Unity.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private PlayerController _playerController;
        [SerializeField]
        private UnityEngine.Camera _camera;

        private void Update()
        {
            _camera.transform.position = new Vector3(_playerController.Position.x,
                                                     _playerController.Position.y,
                                                     _camera.transform.position.z);
        }
    }
}