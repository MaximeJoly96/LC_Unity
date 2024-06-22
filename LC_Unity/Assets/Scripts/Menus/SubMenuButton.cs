using UnityEngine;
using UnityEngine.UI;

namespace Menus
{
    public class SubMenuButton : MonoBehaviour
    {
        private Transform Background
        {
            get { return transform.Find("Bg"); }
        }

        private Transform Cursor
        {
            get { return transform.Find("CursorWrapper"); }
        }

        public void DisplayCursor(bool display)
        {
            Background.gameObject.SetActive(display);
            Cursor.gameObject.SetActive(display);
        }
    }
}
