using TMPro;
using UnityEngine;

namespace Dialogs
{
    public class SelectableChoice : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _choiceText;
        [SerializeField]
        private Transform _cursor;

        public void SetText(string text)
        {
            text = text.Replace("\\n", "<br>");
            _choiceText.text = text;
        }

        public void ShowCursor(bool show)
        {
            _cursor.gameObject.SetActive(show);
        }
    }
}