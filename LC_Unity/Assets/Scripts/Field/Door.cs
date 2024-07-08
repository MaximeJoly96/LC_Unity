using Movement;
using UnityEngine;
using UnityEngine.Events;
using MusicAndSounds;
using Engine.MusicAndSounds;

namespace Field
{
    public class Door : RunnableAgent
    {
        private UnityEvent<bool> _doorStatusChanged;

        public UnityEvent<bool> DoorStatusChanged
        {
            get
            {
                if (_doorStatusChanged == null)
                    _doorStatusChanged = new UnityEvent<bool>();

                return _doorStatusChanged;
            }
        }

        [SerializeField]
        protected Transform _interior;

        public override void RunSequence()
        {
            GetComponent<Animator>().Play("StandardDoorOpen");
            FindObjectOfType<AudioPlayer>().PlaySoundEffect(new PlaySoundEffect
            {
                Name = "OpenWoodenDoor",
                Volume = 0.25f,
                Pitch = 1.0f
            });
        }

        public virtual void FinishedOpening()
        {
            _interior.gameObject.SetActive(true);

            PlayerController pc = FindObjectOfType<PlayerController>();

            pc.GetComponent<SpriteRenderer>().sortingLayerName = "InteriorPlayer";
            pc.transform.Translate(new Vector3(0.0f, 0.25f));

            FinishedSequence.Invoke();
            DoorStatusChanged.Invoke(true);
        }
    }
}
