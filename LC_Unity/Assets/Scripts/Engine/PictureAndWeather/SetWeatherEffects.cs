using Engine.Events;

namespace Engine.PictureAndWeather
{
    public class SetWeatherEffects : IRunnable
    {
        public enum WeatherType { None, Rain, Storm, Snow }

        public WeatherType Weather { get; set; }
        public int Power { get; set; }
        public int TransitionDuration { get; set; }
        public bool WaitForCompletion { get; set; }

        public void Run()
        {

        }
    }
}
