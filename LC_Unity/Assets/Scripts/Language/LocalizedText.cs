using UnityEngine;
using TMPro;

namespace Language
{
    public class LocalizedText : MonoBehaviour
    {
        [SerializeField]
        private string _key;

        private void Start()
        {
            Localizer.Instance.LanguageUpdated.AddListener(UpdateText);
            UpdateText();
        }

        public void UpdateText()
        {
            GetComponent<TMP_Text>().text = Localizer.Instance.GetString(_key);
        }
    }
}
