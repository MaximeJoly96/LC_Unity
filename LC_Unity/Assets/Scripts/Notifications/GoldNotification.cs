using Language;
using TMPro;
using UnityEngine;

namespace Notifications
{
    public class GoldNotification : Notification
    {
        [SerializeField]
        private TMP_Text _text;

        public void Feed(int amount)
        {
            string moneyLabel = Mathf.Abs(amount) == 1 ? "moneyLabel" : "moneyLabelPlural";
            _text.text = (amount > 0 ? "+" : "") + amount + " " + Localizer.Instance.GetString(moneyLabel);
        }
    }
}
