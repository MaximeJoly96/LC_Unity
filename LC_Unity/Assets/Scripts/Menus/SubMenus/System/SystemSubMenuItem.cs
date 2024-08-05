using UnityEngine;
using TMPro;

namespace Menus.SubMenus.System
{
    public class SystemSubMenuItem : MonoBehaviour
    {
        [SerializeField]
        private Transform _cursor;

        public void Hover(bool select)
        {
            _cursor.gameObject.SetActive(select);
        }

        public virtual void Select()
        {

        }
    }
}
