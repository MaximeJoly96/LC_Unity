using Save;

namespace Menus.SubMenus.System
{
    public class SystemSubMenuLoadGame : SystemSubMenuItem
    {
        public override void Select()
        {
            SaveManager.Instance.InitSaveLoad();
        }
    }
}
