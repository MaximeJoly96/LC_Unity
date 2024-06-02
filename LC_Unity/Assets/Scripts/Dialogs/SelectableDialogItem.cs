using TMPro;
using UnityEngine;

namespace Dialogs
{
    public class SelectableDialogItem : MonoBehaviour
    {
        [SerializeField]
        protected TMP_Text _text;
        [SerializeField]
        protected Transform _cursor;

        public void SetText(string text)
        {
            text = text.Replace("\\n", "<br>");
            _text.text = text;
        }

        public void ShowCursor(bool show)
        {
            _cursor.gameObject.SetActive(show);
        }
    }
}