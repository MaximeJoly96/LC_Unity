using UnityEngine;

namespace TitleScreen
{
    public abstract class SpecificTitleScreenOption : MonoBehaviour
    {
        [SerializeField]
        private Transform _cursor;

        public abstract void MoveCursorLeft();
        public abstract void MoveCursorRight();
        public virtual void Select() { }

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
