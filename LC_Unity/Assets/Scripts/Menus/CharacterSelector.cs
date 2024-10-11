using UnityEngine;
using Actors;
using System.Collections.Generic;
using Core;
using System.Collections;
using UnityEngine.Events;
using Utils;

namespace Menus
{
    public class CharacterSelector : MonoBehaviour
    {
        [SerializeField]
        private CharacterPreview _characterPreviewPrefab;

        private InputReceiver _inputReceiver;
        private List<CharacterPreview> _previews;
        private int _cursorPosition;
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
            _previews = new List<CharacterPreview>();
            Init();
            BindInputs();
        }

        private void BindInputs()
        {
            _inputReceiver = FindObjectOfType<InputReceiver>();

            _inputReceiver.OnMoveDown.AddListener(() =>
            {
                if(CanReceiveInput())
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

            _inputReceiver.OnMoveLeft.AddListener(() =>
            {
                if (CanReceiveInput())
                {
                    CommonSounds.CursorMoved();
                    MoveCursorLeft();
                }
            });

            _inputReceiver.OnMoveRight.AddListener(() =>
            {
                if (CanReceiveInput())
                {
                    CommonSounds.CursorMoved();
                    MoveCursorRight();
                }
            });

            _inputReceiver.OnSelect.AddListener(() =>
            {
                if (CanReceiveInput())
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
                    StartCoroutine(ReturnToMainMenu());
                }
            });
        }

        private bool CanReceiveInput()
        {
            return GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.SelectingCharacterPreview;
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
