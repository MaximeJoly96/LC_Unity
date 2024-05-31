using Engine.Events;
using UnityEngine.Events;

namespace Engine.Character
{
    public class ShowBalloonIcon : IRunnable
    {
        public string Target { get; set; }
        public int BalloonIconId { get; set; }
        public bool WaitForCompletion { get; set; }
        public UnityEvent Finished { get; set; }

        public ShowBalloonIcon()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {

        }
    }
}
