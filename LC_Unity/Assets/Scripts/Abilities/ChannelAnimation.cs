using MusicAndSounds;
using UnityEngine;

namespace Abilities
{
    public class ChannelAnimation : MonoBehaviour
    {
        public void PlaySoundEffect(string key)
        {
            FindObjectOfType<AudioPlayer>().PlaySoundEffect(new Engine.MusicAndSounds.PlaySoundEffect
            {
                Name = key,
                Volume = 1.0f,
                Pitch = 1.0f
            });
        }

        public void FinishedAnimation()
        {
            Destroy(gameObject);
        }
    }
}
