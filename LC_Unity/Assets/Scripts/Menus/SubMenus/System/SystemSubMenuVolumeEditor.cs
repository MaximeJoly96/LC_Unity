﻿using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace Menus.SubMenus.System
{
    public class SystemSubMenuVolumeEditor : SystemSubMenuItem
    {
        [SerializeField]
        private AudioMixerGroup _audioGroup;
        [SerializeField]
        private Slider _slider;

        private void Start()
        {
            RestoreVolumeData();
        }

        public override void MoveCursorLeft()
        {
            _slider.value -= 0.05f;
            UpdateAudioMixerVolume();

            RegisterVolume();
        }

        public override void MoveCursorRight()
        {
            _slider.value += 0.05f;
            UpdateAudioMixerVolume();

            RegisterVolume();
        }

        private float SliderValueToAudioVolume(float sliderValue)
        {
            return sliderValue < 0.01f ? -80.0f : Mathf.Lerp(-40.0f, 0.0f, sliderValue);
        }

        private void RegisterVolume()
        {
            PlayerPrefs.SetFloat(_audioGroup.audioMixer.name + "Volume", _slider.value);
        }

        private void RestoreVolumeData()
        {
            _slider.value = PlayerPrefs.GetFloat(_audioGroup.audioMixer.name + "Volume");
            UpdateAudioMixerVolume();
        }

        private void UpdateAudioMixerVolume()
        {
            _audioGroup.audioMixer.SetFloat("volume", SliderValueToAudioVolume(_slider.value));
        }
    }
}
