using UnityEngine;

namespace Save
{
    public class SaveSlot : MonoBehaviour
    {
        [SerializeField]
        private Transform _blankSave;
        [SerializeField]
        private Transform _saveWithData;

        public void Init()
        {
            _blankSave.gameObject.SetActive(false);
            _saveWithData.gameObject.SetActive(true);
        }
    }
}
