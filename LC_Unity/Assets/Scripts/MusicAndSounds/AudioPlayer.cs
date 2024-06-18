using UnityEngine;
using Engine.MusicAndSounds;
using System.Collections.Generic;
using System.Linq;

namespace MusicAndSounds
{
    public class AudioPlayer : MonoBehaviour
    {
        private enum AudioType { BGM, BGS, ME, SE }

        private class RunningAudio
        {
            public SoundMetadata Sound { get; set; }
            public AudioSource Source { get; set; }
            public AudioType Type { get; set; }


            public RunningAudio(SoundMetadata sound, AudioSource source, AudioType type)
            {
                Sound = sound;
                Source = source;
                Type = type;
            }
        }

        [SerializeField]
        private Soundbank _bgmBank;
        [SerializeField]
        private Soundbank _bgsBank;
        [SerializeField]
        private Soundbank _musicalEffectsBank;
        [SerializeField]
        private Soundbank _soundEffectsBank;

        private List<RunningAudio> _runningAudios;
        private List<RunningAudio> _pausedAudios;

        public void PlayBgm(PlayBgm bgm)
        {
            SoundMetadata sound = _bgmBank.GetSound(bgm.Name);

            CreateAudioSource(sound, bgm, true, AudioType.BGM);
        }

        public void PlayBgs(PlayBgs bgs)
        {
            SoundMetadata sound = _bgsBank.GetSound(bgs.Name);

            CreateAudioSource(sound, bgs, true, AudioType.BGS);
        }

        public void PlayMusicalEffect(PlayMusicalEffect musicalEffect)
        {
            SoundMetadata sound = _musicalEffectsBank.GetSound(musicalEffect.Name);

            CreateAudioSource(sound, musicalEffect, false, AudioType.ME);
        }

        public void PlaySoundEffect(PlaySoundEffect soundEffect)
        {
            SoundMetadata sound = _soundEffectsBank.GetSound(soundEffect.Name);

            CreateAudioSource(sound, soundEffect, false, AudioType.SE);
        }

        public void FadeOutBgm(FadeOutBgm bgm)
        {

        }

        public void FadeOutBgs(FadeOutBgs bgs)
        {

        }

        public void ReplayBgm(ReplayBgm bgm)
        {

        }

        public void SaveBgm(SaveBgm bgm)
        {

        }

        private void Awake()
        {
            _runningAudios = new List<RunningAudio>();
            _pausedAudios = new List<RunningAudio>();
        }

        private void Update()
        {
            CleanRunningAudios();

            for (int i = 0; i < _runningAudios.Count; i++)
            {
                if (!_runningAudios[i].Source.isPlaying && _pausedAudios.FirstOrDefault(a => a.Sound.key == _runningAudios[i].Sound.key) == null)
                    Destroy(_runningAudios[i].Source);
            }

            ResumeNonMusicalEffects();
        }

        private void CreateAudioSource(SoundMetadata sound, PlayAudio audio, bool loop, AudioType type)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = sound.audio;
            source.volume = audio.Volume;
            source.pitch = audio.Pitch;
            source.loop = loop;

            if (type == AudioType.ME)
                PauseAllRunningAudios();

            source.Play();

            _runningAudios.Add(new RunningAudio(sound, source, type));
        }

        private void CleanRunningAudios()
        {
            _runningAudios = _runningAudios.Where(r => r.Source).ToList();
            _pausedAudios = _pausedAudios.Where(a => a.Source).ToList();
        }

        private void PauseAllRunningAudios()
        {
            for(int i = 0; i < _runningAudios.Count; i++)
            {
                _runningAudios[i].Source.Pause();
                _pausedAudios.Add(_runningAudios[i]);
            }
        }

        private void ResumeNonMusicalEffects()
        {
            if (_runningAudios.Any(a => a.Type == AudioType.ME))
                return;

            for(int i = 0; i < _runningAudios.Count; i++)
            {
                _runningAudios[i].Source.UnPause();
            }
        }
    }
}
