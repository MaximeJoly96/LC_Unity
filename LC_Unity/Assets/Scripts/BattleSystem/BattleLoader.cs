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
            SaveManager.Instance.SetPlayerPosition(Object.FindObjectOfType<PlayerController>().Position);

            BattleTransitionsHolder holder = Object.FindObjectOfType<BattleTransitionsHolder>();
            
            if(holder)
                holder.PlayRandomTransition();
        }
    }
}
