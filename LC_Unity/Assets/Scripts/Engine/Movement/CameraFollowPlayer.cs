using Engine.Events;
using UnityEngine.Events;
using UnityEngine;
using Movement;

namespace Engine.Movement
{
    public class CameraFollowPlayer : IRunnable
    {
        public bool Follow { get; set; }
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        public CameraFollowPlayer()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {
            Object.FindObjectOfType<CameraFollower>().FollowPlayer(Follow);

            Finished.Invoke();
            IsFinished = true;
        }
    }
}
