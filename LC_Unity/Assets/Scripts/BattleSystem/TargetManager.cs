using BattleSystem.UI;
using UnityEngine;
using System.Collections.Generic;
using Abilities;

namespace BattleSystem
{
    public class TargetManager : MonoBehaviour
    {
        [SerializeField]
        private PointAndClickSingleTargetCursor _pointAndClickSingleTargetCursorPrefab;

        private TargetCursor _instTargetCursor;

        private List<BattlerBehaviour> _availableTargets;
        
        public Ability CurrentAbility { get; private set; }

        public void LoadTargets(List<BattlerBehaviour> targets, Ability ability)
        {
            _availableTargets = new List<BattlerBehaviour>();
            _availableTargets.AddRange(targets);

            _instTargetCursor = Instantiate(_pointAndClickSingleTargetCursorPrefab, targets[0].transform.position, Quaternion.identity);
            CurrentAbility = ability;
        }

        public void ConfirmTarget(BattlerBehaviour currentBattler)
        {
            currentBattler.LockedInAbility = CurrentAbility;
            CurrentAbility.Targets = new List<BattlerBehaviour> { _availableTargets[0] };
        }

        public void Clear()
        {
            if (_instTargetCursor)
                Destroy(_instTargetCursor.gameObject);

            _availableTargets.Clear();
        }
    }
}
