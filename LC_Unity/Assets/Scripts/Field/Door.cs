using Movement;
using UnityEngine;

namespace Field
{
    public class Door : RunnableAgent
    {
        private bool _isOpen;

        [SerializeField]
        private Transform _interior;

        public override void RunSequence()
        {
            GetComponent<Animator>().Play(_isOpen ? "StandardDoorClose" : "StandardDoorOpen");
        }

        public void FinishedOpening()
        {
            _isOpen = true;
            _interior.gameObject.SetActive(true);

            PlayerController pc = FindObjectOfType<PlayerController>();

            pc.GetComponent<SpriteRenderer>().sortingLayerName = "InteriorPlayer";
            pc.transform.Translate(new Vector3(0.0f, 0.5f));
        }

        public void FinishedClosing()
        {
            _isOpen = false;
            _interior.gameObject.SetActive(false);
        }
    }
}
