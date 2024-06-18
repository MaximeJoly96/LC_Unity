using Engine.Events;
using UnityEngine.Events;
using UnityEngine;
using MusicAndSounds;

namespace Engine.MusicAndSounds
{
    public class SaveBgm : IRunnable
    {
        public string Name { get; set; }
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        public SaveBgm()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {
            Object.FindObjectOfType<AudioPlayer>().SaveBgm(this);
            Finished.Invoke();
            IsFinished = true;
        }
    }
}
