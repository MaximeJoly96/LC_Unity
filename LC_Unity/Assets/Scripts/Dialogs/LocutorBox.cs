using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Dialogs
{
    public class LocutorBox : MonoBehaviour
    {
        [SerializeField]
        private Image _border;
        [SerializeField]
        private TMP_Text _text;

        public bool Displayed { get; private set; } = true;
        public float Height
        {
            get { return 50.0f; }
        }

        public void SetName(string locutorName)
        {
            _text.text = locutorName;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            Displayed = false;
        }

        public void Open()
        {
            gameObject.SetActive(true);
            Displayed = true;
        }
    }
}
