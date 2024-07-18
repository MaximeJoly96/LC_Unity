using Movement;
using UnityEngine;
using UnityEngine.Events;
using MusicAndSounds;
using Engine.MusicAndSounds;
using System.Collections;

namespace Field
{
    public class Door : RunnableAgent
    {
        public enum DoorType { ToInterior, ToOutside, ChangeFloor }

        [SerializeField]
        protected Transform _interiorToShow;
        [SerializeField]
        protected Transform _interiorToHide;
        [SerializeField]
        protected Vector3 _playerPositionShift;
        [SerializeField]
        protected Vector3 _playerScale;
        [SerializeField]
        protected DoorType _type;

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

        public override void RunSequence()
        {
            if(_type != DoorType.ChangeFloor)
            {
                FindObjectOfType<AudioPlayer>().PlaySoundEffect(new PlaySoundEffect
                {
                    Name = "OpenWoodenDoor",
                    Volume = 0.25f,
                    Pitch = 1.0f
                });
            }

            if (_type == DoorType.ToInterior)
            {
                Animator animator = GetComponent<Animator>();

                if (animator)
                    animator.Play("StandardDoorOpen");

                return;
            }

            StartCoroutine(Exit());
            
        }

        public virtual void FinishedOpening()
        {
            PlayerController pc = FindObjectOfType<PlayerController>();

            switch (_type)
            {
                case DoorType.ToInterior:
                    _interiorToShow.gameObject.SetActive(true);
                    pc.GetComponent<SpriteRenderer>().sortingLayerName = "InteriorPlayer";
                    break;
                case DoorType.ToOutside:
                    _interiorToHide.gameObject.SetActive(false);
                    pc.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
                    FindObjectOfType<FieldBuilder>().SwitchToInteriorMode(false);
                    break;
                case DoorType.ChangeFloor:
                    _interiorToHide.gameObject.SetActive(false);
                    _interiorToShow.gameObject.SetActive(true);
                    pc.GetComponent<SpriteRenderer>().sortingLayerName = "InteriorPlayer";
                    break;
            }

            pc.transform.Translate(_playerPositionShift);
            pc.transform.localScale = _playerScale;

            FinishedSequence.Invoke();
            DoorStatusChanged.Invoke(_type != DoorType.ToOutside);
        }

        protected IEnumerator Exit()
        {
            yield return null;

            FinishedOpening();
        }
    }
}
