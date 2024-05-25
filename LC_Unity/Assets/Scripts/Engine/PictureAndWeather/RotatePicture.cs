using Engine.Events;

namespace Engine.PictureAndWeather
{
    public class RotatePicture : IRunnable
    {
        public int Id { get; set; }
        public int Angle { get; set; }
        public int Duration { get; set; }

        public void Run()
        {

        }
    }
}
