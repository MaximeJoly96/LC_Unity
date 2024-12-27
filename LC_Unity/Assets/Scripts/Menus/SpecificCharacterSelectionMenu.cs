using UnityEngine;
using System.Collections;
using Core;
using Inputs;
using Menus.SubMenus.Items;
using Party;
using System.Collections.Generic;
using Actors;
using Utils;
using System.Linq;
using Effects;

namespace Menus
{
    public class SpecificCharacterSelectionMenu : MonoBehaviour
    {
        [SerializeField]
        private TargetableCharacter _targetableCharacterPrefab;
        [SerializeField]
        private Transform _wrapper;

        private InputReceiver _inputReceiver;
        private SelectableInventoryItem _selectedItem;
        private List<TargetableCharacter> _characters;

        private GlobalStateMachine.State _previousState;
        private int _cursorPosition;

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
                    SelectCharacter();
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

            _inputReceiver.OnMoveDown.AddListener(() =>
            {
                if (CanReceiveInput())
                {
                    CommonSounds.CursorMoved();
                    MoveCursorDown();
                }
            });

            _inputReceiver.OnMoveUp.AddListener(() =>
            {
                if (CanReceiveInput())
                {
                    CommonSounds.CursorMoved();
                    MoveCursorUp();
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

        public void FeedItem(SelectableInventoryItem item)
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
                character.ShowCursor(false);
            }

            _cursorPosition = 0;
            PlaceCursor();
        }

        private void Clear()
        {
            foreach(Transform child in _wrapper)
            {
                Destroy(child.gameObject);
            }
        }

        private void MoveCursorDown()
        {
            _cursorPosition = _cursorPosition >= _characters.Count - 1 ? 0 : ++_cursorPosition;
            PlaceCursor();
        }

        private void MoveCursorUp()
        {
            _cursorPosition = _cursorPosition == 0 ? _characters.Count - 1 : --_cursorPosition;
            PlaceCursor();
        }

        private void PlaceCursor()
        {
            for(int i = 0; i < _characters.Count; i++)
            {
                _characters[i].ShowCursor(i == _cursorPosition);
            }
        }

        private void SelectCharacter()
        {
            bool eligible = true;

            for(int i = 0; i < _selectedItem.Item.ItemData.Effects.Count && eligible; i++)
            {
                eligible = _characters[_cursorPosition].Character.EligibleForMenuEffect(_selectedItem.Item.ItemData.Effects[i]);
            }

            if(eligible)
                Debug.Log("Selected " + _characters[_cursorPosition].Character.Name + " with " + _selectedItem.Item.ItemData.Name);
        }
    }
}
