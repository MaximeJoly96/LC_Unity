using UnityEngine;

namespace BattleSystem.UI
{
    public class DamageDisplay : MonoBehaviour
    {
        [SerializeField]
        private Canvas _damageAndEffectsCanvas;
        [SerializeField]
        private DamageText _damageTextPrefab;

        public void DisplayDamage(Vector3 worldPosition, int damage)
        {
            DamageText instDmgText = Instantiate(_damageTextPrefab, _damageAndEffectsCanvas.transform);
            instDmgText.transform.position = Camera.main.WorldToScreenPoint(worldPosition);
            instDmgText.Show(damage);
        }
    }
}
