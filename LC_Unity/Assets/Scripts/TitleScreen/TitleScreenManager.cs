using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Party;
using Inputs;

namespace TitleScreen
{
    public class TitleScreenManager : MonoBehaviour
    {
        internal enum Options { NewGame, LoadGame, ChangeOptions, Quit }

        private const float SELECTION_DELAY = 0.2f; // seconds
        private const float LOAD_DELAY = 1.0f;

        [SerializeField]
        private TextAsset _charactersData;
        [SerializeField]
        private TitleScreenOption[] _options;

        private int _cursorPosition;
        private float _selectionDelay;
        private bool _delayOn;
        private bool _lockedChoice;

        private void Start()
        {
            _selectionDelay = 0.0f;
            _delayOn = false;
            UpdateCursor();

            PartyManager.Instance.LoadPartyFromBaseFile(_charactersData);
            FindObjectOfType<InputController>().ButtonClicked.AddListener(ReceiveInput);
        }

        private IEnumerator LoadNextScene()
        {
            yield return new WaitForSeconds(LOAD_DELAY);
            SceneManager.LoadScene("Field");
        }

        private IEnumerator Quit()
        {
            yield return new WaitForSeconds(LOAD_DELAY);
            Application.Quit();
        }

        private void ReceiveInput(InputAction input)
        {
            if(!_delayOn)
            {
                switch (input)
                {
                    case InputAction.MoveDown:
                        MoveCursorDown();
                        break;
                    case InputAction.MoveUp:
                        MoveCursorUp();
                        break;
                    case InputAction.Select:
                        SelectOption();
                        break;
                }

                _delayOn = true;
            }
        }

        private void Update()
        {
            if(_delayOn && !_lockedChoice)
            {
                _selectionDelay += Time.deltaTime;
                if(_selectionDelay > SELECTION_DELAY)
                {
                    _selectionDelay = 0.0f;
                    _delayOn = false;
                }
            }
        }

        private void MoveCursorDown()
        {
            _cursorPosition = _cursorPosition == _options.Length - 1 ? 0 : ++_cursorPosition;
            UpdateCursor();
        }

        private void MoveCursorUp()
        {
            _cursorPosition = _cursorPosition == 0 ? _options.Length - 1 : --_cursorPosition;
            UpdateCursor();
        }

        private void SelectOption()
        {
            _lockedChoice = true;

            switch(_options[_cursorPosition].Option)
            {
                case Options.NewGame:
                    StartCoroutine(LoadNextScene());
                    break;
                case Options.LoadGame:
                    break;
                case Options.ChangeOptions:
                    break;
                case Options.Quit:
                    StartCoroutine(Quit());
                    break;
            }
        }

        private void UpdateCursor()
        {
            for(int i = 0; i < _options.Length; i++)
            {
                _options[i].ShowCursor(_cursorPosition == i);
            }
        }
    }
}
