using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace BattleSystem.UI
{
    public class BattlerTimeline : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _battlerName;
        [SerializeField]
        private Image _timeline;

        public void Feed(BattlerBehaviour battler)
        {
            _battlerName.text = battler.BattlerData.Name;
        }
    }
}
