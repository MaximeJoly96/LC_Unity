using Actors;
using System.Collections.Generic;
using UnityEngine;

namespace BattleSystem.UI
{
    public class DamageDisplay : MonoBehaviour
    {
        [SerializeField]
        private Canvas _damageAndEffectsCanvas;
        [SerializeField]
        private DamageText _damageTextPrefab;

        [SerializeField]
        private Transform _displayedStatusWrapper;
        [SerializeField]
        private DisplayedStatusChange _displayedStatusChangePrefab;

        private Transform _instStatusWrapper;

        public void DisplayDamage(Vector3 worldPosition, int damage)
        {
            DamageText instDmgText = Instantiate(_damageTextPrefab, _damageAndEffectsCanvas.transform);
            instDmgText.transform.position = Camera.main.WorldToScreenPoint(worldPosition);
            instDmgText.Show(damage);
        }

        public void DisplayStatusChange(Vector3 worldPosition, EffectType effect, bool add)
        {
            if (!_instStatusWrapper)
                _instStatusWrapper = Instantiate(_displayedStatusWrapper, _damageAndEffectsCanvas.transform);

            _instStatusWrapper.transform.position = Camera.main.WorldToScreenPoint(worldPosition);

            DisplayedStatusChange status = Instantiate(_displayedStatusChangePrefab, _instStatusWrapper);
            status.Feed(effect, add);
            status.Show();
        }
    }
}
