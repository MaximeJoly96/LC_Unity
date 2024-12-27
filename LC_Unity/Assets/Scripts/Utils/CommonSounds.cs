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

        public static void Victory()
        {
            PlayMusicalEffect("victory");
        }

        public static void Defeat()
        {
            PlayMusicalEffect("defeat");
        }

        public static void Error()
        {
            PlaySound("Error1");
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

        private static void PlayMusicalEffect(string key)
        {
            GameObject.FindObjectOfType<AudioPlayer>().PlayMusicalEffect(new PlayMusicalEffect
            {
                Name = key,
                Volume = 1.0f,
                Pitch = 1.0f
            });
        }
    }
}
