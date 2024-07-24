using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace BattleSystem.UI
{
    public class TimelineUiController : MonoBehaviour
    {
        [SerializeField]
        private Transform _characters;
        [SerializeField]
        private BattlerTimeline _battlerTimelinePrefab;

        public void Feed(List<BattlerBehaviour> battlers)
        {
            for(int i = 0; i < battlers.Count; i++)
            {
                BattlerTimeline timeline = Instantiate(_battlerTimelinePrefab, _characters);
                timeline.Feed(battlers[i]);
            }
        }
    }
}
