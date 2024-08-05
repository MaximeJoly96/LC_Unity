using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

namespace TitleScreen
{
    public class VolumeEditionOption : SpecificTitleScreenOption
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

        private void UpdateAudioMixerVolume()
        {
            _audioGroup.audioMixer.SetFloat("volume", SliderValueToAudioVolume(_slider.value));
        }

        private void RestoreVolumeData()
        {
            if(PlayerPrefs.HasKey(_audioGroup.audioMixer.name + "Volume"))
            {
                _slider.value = PlayerPrefs.GetFloat(_audioGroup.audioMixer.name + "Volume");
            }
            
            UpdateAudioMixerVolume();
        }
    }
}
