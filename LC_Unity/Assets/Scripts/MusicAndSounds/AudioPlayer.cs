using UnityEngine;
using Engine.MusicAndSounds;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using UnityEngine.Audio;

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
        [SerializeField]
        private AudioMixerGroup _musicMixer;
        [SerializeField]
        private AudioMixerGroup _soundMixer;

        private List<RunningAudio> _runningAudios;
        private List<RunningAudio> _pausedAudios;

        public void PlayBgm(PlayBgm bgm)
        {
            if(_bgmBank)
            {
                SoundMetadata sound = _bgmBank.GetSound(bgm.Name);

                CreateAudioSource(sound, bgm, true, AudioType.BGM);
            }
        }

        public void PlayBgs(PlayBgs bgs)
        {
            if(_bgsBank)
            {
                SoundMetadata sound = _bgsBank.GetSound(bgs.Name);

                CreateAudioSource(sound, bgs, true, AudioType.BGS);
            }
        }

        public void PlayMusicalEffect(PlayMusicalEffect musicalEffect)
        {
            if(_musicalEffectsBank)
            {
                SoundMetadata sound = _musicalEffectsBank.GetSound(musicalEffect.Name);

                CreateAudioSource(sound, musicalEffect, false, AudioType.ME);
            }
        }

        public void PlaySoundEffect(PlaySoundEffect soundEffect)
        {
            if(_soundEffectsBank)
            {
                SoundMetadata sound = _soundEffectsBank.GetSound(soundEffect.Name);

                CreateAudioSource(sound, soundEffect, false, AudioType.SE);
            }
        }

        public void FadeOutBgm(FadeOutBgm bgm)
        {
            RunningAudio audio = _runningAudios.FirstOrDefault(a => a.Sound.key == bgm.Name);

            if(audio != null)
            {
                StartCoroutine(FadeOutAudio(audio.Source, bgm.TransitionDuration));
            }
        }

        public void FadeOutBgs(FadeOutBgs bgs)
        {
            RunningAudio audio = _runningAudios.FirstOrDefault(a => a.Sound.key == bgs.Name);

            if (audio != null)
            {
                StartCoroutine(FadeOutAudio(audio.Source, bgs.TransitionDuration));
            }
        }

        public void StopAllAudio()
        {
            for(int i = 0; i < _runningAudios.Count; i++)
            {
                StartCoroutine(FadeOutAudio(_runningAudios[i].Source, 0.1f));
            }
        }

        private void Awake()
        {
            _runningAudios = new List<RunningAudio>();
            _pausedAudios = new List<RunningAudio>();

            DontDestroyOnLoad(this.gameObject);
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
            if(sound != null)
            {
                AudioSource source = gameObject.AddComponent<AudioSource>();
                source.clip = sound.audio;
                source.volume = audio.Volume;
                source.pitch = audio.Pitch;
                source.loop = loop;

                source.outputAudioMixerGroup = type == AudioType.BGM || type == AudioType.ME ? _musicMixer : _soundMixer;

                if (type == AudioType.ME)
                    PauseAllRunningAudios();

                source.Play();

                _runningAudios.Add(new RunningAudio(sound, source, type));
            }
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
                PauseAudio(_runningAudios[i]);
            }
        }

        private void ResumeNonMusicalEffects()
        {
            if (_runningAudios.Any(a => a.Type == AudioType.ME))
                return;

            for(int i = 0; i < _runningAudios.Count; i++)
            {
                ResumeAudio(_runningAudios[i]);
            }
        }

        private void PauseAudio(RunningAudio audio)
        {
            if(_pausedAudios.FirstOrDefault(a => a.Sound.key == audio.Sound.key) == null)
            {
                audio.Source.Pause();
                _pausedAudios.Add(audio);
            }
        }

        private void ResumeAudio(RunningAudio audio)
        {
            audio.Source.UnPause();
            _pausedAudios.Remove(audio);
        }

        private IEnumerator FadeOutAudio(AudioSource source, float duration)
        {
            WaitForFixedUpdate wait = new WaitForFixedUpdate();

            float baseVolume = source.volume;
            float step = baseVolume / (duration / Time.fixedDeltaTime);

            for(float i = 0.0f; i < duration; i += Time.fixedDeltaTime)
            {
                source.volume -= step;

                yield return wait;
            }

            source.Stop();
        }
    }
}
