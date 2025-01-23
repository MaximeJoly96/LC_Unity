using UnityEngine;
using System;
using Engine.PictureAndWeather;

namespace PictureAndWeather
{
    [Serializable]
    public class Weather
    {
        public SetWeatherEffects.WeatherType WeatherType;
        public GameObject Prefab;
    }

    public class WeatherMaker : MonoBehaviour
    {
        [SerializeField]
        private Weather _rainyWeather;

        private GameObject _currentWeatherEffect;

        public void SetWeather(SetWeatherEffects effect)
        {
            if (_currentWeatherEffect)
                Destroy(_currentWeatherEffect);

            switch(effect.Weather)
            {
                case SetWeatherEffects.WeatherType.Rain:
                    MakeItRain(effect);
                    break;
            }
        }

        private void MakeItRain(SetWeatherEffects effect)
        {
            _currentWeatherEffect = Instantiate(_rainyWeather.Prefab);
        }
    }
}
