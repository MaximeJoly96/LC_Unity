using UnityEngine;
using BattleSystem.UI;
using System.Collections.Generic;

namespace BattleSystem
{
    public class BattleProcessor : MonoBehaviour
    {
        public void ProcessBattle(List<BattlerTimeline> timelines)
        {
            foreach(var timeline in timelines)
            {
                Debug.Log(timeline.Battler.LockedInAbility.Name + " StartPos = " + timeline.Action.StartPoint + " length = " + timeline.Action.Length);
            }
        }
    }
}
