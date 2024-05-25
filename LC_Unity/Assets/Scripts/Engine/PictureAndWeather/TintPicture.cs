using Engine.Events;
using UnityEngine;

namespace Engine.PictureAndWeather
{
    public class TintPicture : IRunnable
    {
        public int Id { get; set; }
        public int Duration { get; set; }
        public bool WaitForCompletion { get; set; }
        public Color TargetColor { get; set; }

        public void Run()
        {

        }
    }
}
