using NUnit.Framework;
using BattleSystem;
using BattleSystem.UI;
using UnityEngine;

namespace Testing.BattleSystem
{
    public class BattleUiManagerTests : TestFoundation
    {


        private BattleUiManager CreateBattleUiManager()
        {
            BattleUiManager uiManager = ComponentCreator.CreateBattleUiManager();
            _usedGameObjects.Add(uiManager.HelpWindow.gameObject);
            _usedGameObjects.Add(uiManager.AttackLabelWindow.gameObject);
            _usedGameObjects.Add(uiManager.gameObject);

            return uiManager;
        }
    }
}
