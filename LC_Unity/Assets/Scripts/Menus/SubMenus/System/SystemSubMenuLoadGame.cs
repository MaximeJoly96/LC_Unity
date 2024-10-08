﻿using Core;
using MusicAndSounds;
using Save;

namespace Menus.SubMenus.System
{
    public class SystemSubMenuLoadGame : SystemSubMenuItem
    {
        public override void Select()
        {
            SaveManager.Instance.SaveCancelledEvent.RemoveAllListeners();
            SaveManager.Instance.SaveCancelledEvent.AddListener(() => GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.InMenuSystemTab));
            SaveManager.Instance.InitSaveLoad();
        }
    }
}
