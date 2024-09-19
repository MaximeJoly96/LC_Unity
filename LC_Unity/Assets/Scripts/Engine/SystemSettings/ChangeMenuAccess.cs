using Menus;
using UnityEngine;

namespace Engine.SystemSettings
{
    public class ChangeMenuAccess : ChangeAccess
    {
        public override void Run()
        {
            Object.FindObjectOfType<MainMenuController>().ToggleAccess(Enabled);

            Finished.Invoke();
            IsFinished = true;
        }
    }
}
