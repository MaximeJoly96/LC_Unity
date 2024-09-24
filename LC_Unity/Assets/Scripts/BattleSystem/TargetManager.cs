using BattleSystem.UI;
using UnityEngine;
using System.Collections.Generic;
using Abilities;
using Inventory;

namespace BattleSystem
{
    public class TargetManager : MonoBehaviour
    {
        [SerializeField]
        private PointAndClickSingleTargetCursor _pointAndClickSingleTargetCursorPrefab;

        private TargetCursor _instTargetCursor;
        private int _cursorPosition;

        private List<BattlerBehaviour> _availableTargets;
        
        public Ability CurrentAbility { get; private set; }
        public BattlerBehaviour CurrentlySelectedTarget
        {
            get { return _availableTargets[_cursorPosition]; }
        }

        public void LoadTargets(List<BattlerBehaviour> targets, Ability ability)
        {
            _availableTargets = new List<BattlerBehaviour>();
            _availableTargets.AddRange(targets);
            _cursorPosition = 0;

            _instTargetCursor = Instantiate(_pointAndClickSingleTargetCursorPrefab, targets[_cursorPosition].transform.position, Quaternion.identity);
            CurrentAbility = ability;
        }

        public void ConfirmTarget(BattlerBehaviour currentBattler)
        {
            currentBattler.LockedInAbility = new Ability(CurrentAbility);
            currentBattler.LockedInAbility.Targets = new List<BattlerBehaviour> { _availableTargets[_cursorPosition] };

            if(currentBattler.LockedInAbility.Category == AbilityCategory.AttackCommand)
            {
                Weapon weapon = currentBattler.BattlerData.Character.RightHand.GetItem() as Weapon;
                currentBattler.LockedInAbility.Range = weapon != null ? weapon.Range : 100;
            }
        }

        public void Clear()
        {
            if (_instTargetCursor)
                Destroy(_instTargetCursor.gameObject);

            _availableTargets.Clear();
        }

        public void NextTarget()
        {
            _cursorPosition = _cursorPosition == _availableTargets.Count - 1 ? 0 : ++_cursorPosition;
            UpdateCursor();
        }

        public void PreviousTarget()
        {
            _cursorPosition = _cursorPosition == 0 ? _availableTargets.Count - 1 : --_cursorPosition;
            UpdateCursor();
        }

        private void UpdateCursor()
        {
            _instTargetCursor.transform.position = _availableTargets[_cursorPosition].transform.position;
        }
    }
}
