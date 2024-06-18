using UnityEngine;
using System.Linq;

namespace MusicAndSounds
{
    public class Soundbank : MonoBehaviour
    {
        [SerializeField]
        private SoundMetadata[] _sounds;

        public SoundMetadata GetSound(string key)
        {
            return _sounds.FirstOrDefault(s => s.key == key);
        }
    }
}
