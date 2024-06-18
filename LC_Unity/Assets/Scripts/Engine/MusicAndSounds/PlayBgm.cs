using UnityEngine;
using MusicAndSounds;

namespace Engine.MusicAndSounds
{
    public class PlayBgm : PlayAudio
    {
        public override void Run()
        {
            Object.FindObjectOfType<AudioPlayer>().PlayBgm(this);
            Finished.Invoke();
            IsFinished = true;
        }
    }
}
