using UnityEngine.SceneManagement;

namespace Menus.SubMenus.System
{
    public class SystemSubMenuReturnToTitle : SystemSubMenuItem
    {
        public override void Select()
        {
            SceneManager.LoadScene("TitleScreen");
        }
    }
}
