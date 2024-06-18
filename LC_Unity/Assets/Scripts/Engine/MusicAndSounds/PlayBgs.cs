using UnityEngine;
using MusicAndSounds;

namespace Engine.MusicAndSounds
{
    public class PlayBgs : PlayAudio
    {
        public override void Run()
        {
            Object.FindObjectOfType<AudioPlayer>().PlayBgs(this);
            Finished.Invoke();
            IsFinished = true;
        }
    }
}
