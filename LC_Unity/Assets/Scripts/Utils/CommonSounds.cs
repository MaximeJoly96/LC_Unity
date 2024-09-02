using Engine.MusicAndSounds;
using MusicAndSounds;
using UnityEngine;

namespace Utils
{
    public static class CommonSounds
    {
        public static void CursorMoved()
        {
            PlaySound("MoveCursor");
        }

        public static void OptionSelected()
        {
            PlaySound("Confirm");
        }

        public static void ActionCancelled()
        {
            PlaySound("Cancel");
        }

        private static void PlaySound(string key)
        {
            GameObject.FindObjectOfType<AudioPlayer>().PlaySoundEffect(new PlaySoundEffect
            {
                Name = key,
                Volume = 0.25f,
                Pitch = 1.0f
            });
        }
    }
}
