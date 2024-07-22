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
    }
}
