using Save;

namespace Menus.SubMenus.System
{
    public class SystemSubMenuSaveGame : SystemSubMenuItem
    {
        public override void Select()
        {
            SaveManager.Instance.InitSaveCreation();
        }
    }
}
