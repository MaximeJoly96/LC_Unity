using UnityEngine;

namespace Save
{
    public class SaveSlot : MonoBehaviour
    {
        [SerializeField]
        private Transform _blankSave;
        [SerializeField]
        private Transform _saveWithData;

        public bool HasData { get; private set; }

        public void Init()
        {
            _blankSave.gameObject.SetActive(false);
            _saveWithData.gameObject.SetActive(true);

            HasData = true;
        }

        public void Select()
        {
            GetComponent<Animator>().Play("SaveSlotSelected");
        }

        public void Unselect()
        {
            GetComponent<Animator>().Play("SaveSlotIdle");
        }
    }
}
