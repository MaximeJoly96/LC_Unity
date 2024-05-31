using Engine.Events;
using UnityEngine;
using UnityEngine.Events;

namespace Engine.PictureAndWeather
{
    public class TintPicture : IRunnable
    {
        public int Id { get; set; }
        public int Duration { get; set; }
        public bool WaitForCompletion { get; set; }
        public Color TargetColor { get; set; }
        public UnityEvent Finished { get; set; }

        public TintPicture()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {

        }
    }
}
