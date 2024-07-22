using TMPro;
using UnityEngine;

namespace TitleScreen
{
    public class TitleScreenOption : MonoBehaviour
    {
        [SerializeField]
        private MainMenuPanel.MainMenuOptions _option;
        [SerializeField]
        private TMP_Text _text;
        [SerializeField]
        private Transform _cursor;

        internal MainMenuPanel.MainMenuOptions Option { get { return _option; } }

        public void ShowCursor(bool show)
        {
            _cursor.gameObject.SetActive(show);
        }
    }
}
