using Engine.SceneControl;
using Save;
using UnityEngine;
using Movement;
using Core;

namespace BattleSystem
{
    public class BattleLoader
    {
        public void LoadBattle(BattleProcessing battle)
        {
            if(GlobalStateMachine.Instance.CurrentState != GlobalStateMachine.State.LoadingBattle && 
               !BattleDataHolder.Instance.Loading)
            {
                GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.LoadingBattle);
                BattleDataHolder.Instance.BattleData = battle;
                BattleDataHolder.Instance.Loading = true;
                SaveManager.Instance.SetPlayerPosition(Object.FindObjectOfType<PlayerController>().Position);

                BattleTransitionsHolder holder = Object.FindObjectOfType<BattleTransitionsHolder>();

                if (holder)
                    holder.PlayRandomTransition();
            }
        }
    }
}
