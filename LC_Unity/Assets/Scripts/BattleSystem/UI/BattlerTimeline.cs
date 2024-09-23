using UnityEngine;
using TMPro;
using UnityEngine.UI;
using BattleSystem.Model;

namespace BattleSystem.UI
{
    public class BattlerTimeline : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _battlerName;
        [SerializeField]
        private Image _timeline;

        public TimelineAction Action { get; set; }
        public BattlerBehaviour Battler { get; set; }
        public bool Processed { get; set; }

        public void Feed(BattlerBehaviour battler)
        {
            Battler = battler;
            _battlerName.text = battler.BattlerData.Character.Name;
        }

        public TimelineAction ComputeAction(BattlerBehaviour battler)
        {
            Action = new TimelineAction(battler);
            return Action;
        }

        public void DrawTimeline(float totalLength)
        {
            Vector2 anchoredPosition = _timeline.GetComponent<RectTransform>().anchoredPosition;
            float totalLengthPixels = anchoredPosition.x;

            float actionLength = Action.Length / totalLength;
            anchoredPosition.x += (Action.StartPoint / totalLength) * totalLengthPixels;
            _timeline.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
            _timeline.fillAmount = actionLength;
        }
    }
}
