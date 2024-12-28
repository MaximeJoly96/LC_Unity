using UnityEngine;
using Abilities;
using System.Collections.Generic;
using System.Linq;
using Utils;
using Inventory;
using Party;
using UnityEngine.UI;

namespace BattleSystem.UI
{
    public class MoveSelectionWindow : MonoBehaviour
    {
        public enum SelectionState { Category, Items }

        [SerializeField]
        private Transform _listWrapper;
        [SerializeField]
        private SelectableMoveCategory _selectableMoveCategoryPrefab;
        [SerializeField]
        private BattleInventoryItem _inventoryItemPrefab;

        private BattlerBehaviour _currentCharacter;
        private List<BattleMenuItem> _instMenuItems;

        private int _cursorCurrentPosition;
        private SelectionState _currentSelectionState;
        private BattleManager _battleManager;

        #region Properties
        private Animator Animator
        {
            get { return GetComponent<Animator>(); }
        }

        public Transform ListWrapper { get { return _listWrapper; } set { _listWrapper = value; } }

        private BattleManager BattleManager
        {
            get
            {
                if(!_battleManager)
                    _battleManager = FindObjectOfType<BattleManager>();

                return _battleManager;
            }
        }
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

            CreateMoveCategories();
        }

        private void CreateMoveCategories()
        {
            Clear();
            _instMenuItems = new List<BattleMenuItem>();

            CreateMoveCategory(AbilityCategory.AttackCommand);
            CreateMoveCategory(AbilityCategory.Skill);
            CreateMoveCategory(AbilityCategory.ItemCommand);
            CreateMoveCategory(AbilityCategory.FleeCommand);

            _cursorCurrentPosition = 0;
            UpdateCursor();
        }

        private void CreateMoveCategory(AbilityCategory category)
        {
            SelectableMoveCategory smc = Instantiate(_selectableMoveCategoryPrefab, _listWrapper);
            smc.Feed(category);

            _instMenuItems.Add(smc);
            smc.ShowCursor(false);
        }

        private void Clear()
        {
            foreach (Transform child in _listWrapper)
                Destroy(child.gameObject);
        }

        public void CancelSelection()
        {
            switch(_currentSelectionState)
            {
                case SelectionState.Items:
                    CommonSounds.ActionCancelled();
                    _currentSelectionState = SelectionState.Category;
                    CreateMoveCategories();
                    break;
            }
        }

        public void UpPressed()
        {
            _cursorCurrentPosition = _cursorCurrentPosition == 0 ? _instMenuItems.Count - 1 : --_cursorCurrentPosition;

            UpdateCursor();
        }

        public void DownPressed()
        {
            _cursorCurrentPosition = _cursorCurrentPosition == _instMenuItems.Count - 1 ? 0 : ++_cursorCurrentPosition;

            UpdateCursor();
        }

        private void UpdateCursor()
        {
            for(int i = 0; i < _instMenuItems.Count; i++)
            {
                _instMenuItems[i].ShowCursor(_cursorCurrentPosition == i);
            }
        }

        public void SelectMove()
        {
            switch(_currentSelectionState)
            {
                case SelectionState.Category:
                    AbilityCategory abilityCategory = (_instMenuItems[_cursorCurrentPosition] as SelectableMoveCategory).Category;
                    AbilityCategorySelected(abilityCategory);
                    break;
                case SelectionState.Items:
                    InventoryItem selectedItem = (_instMenuItems[_cursorCurrentPosition] as BattleInventoryItem).Item;
                    InventoryItemSelected(selectedItem);
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
                        CommonSounds.Error();
                    }
                    break;
                case AbilityCategory.ItemCommand:
                    CommonSounds.OptionSelected();
                    _currentSelectionState = SelectionState.Items;
                    DisplayInventory();
                    break;
            }
        }

        private void SelectTargetForAttackCommand()
        {
            BattleManager.SelectTargetWithAbility(AbilitiesManager.Instance.GetAbility(0), _currentCharacter);
        }

        private void InventoryItemSelected(InventoryItem item)
        {
            // Fundamentally, using an item is binding its properties to the ItemCommand ability
            Ability copy = new Ability(AbilitiesManager.Instance.GetAbility(46));
            copy.Effects.Clear();
            copy.SetEffects(item.ItemData.Effects);
            copy.SetAnimation((item.ItemData as Consumable).Animation);
            copy.Range = (item.ItemData as Consumable).Range;
            copy.TargetEligibility = (item.ItemData as Consumable).TargetEligibility;

            BattleManager.TargetManager.CurrentItem = item;
            BattleManager.SelectTargetWithAbility(copy, _currentCharacter);
        }

        private void DisplayInventory()
        {
            Clear();

            List<InventoryItem> itemsToDisplay = PartyManager.Instance.Inventory.Where(i => i.ItemData.Category == ItemCategory.Consumable &&
                                                                                            ((i.ItemData as Consumable).Usability == ItemUsability.Always ||
                                                                                            (i.ItemData as Consumable).Usability == ItemUsability.BattleOnly)).ToList();
            _instMenuItems = new List<BattleMenuItem>();

            for(int i = 0; i < itemsToDisplay.Count; i++)
            {
                BattleInventoryItem item = Instantiate(_inventoryItemPrefab, _listWrapper);
                item.Feed(itemsToDisplay[i]);

                _instMenuItems.Add(item);
                item.ShowCursor(false);
            }

            _cursorCurrentPosition = 0;
            UpdateCursor();
        }
    }
}
