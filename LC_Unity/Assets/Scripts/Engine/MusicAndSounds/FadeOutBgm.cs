using UnityEngine;
using MusicAndSounds;

namespace Engine.MusicAndSounds
{
    public class FadeOutBgm : FadeOutAudio
    {
        public override void Run()
        {
            Object.FindObjectOfType<AudioPlayer>().FadeOutBgm(this);
            Finished.Invoke();
            IsFinished = true;
        }
    }
}
