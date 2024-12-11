using UnityEngine;
using TMPro;
using Abilities;
using System.Collections.Generic;
using System.Linq;
using MusicAndSounds;
using Utils;

namespace BattleSystem.UI
{
    public class MoveSelectionWindow : MonoBehaviour
    {
        public enum SelectionState { Category }

        [SerializeField]
        private Transform _cursor;
        [SerializeField]
        private Transform _listWrapper;
        [SerializeField]
        private SelectableMoveCategory _selectableMoveCategoryPrefab;

        private BattlerBehaviour _currentCharacter;
        private List<SelectableMoveCategory> _instMoveCategories;

        private int _cursorCurrentPosition;
        private SelectionState _currentSelectionState;

        #region Properties
        private Animator Animator
        {
            get { return GetComponent<Animator>(); }
        }

        public Transform Cursor { get { return _cursor; } set { _cursor = value; } }
        public Transform ListWrapper { get { return _listWrapper; } set { _listWrapper = value; } }
        #endregion

        public void Show()
        {
            Animator.Play("Show");
        }

        public void Hide()
        {
            Animator.Play("Hide");
        }

        public void Feed(BattlerBehaviour character)
        {
            _currentCharacter = character;
            _currentSelectionState = SelectionState.Category;

            CreateMoveCategories(_currentCharacter.BattlerData.Character.Abilities);
        }

        private void CreateMoveCategories(List<Ability> abilities)
        {
            Clear();
            _instMoveCategories = new List<SelectableMoveCategory>();

            foreach(Ability ability in abilities)
            {
                if(_instMoveCategories.FirstOrDefault(c => c.Category == ability.Category) == null)
                {
                    SelectableMoveCategory smc = Instantiate(_selectableMoveCategoryPrefab, _listWrapper);
                    smc.Feed(ability.Category);

                    _instMoveCategories.Add(smc);
                }
            }
        }

        private void Clear()
        {
            foreach (Transform child in _listWrapper)
                Destroy(child.gameObject);
        }

        public void UpPressed()
        {
            switch(_currentSelectionState)
            {
                case SelectionState.Category:
                    _cursorCurrentPosition = _cursorCurrentPosition == 0 ? _instMoveCategories.Count - 1 : --_cursorCurrentPosition;
                    break;
            }

            UpdateCursor();
        }

        public void DownPressed()
        {
            switch(_currentSelectionState)
            {
                case SelectionState.Category:
                    _cursorCurrentPosition = _cursorCurrentPosition == _instMoveCategories.Count - 1 ? 0 : ++_cursorCurrentPosition;
                    break;
            }

            UpdateCursor();
        }

        private void UpdateCursor()
        {
            switch(_currentSelectionState)
            {
                case SelectionState.Category:
                    _cursor.transform.position = _instMoveCategories[_cursorCurrentPosition].transform.position;
                    break;
            }
        }

        public void SelectMove()
        {
            switch(_currentSelectionState)
            {
                case SelectionState.Category:
                    AbilityCategory abilityCategory = _instMoveCategories[_cursorCurrentPosition].Category;

                    AbilityCategorySelected(abilityCategory);
                    break;
            }
        }

        private void AbilityCategorySelected(AbilityCategory abilityCategory)
        {
            switch(abilityCategory)
            {
                case AbilityCategory.Skill:
                    CommonSounds.OptionSelected();
                    Debug.Log("Need to implement skill selection");
                    break;
                case AbilityCategory.AttackCommand:
                    CommonSounds.OptionSelected();
                    SelectTargetForAttackCommand();
                    break;
                case AbilityCategory.FleeCommand:
                    if(BattleDataHolder.Instance.BattleData.CanEscape)
                    {
                        CommonSounds.OptionSelected();
                        Debug.Log("Need to implement flee");
                    }
                    else
                    {
                        FindObjectOfType<AudioPlayer>().PlaySoundEffect(new Engine.MusicAndSounds.PlaySoundEffect
                        {
                            Name = "Error1",
                            Pitch = 1.0f,
                            Volume = 1.0f
                        });
                    }
                    break;
            }
        }

        private void SelectTargetForAttackCommand()
        {
            FindObjectOfType<BattleManager>().SelectTargetWithAbility(AbilitiesManager.Instance.GetAbility(0));
        }
    }
}
