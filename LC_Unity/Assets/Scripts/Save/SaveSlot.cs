using UnityEngine;
using TMPro;
using Field;
using Language;
using Utils;
using UnityEngine.UI;

namespace Save
{
    public class SaveSlot : MonoBehaviour
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

        public void Init(SavedData data)
        {
            _blankSave.gameObject.SetActive(false);
            _saveWithData.gameObject.SetActive(true);

            Data = data;

            _location.text = Localizer.Instance.GetString(FieldNames.MAP_NAMES[Data.MapID]);
            _inGameTime.text = TimeConverter.FormatTimeFromSeconds(data.InGameTimeSeconds);

            for(int i = 0;  i < _characters.Length; i++)
            {
                _characters[i].gameObject.SetActive(false);
            }

            for(int i = 0; i < Data.Party.Count; i++)
            {
                _characters[i].gameObject.SetActive(true);
            }

            SetSize();
        }

        public void Select()
        {
            GetComponent<Animator>().Play("SaveSlotSelected");
        }

        public void Unselect()
        {
            GetComponent<Animator>().Play("SaveSlotIdle");
        }

        public bool IsInsideRect(Vector2 position)
        {
            RectTransform rt = GetComponent<RectTransform>();

            return position.x >= rt.position.x - rt.rect.width / 2 && position.x <= rt.position.x + rt.rect.width / 2 &&
                   position.y >= rt.position.y - rt.rect.height / 2 && position.y <= rt.position.y + rt.rect.height / 2;
        }

        private void SetSize()
        {
            RectTransform rt = GetComponent<RectTransform>();

            rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y * Screen.currentResolution.height / 1080.0f);
        }
    }
}
