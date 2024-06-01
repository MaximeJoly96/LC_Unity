using TMPro;
using UnityEngine;

namespace Dialogs
{
    public class SelectableChoice : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _choiceText;

        public void SetText(string text)
        {
            text = text.Replace("\\n", "<br>");
            _choiceText.text = text;
        }
    }
}