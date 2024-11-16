using UnityEngine;
using TMPro;

namespace UI
{
    public class SelectableItem : MonoBehaviour
    {
        [SerializeField]
        protected TMP_Text _label;
        [SerializeField]
        protected Transform _cursor;

        public Transform Cursor { get { return _cursor; } set { _cursor = value; } }
        public TMP_Text Label { get { return _label; } set { _label = value; } }

        public virtual void ShowCursor(bool show)
        {
            _cursor.gameObject.SetActive(show);
        }
    }
}
