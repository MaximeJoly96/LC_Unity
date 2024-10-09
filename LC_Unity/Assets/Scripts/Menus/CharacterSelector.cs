using UnityEngine;
using Actors;
using System.Collections.Generic;
using Inputs;
using Core;
using System.Collections;
using UnityEngine.Events;
using Utils;

namespace Menus
{
    public class CharacterSelector : MonoBehaviour
    {
        private const float DELAY_BETWEEN_ACTIONS = 0.2f; // seconds

        [SerializeField]
        private CharacterPreview _characterPreviewPrefab;

        private List<CharacterPreview> _previews;
        private InputController _inputController;
        private bool _busy;
        private int _cursorPosition;
        private float _selectionDelay;
        private UnityEvent<Character> _characterSelected;
        
        public UnityEvent<Character> CharacterSelected
        {
            get
            {
                if (_characterSelected == null)
                    _characterSelected = new UnityEvent<Character>();

                return _characterSelected;
            }
        }

        public List<CharacterPreview> Previews { get { return _previews; } }
        public int CursorPosition { get { return _cursorPosition; } }

        private void Start()
        {
            _inputController = FindObjectOfType<InputController>();
            _inputController.ButtonClicked.AddListener(HandleInputs);

            _previews = new List<CharacterPreview>();
            Init();
        }

        public void Init()
        {
            _cursorPosition = 0;
        }

        public void Feed(List<Character> characters)
        {
            for (int i = 0; i < characters.Count; i++)
            {
                CharacterPreview preview = Instantiate(_characterPreviewPrefab, transform);
                preview.Feed(characters[i]);

                _previews.Add(preview);
            }
        }

        public void SetPreviewPrefab(CharacterPreview prefab)
        {
            _characterPreviewPrefab = prefab;
        }

        public void Clear()
        {
            foreach (Transform child in transform)
                Destroy(child.gameObject);

            _previews.Clear();
        }

        public void HoverCharacters()
        {
            Init();
            _previews[_cursorPosition].Hover();
        }

        private void HandleInputs(InputAction input)
        {
            if(!_busy && GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.SelectingCharacterPreview)
            {
                switch(input)
                {
                    case InputAction.MoveDown:
                        CommonSounds.CursorMoved();
                        MoveCursorDown();
                        break;
                    case InputAction.MoveUp:
                        CommonSounds.CursorMoved();
                        MoveCursorUp();
                        break;
                    case InputAction.MoveLeft:
                        CommonSounds.CursorMoved();
                        MoveCursorLeft();
                        break;
                    case InputAction.MoveRight:
                        CommonSounds.CursorMoved();
                        MoveCursorRight();
                        break;
                    case InputAction.Cancel:
                        CommonSounds.ActionCancelled();
                        StartCoroutine(ReturnToMainMenu());
                        break;
                    case InputAction.Select:
                        CommonSounds.OptionSelected();
                        SelectCharacter();
                        break;
                }

                _busy = true;
            }
        }

        private void MoveCursorDown()
        {
            if(_cursorPosition < _previews.Count - 2)
            {
                _cursorPosition += 2;
                UnselectAllPreviews();
                _previews[_cursorPosition].Hover();
            }
        }

        private void MoveCursorUp()
        {
            if (_cursorPosition > 1)
            {
                _cursorPosition -= 2;
                UnselectAllPreviews();
                _previews[_cursorPosition].Hover();
            }
        }

        private void MoveCursorLeft()
        {
            if (_cursorPosition > 0)
            {
                _cursorPosition--;
                UnselectAllPreviews();
                _previews[_cursorPosition].Hover();
            }
        }

        private void MoveCursorRight()
        {
            if (_cursorPosition < _previews.Count - 1)
            {
                _cursorPosition++;
                UnselectAllPreviews();
                _previews[_cursorPosition].Hover();
            }
        }

        private void UnselectAllPreviews()
        {
            for(int i = 0; i < _previews.Count; i++)
            {
                _previews[i].Unselect();
            }
        }

        private void Update()
        {
            if (_busy)
            {
                _selectionDelay += Time.deltaTime;

                if (_selectionDelay >= DELAY_BETWEEN_ACTIONS)
                {
                    _selectionDelay = 0.0f;
                    _busy = false;
                }
            }
        }

        private IEnumerator ReturnToMainMenu()
        {
            UnselectAllPreviews();

            yield return new WaitForSeconds(0.2f);
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.InMenu);
        }

        private void SelectCharacter()
        {
            CharacterSelected.Invoke(_previews[_cursorPosition].Character);
        }
    }
}
