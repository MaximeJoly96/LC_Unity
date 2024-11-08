using UnityEngine;
using Engine.MusicAndSounds;
using System.Collections.Generic;
using MusicAndSounds;
using System.Linq;

namespace Movement
{
    public enum GroundType
    {
        Stone,
        Wood,
        Grass,
        Dirt,
        None
    }

    public class StepSoundsPlayer : MonoBehaviour
    {
        private static readonly Dictionary<GroundType, PlaySoundEffect> SOUND_EFFECTS = new Dictionary<GroundType, PlaySoundEffect>
        {
            { GroundType.Dirt, new PlaySoundEffect { Name = "StepDirt", Volume = 0.1f, Pitch = 1.0f} },
            { GroundType.Wood, new PlaySoundEffect { Name = "StepWood", Volume = 0.2f, Pitch = 1.0f} },
            { GroundType.Grass, new PlaySoundEffect { Name = "StepGrass", Volume = 0.1f, Pitch = 1.0f} },
            { GroundType.Stone, new PlaySoundEffect { Name = "StepFloor", Volume = 0.2f, Pitch = 1.0f} },
            { GroundType.None, new PlaySoundEffect { Name = "StepFloor", Volume = 0.0f, Pitch = 1.0f} },
        };

        private GroundType _currentlyWalkingOn;
        private AudioPlayer _audioPlayer;
        private AudioSource _source;

        private AudioSource Source
        {
            get
            {
                if(!_source)
                    _source = GetComponent<AudioSource>();

                return _source;
            }
        }

        private AudioPlayer AudioPlayer
        {
            get
            {
                if(!_audioPlayer)
                    _audioPlayer = FindObjectOfType<AudioPlayer>();

                return _audioPlayer;
            }
        }

        public void StepSound()
        {
            _currentlyWalkingOn = AssessGroundType();

            PlaySoundEffect pse = SOUND_EFFECTS[_currentlyWalkingOn];

            Source.volume = pse.Volume;
            Source.pitch = pse.Pitch;
            Source.clip = AudioPlayer.GetSoundEffectMetadata(pse.Name).audio;
            Source.Play();
        }

        private GroundType AssessGroundType()
        {
            Bounds colliderBounds = GetComponent<Collider2D>().bounds;
            RaycastHit2D[] result = Physics2D.RaycastAll(colliderBounds.center, new Vector3(0.0f, -1.0f, 0.0f));

            RaycastHit2D hitSurface = result.FirstOrDefault(r => r && r.collider.GetComponent<WalkableSurface>());

            return hitSurface ? hitSurface.collider.GetComponent<WalkableSurface>().GroundType : GroundType.None;
        }
    }
}
