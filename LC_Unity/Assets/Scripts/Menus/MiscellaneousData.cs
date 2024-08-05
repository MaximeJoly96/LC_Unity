using TMPro;
using UnityEngine;
using Timing;
using Utils;
using Field;
using Core;
using Language;
using Party;

namespace Menus
{
    public class MiscellaneousData : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _goldText;
        [SerializeField]
        private TMP_Text _location;
        [SerializeField]
        private TMP_Text _inGameTime;

        private GlobalTimer _globalTimer;

        private void Awake()
        {
            _globalTimer = FindObjectOfType<GlobalTimer>();
            _globalTimer.GlobalTimeChangedEvent.AddListener(UpdateTime);
        }

        public void Open()
        {
            _location.text = Localizer.Instance.GetString(FieldNames.MAP_NAMES[GlobalStateMachine.Instance.CurrentMapId]);

            string moneyKey = PartyManager.Instance.Gold > 1 ? "moneyLabelPlural" : "moneyLabel";
            _goldText.text = PartyManager.Instance.Gold + " " + Localizer.Instance.GetString(moneyKey);
        }

        private void UpdateTime(int timeSeconds)
        {
            _inGameTime.text = TimeConverter.FormatTimeFromSeconds(timeSeconds);
        }
    }
}
