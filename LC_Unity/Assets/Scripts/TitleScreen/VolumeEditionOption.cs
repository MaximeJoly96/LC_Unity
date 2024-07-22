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

        public override void MoveCursorLeft()
        {
            _slider.value -= 0.05f;
            _audioGroup.audioMixer.SetFloat("volume", SliderValueToAudioVolume(_slider.value));
        }

        public override void MoveCursorRight()
        {
            _slider.value += 0.05f;
            _audioGroup.audioMixer.SetFloat("volume", SliderValueToAudioVolume(_slider.value));
        }

        private float SliderValueToAudioVolume(float sliderValue)
        {
            return sliderValue < 0.01f ? -80.0f : Mathf.Lerp(-40.0f, 0.0f, sliderValue);
        }
    }
}
