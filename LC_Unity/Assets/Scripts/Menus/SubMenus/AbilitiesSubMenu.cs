using UnityEngine;

namespace Menus.SubMenus
{
    public class AbilitiesSubMenu : SubMenu 
    {
        public override void Open()
        {
            StartCoroutine(DoOpen());
        }

        public override void Close()
        {
            StartCoroutine(DoClose());
        }
    }
}
