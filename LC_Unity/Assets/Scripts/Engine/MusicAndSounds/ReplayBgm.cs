using Engine.Events;
using UnityEngine.Events;
using UnityEngine;
using MusicAndSounds;

namespace Engine.MusicAndSounds
{
    public class ReplayBgm : IRunnable
    {
        public string Name { get; set; }
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        public ReplayBgm()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {
            Object.FindObjectOfType<AudioPlayer>().ReplayBgm(this);
            Finished.Invoke();
            IsFinished = true;
        }
    }
}
