using TMPro;
using UnityEngine;

namespace TitleScreen
{
    public class TitleScreenOption : MonoBehaviour
    {
        [SerializeField]
        private TitleScreenManager.Options _option;
        [SerializeField]
        private TMP_Text _text;
        [SerializeField]
        private Transform _cursor;

        internal TitleScreenManager.Options Option { get { return _option; } }

        public void ShowCursor(bool show)
        {
            _cursor.gameObject.SetActive(show);
        }
    }
}
