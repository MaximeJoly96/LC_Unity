using MusicAndSounds;
using UnityEngine;

namespace Engine.MusicAndSounds
{
    public class PlaySoundEffect : PlayAudio
    {
        public override void Run()
        {
            Object.FindObjectOfType<AudioPlayer>().PlaySoundEffect(this);
            Finished.Invoke();
            IsFinished = true;
        }
    }
}
