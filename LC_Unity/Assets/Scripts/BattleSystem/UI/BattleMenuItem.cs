using UnityEngine;
using TMPro;

namespace BattleSystem.UI
{
    public class BattleMenuItem : MonoBehaviour
    {
        [SerializeField]
        protected Transform _cursor;
        [SerializeField]
        protected TMP_Text _label;

        public void ShowCursor(bool show)
        {
            _cursor.gameObject.SetActive(show);
        }
    }
}
