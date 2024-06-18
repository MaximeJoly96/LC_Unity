using UnityEngine;
using MusicAndSounds;

namespace Engine.MusicAndSounds
{
    public class FadeOutBgs : FadeOutAudio
    {
        public override void Run()
        {
            Object.FindObjectOfType<AudioPlayer>().FadeOutBgs(this);
            Finished.Invoke();
            IsFinished = true;
        }
    }
}
