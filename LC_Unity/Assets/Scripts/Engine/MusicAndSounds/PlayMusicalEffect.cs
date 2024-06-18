using UnityEngine;
using MusicAndSounds;

namespace Engine.MusicAndSounds
{
    public class PlayMusicalEffect : PlayAudio
    {
        public override void Run()
        {
            Object.FindObjectOfType<AudioPlayer>().PlayMusicalEffect(this);
            Finished.Invoke();
            IsFinished = true;
        }
    }
}
