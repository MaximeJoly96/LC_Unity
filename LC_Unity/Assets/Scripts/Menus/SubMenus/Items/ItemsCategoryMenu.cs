using UnityEngine;

namespace Menus.SubMenus.Items
{
    public class ItemsCategoryMenu : MonoBehaviour
    {
        public void ShowCursor(bool show)
        {
            transform.Find("Bg").gameObject.SetActive(show);
            transform.Find("CursorWrapper").gameObject.SetActive(show);
        }
    }
}
