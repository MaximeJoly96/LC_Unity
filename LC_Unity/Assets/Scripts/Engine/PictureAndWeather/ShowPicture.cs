using Engine.Events;

namespace Engine.PictureAndWeather
{
    public class ShowPicture : IRunnable
    {
        public int Id { get; set; }
        public bool Show { get; set; }
        public string Graphic { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public float Alpha { get; set; }

        public void Run()
        {

        }
    }
}
