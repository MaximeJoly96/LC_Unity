﻿using Engine.Events;
using UnityEngine.Events;

namespace Engine.PictureAndWeather
{
    public class SetWeatherEffects : IRunnable
    {
        public enum WeatherType { None, Rain, Storm, Snow }

        public WeatherType Weather { get; set; }
        public int Power { get; set; }
        public int TransitionDuration { get; set; }
        public bool WaitForCompletion { get; set; }
        public UnityEvent Finished { get; set; }

        public SetWeatherEffects()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {

        }
    }
}
