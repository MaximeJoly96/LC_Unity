using UnityEngine;
using TMPro;

namespace Shop
{
    public class QuantitySelector : MonoBehaviour
    {
        [SerializeField]
        private Transform _background;
        [SerializeField]
        private Transform _upArrow;
        [SerializeField] 
        private Transform _downArrow;

        private int _currentValue;

        public int Value { get { return _currentValue; } }

        public void Select(bool select)
        {
            _background.gameObject.SetActive(select);
            _upArrow.gameObject.SetActive(select);
            _downArrow.gameObject.SetActive(select);
        }

        public void SetValue(int value)
        {
            _currentValue = value;
            GetComponent<TMP_Text>().text = _currentValue.ToString();
        }

        public void Increment()
        {
            SetValue(_currentValue == 9 ? 0 : ++_currentValue);
        }

        public void Decrement()
        {
            SetValue(_currentValue == 0 ? 9 : --_currentValue);
        }
    }
}
