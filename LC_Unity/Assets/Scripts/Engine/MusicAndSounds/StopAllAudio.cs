using Engine.Events;
using UnityEngine.Events;
using UnityEngine;
using MusicAndSounds;

namespace Engine.MusicAndSounds
{
    public class StopAllAudio : IRunnable
    {
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        public StopAllAudio()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {
            Object.FindObjectOfType<AudioPlayer>().StopAllAudio();

            Finished.Invoke();
            IsFinished = true;
        }
    }
}
