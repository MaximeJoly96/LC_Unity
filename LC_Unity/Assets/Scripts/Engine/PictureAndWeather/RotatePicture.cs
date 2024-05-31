using Engine.Events;
using UnityEngine.Events;

namespace Engine.PictureAndWeather
{
    public class RotatePicture : IRunnable
    {
        public int Id { get; set; }
        public int Angle { get; set; }
        public int Duration { get; set; }
        public UnityEvent Finished { get; set; }

        public RotatePicture()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {

        }
    }
}
