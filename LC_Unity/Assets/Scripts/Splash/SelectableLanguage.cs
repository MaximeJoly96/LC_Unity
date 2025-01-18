using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Language;

namespace Splash
{
    public class SelectableLanguage : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _label;
        [SerializeField]
        private Image _flag;
        [SerializeField]
        private Transform _cursor;

        public void Feed(Localization localization)
        {
            _label.text = LanguageUtility.TranslateLanguageLabel(localization.language);
            _flag.sprite = localization.flag;
        }

        public void ShowCursor(bool show)
        {
            _cursor.GetComponent<Animator>().Play(show ? "Show" : "Hide");
        }
    }
}
