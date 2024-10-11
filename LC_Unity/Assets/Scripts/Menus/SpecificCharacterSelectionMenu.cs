using UnityEngine;
using System.Collections;
using Core;
using Inputs;
using Menus.SubMenus.Items;
using Party;
using System.Collections.Generic;
using Actors;
using Utils;

namespace Menus
{
    public class SpecificCharacterSelectionMenu : MonoBehaviour
    {
        [SerializeField]
        private TargetableCharacter _targetableCharacterPrefab;
        [SerializeField]
        private Transform _wrapper;

        private InputReceiver _inputReceiver;
        private SelectableItem _selectedItem;
        private List<TargetableCharacter> _characters;

        private GlobalStateMachine.State _previousState;

        private void Start()
        {
            BindInputs();
        }

        private void BindInputs()
        {
            _inputReceiver = GetComponent<InputReceiver>();

            _inputReceiver.OnSelect.AddListener(() =>
            {
                if(CanReceiveInput())
                {
                    CommonSounds.OptionSelected();
                }
            });

            _inputReceiver.OnCancel.AddListener(() =>
            {
                if (CanReceiveInput())
                {
                    CommonSounds.ActionCancelled();
                    Close();
                }
            });
        }

        private bool CanReceiveInput()
        {
            return GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InCharacterTargetMenu;
        }

        public void Open()
        {
            Clear();
            Init();
            _previousState = GlobalStateMachine.Instance.CurrentState;
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.TransitionCharacterTarget);
            StartCoroutine(DoOpen());
        }

        private IEnumerator DoOpen()
        {
            CanvasGroup group = GetComponent<CanvasGroup>();
            float currentAlpha = group.alpha;
            WaitForFixedUpdate wait = new WaitForFixedUpdate();

            for (float i = currentAlpha; i < 1.0f; i += 0.05f)
            {
                group.alpha = i;
                yield return wait;
            }

            group.alpha = 1.0f;
            group.interactable = true;
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.InCharacterTargetMenu);
        }

        public void Close()
        {
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.TransitionCharacterTarget);
            StartCoroutine(DoClose());
        }

        private IEnumerator DoClose()
        {
            CanvasGroup group = GetComponent<CanvasGroup>();
            group.interactable = false;
            float currentAlpha = group.alpha;
            WaitForFixedUpdate wait = new WaitForFixedUpdate();

            for (float i = currentAlpha; i > 0.0f; i -= 0.05f)
            {
                group.alpha = i;
                yield return wait;
            }

            group.alpha = 0.0f;
            GlobalStateMachine.Instance.UpdateState(_previousState);
        }

        public void FeedItem(SelectableItem item)
        {
            _selectedItem = item;
        }

        private void Init()
        {
            List<Character> party = PartyManager.Instance.GetParty();
            _characters = new List<TargetableCharacter>();

            for (int i = 0; i < party.Count; i++)
            {
                TargetableCharacter character = Instantiate(_targetableCharacterPrefab, _wrapper);
                character.Feed(party[i]);

                _characters.Add(character);
            }
        }

        private void Clear()
        {
            foreach(Transform child in _wrapper)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
