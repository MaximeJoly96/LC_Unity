using UnityEngine;
using TMPro;
using Field;
using Language;
using Utils;
using UI;
using Save.Model;

namespace Save
{
    public class SaveSlot : SelectableItem
    {
        [SerializeField]
        private TMP_Text _inGameTime;
        [SerializeField]
        private TMP_Text _slotIdLabel;

        public SavedData Data { get; private set; }
        public TMP_Text InGameTime { get { return _inGameTime; } set { _inGameTime = value; } }
        public TMP_Text SlotIdLabel { get { return _slotIdLabel; } set { _slotIdLabel = value; } }
        public Animator Animator
        {
            get
            {
                return GetComponent<Animator>();
            }
        }

        public void Init(SaveDescriptor descriptor)
        {
            SlotIdLabel.text = descriptor.Id.ToString();

            if (descriptor.MapId == -1)
            {
                Label.text = Localizer.Instance.GetString("noSaveData");
                InGameTime.text = string.Empty;
                return;
            }

            Label.text = Localizer.Instance.GetString(FieldNames.MAP_NAMES[descriptor.MapId]);
            InGameTime.text = TimeConverter.FormatTimeFromSeconds(descriptor.InGameTime);
        }

        public override void ShowCursor(bool show)
        {
            base.ShowCursor(show);
            if(Animator)
                Animator.Play(show ? "Hovered" : "Idle");
        }
    }
}
