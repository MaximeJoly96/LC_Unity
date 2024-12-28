using UnityEngine;

namespace BattleSystem.Behaviours
{
    public class BattlerMoverBehaviour : MonoBehaviour
    {
        private const float MOVEMENT_SPEED = 5.0f;

        private Vector3 _direction;
        private float _distance;
        private bool _running;

        private float _travelledDistance;
        private Vector3 _previousPosition;

        public void Feed(Vector3 direction, float distance)
        {
            _direction = direction;
            _distance = distance;
            GetComponent<BattlerBehaviour>().TemporaryInterruption = true;

            _travelledDistance = 0.0f;
            _previousPosition = transform.position;
            _running = true;
        }

        private void Update()
        {
            if (_running)
            {
                transform.Translate(_direction * Time.deltaTime * MOVEMENT_SPEED);
                _travelledDistance += Vector3.Distance(transform.position, _previousPosition);
                _previousPosition = transform.position;

                if (_travelledDistance > _distance)
                {
                    _running = false;
                    GetComponent<BattlerBehaviour>().TemporaryInterruption = false;
                    Destroy(this);
                }
            }
        }
    }
}
