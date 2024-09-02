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
        private const float DELAY_BETWEEN_ACTIONS = 0.2f;

        [SerializeField]
        private TargetableCharacter _targetableCharacterPrefab;
        [SerializeField]
        private Transform _wrapper;

        private SelectableItem _selectedItem;
        private bool _busy;
        private float _delay;
        private List<TargetableCharacter> _characters;

        private GlobalStateMachine.State _previousState;

        private void Start()
        {
            FindObjectOfType<InputController>().ButtonClicked.AddListener(HandleInputs);
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

        private void HandleInputs(InputAction input)
        {
            if(!_busy && GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InCharacterTargetMenu)
            {
                switch(input)
                {
                    case InputAction.Select:
                        CommonSounds.OptionSelected();
                        break;
                    case InputAction.Cancel:
                        CommonSounds.ActionCancelled();
                        Close();
                        break;
                }

                _busy = true;
            }
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

        private void Update()
        {
            if (_busy)
            {
                _delay += Time.deltaTime;

                if (_delay >= DELAY_BETWEEN_ACTIONS)
                {
                    _delay = 0.0f;
                    _busy = false;
                }
            }
        }
    }
}
