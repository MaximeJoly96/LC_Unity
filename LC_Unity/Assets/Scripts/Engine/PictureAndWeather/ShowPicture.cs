using Engine.Events;
using UnityEngine.Events;

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
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        public ShowPicture()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {

        }
    }
}
