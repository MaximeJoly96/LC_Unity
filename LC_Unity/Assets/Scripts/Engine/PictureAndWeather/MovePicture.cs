using Engine.Events;

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

        public void Run()
        {

        }
    }
}
