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

        public Image Gauge { get { return _filledGauge; } }
        public TMP_Text Value { get { return _value; } }

        public void SetGauge(float value, float maxValue)
        {
            if (value > maxValue)
                value = maxValue;

            _value.text = value + " / " + maxValue;
            _filledGauge.fillAmount = Mathf.Abs(0.0f - maxValue) < 0.01f ? 0.0f : value / maxValue;
        }

        // For unit testing purposes
        public void Init(Image gauge, TMP_Text text)
        {
            _filledGauge = gauge;
            _value = text;
        }
    }
}
