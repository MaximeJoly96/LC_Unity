using UnityEngine;

namespace BattleSystem.UI
{
    public class TargetInfoCanvas : MonoBehaviour
    {
        [SerializeField]
        private TargetInfoWindow _targetInfoWindowPrefab;

        private TargetInfoWindow _currentTargetInfoWindow;

        public void ShowTargetInfo(BattlerBehaviour battler)
        {
            if(!_currentTargetInfoWindow)
                _currentTargetInfoWindow = Instantiate(_targetInfoWindowPrefab, transform);

            _currentTargetInfoWindow.transform.position = Camera.main.WorldToScreenPoint(battler.transform.position);
            _currentTargetInfoWindow.Feed(battler.BattlerData.Character);
        }

        public void Clear()
        {
            if (_currentTargetInfoWindow)
                Destroy(_currentTargetInfoWindow.gameObject);
        }
    }
}
