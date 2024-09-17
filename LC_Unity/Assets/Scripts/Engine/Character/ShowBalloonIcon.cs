using Engine.Events;
using UnityEngine.Events;
using Actors;
using UnityEngine;

namespace Engine.Character
{
    public class ShowBalloonIcon : IRunnable
    {
        public enum BalloonType
        {
            Silence,
            Sweat
        }

        public string AgentId { get; set; }
        public BalloonType BalloonIcon { get; set; }
        public bool WaitForCompletion { get; set; }
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        public ShowBalloonIcon()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {
            Object.FindObjectOfType<BalloonIconsManager>().ShowBalloonIcon(this);
        }
    }
}
