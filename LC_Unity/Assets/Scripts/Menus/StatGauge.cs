using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Menus
{
    public class StatGauge : MonoBehaviour
    {
        [SerializeField]
        protected Image _filledGauge;
        [SerializeField]
        protected TMP_Text _value;

        public void SetGauge(float value, float maxValue)
        {
            _value.text = value + " / " + maxValue;
            _filledGauge.fillAmount = value / maxValue;
        }
    }
}
