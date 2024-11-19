using UnityEngine;
using TMPro;
using Field;
using Language;
using Utils;
using UnityEngine.UI;
using UI;

namespace Save
{
    public class SaveSlot : SelectableItem
    {
        [SerializeField]
        private Transform _blankSave;
        [SerializeField]
        private Transform _saveWithData;
        [SerializeField]
        private TMP_Text _inGameTime;
        [SerializeField]
        private TMP_Text _location;
        [SerializeField]
        private Image[] _characters;

        public SavedData Data { get; private set; }
        public Transform BlankSave { get { return _blankSave; } }
        public Transform SaveWithData { get { return _saveWithData; } }
        public TMP_Text InGameTime { get { return _inGameTime; } }
        public TMP_Text Location { get { return _location; } }
        public Image[] Characters { get { return _characters; } }

        public override void ShowCursor(bool show)
        {
            GetComponent<Image>().enabled = show;
        }

        public void Init(SavedData data)
        {
            BlankSave.gameObject.SetActive(false);
            SaveWithData.gameObject.SetActive(true);

            Data = data;

            Location.text = Localizer.Instance.GetString(FieldNames.MAP_NAMES[Data.MapID]);
            InGameTime.text = TimeConverter.FormatTimeFromSeconds(data.InGameTimeSeconds);

            for(int i = 0;  i < Characters.Length; i++)
            {
                Characters[i].gameObject.SetActive(false);
            }

            for(int i = 0; i < Data.Party.Count; i++)
            {
                Characters[i].gameObject.SetActive(true);
            }

            SetSize();
        }

        public void Select()
        {
            Animator animator = GetComponent<Animator>();
            if(animator)
                animator.Play("SaveSlotSelected");
        }

        public void Unselect()
        {
            Animator animator = GetComponent<Animator>();
            if (animator)
                animator.Play("SaveSlotIdle");
        }

        private void SetSize()
        {
            RectTransform rt = GetComponent<RectTransform>();

            rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y * Screen.currentResolution.height / 1080.0f);
        }

        public void SetComponents(Transform blankSave, Transform saveWithData, TMP_Text inGameTime, TMP_Text location, Image[] characters)
        {
            _blankSave = blankSave;
            _saveWithData = saveWithData;
            _inGameTime = inGameTime;
            _location = location;
            _characters = characters;
        }
    }
}
