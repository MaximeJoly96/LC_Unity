using UnityEngine;

namespace TitleScreen
{
    public class TitleScreenOption : MonoBehaviour
    {
        [SerializeField]
        private MainMenuPanel.MainMenuOptions _option;
        [SerializeField]
        private Transform _cursor;

        internal MainMenuPanel.MainMenuOptions Option { get { return _option; } }

        public void ShowCursor(bool show)
        {
            _cursor.gameObject.SetActive(show);
        }

        public bool IsInsideRect(Vector2 position)
        {
            RectTransform rt = GetComponent<RectTransform>();

            return position.x >= rt.position.x - rt.rect.width / 2 && position.x <= rt.position.x + rt.rect.width / 2 &&
                   position.y >= rt.position.y - rt.rect.height / 2 && position.y <= rt.position.y + rt.rect.height / 2;
        }
    }
}
