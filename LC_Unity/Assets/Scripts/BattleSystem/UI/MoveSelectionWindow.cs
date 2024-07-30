using UnityEngine;
using TMPro;
using Abilities;
using System.Collections.Generic;
using System.Linq;

namespace BattleSystem.UI
{
    public class MoveSelectionWindow : MonoBehaviour
    {
        public enum SelectionState { Category }

        [SerializeField]
        private TMP_Text _instructionsText;
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

        private Animator Animator
        {
            get { return GetComponent<Animator>(); }
        }

        public void UpdateInstructions(string characterName)
        {
            _instructionsText.text = "Select " + characterName + "'s next move.";
        }

        public void Show()
        {
            Animator.Play("Show");
        }

        public void Feed(BattlerBehaviour character)
        {
            _currentCharacter = character;
            _currentSelectionState = SelectionState.Category;

            UpdateInstructions(_currentCharacter.BattlerData.Name);
            CreateMoveCategories(_currentCharacter.BattlerData.Abilities);
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
                    Debug.Log("Need to implement skill selection");
                    break;
                case AbilityCategory.AttackCommand:
                    SelectTargetForAttackCommand();
                    break;
                case AbilityCategory.FleeCommand:
                    Debug.Log("Need to implement flee");
                    break;
            }
        }

        private void SelectTargetForAttackCommand()
        {
            FindObjectOfType<BattleManager>().SelectTargetWithAbility(AbilitiesManager.Instance.GetAbility(0));
        }
    }
}
