using UnityEngine.SceneManagement;
using Engine.SceneControl;
using Save;
using UnityEngine;
using Movement;

namespace BattleSystem
{
    public class BattleLoader
    {
        public void LoadBattle(BattleProcessing battle)
        {
            BattleDataHolder.Instance.BattleData = battle;
            SaveManager.Instance.Data.PlayerPosition = Object.FindObjectOfType<PlayerController>().Position;

            Object.FindObjectOfType<BattleTransitionsHolder>().PlayRandomTransition();
        }
    }
}
