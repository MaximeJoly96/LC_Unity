using Engine.Events;
using UnityEngine.Events;

namespace Engine.PictureAndWeather
{
    public class MovePicture : IRunnable
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public float Alpha { get; set; }
        public int Duration { get; set; }
        public bool WaitForCompletion { get; set; }
        public UnityEvent Finished { get; set; }

        public MovePicture()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {

        }
    }
}
