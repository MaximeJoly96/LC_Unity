using UnityEngine;
using UnityEngine.UI;

namespace TitleScreen
{
    public class VolumeEditionOption : SpecificTitleScreenOption
    {
        public enum VolumeOption { Music, Sfx }

        [SerializeField]
        private VolumeOption _volumeOption;
        [SerializeField]
        private Slider _slider;

        public override void MoveCursorLeft()
        {
            _slider.value -= 0.05f;
        }

        public override void MoveCursorRight()
        {
            _slider.value += 0.05f;
        }
    }
}
