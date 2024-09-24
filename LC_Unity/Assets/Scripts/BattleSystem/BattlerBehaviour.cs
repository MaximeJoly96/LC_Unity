using UnityEngine;
using BattleSystem.Model;
using BattleSystem.Behaviours;
using Abilities;
using System.Collections.Generic;
using Inventory;

namespace BattleSystem
{
    public class BattlerBehaviour : MonoBehaviour
    {
        [SerializeField]
        private int _battlerId;
        [SerializeField]
        private bool _isEnemy;

        public int BattlerId { get { return _battlerId; } }
        public bool IsEnemy { get { return _isEnemy; } set { _isEnemy = value; } }

        public Battler BattlerData { get; private set; }
        public Ability LockedInAbility { get; set; }
        public bool FinishedAction { get; private set; }

        public void Feed(Battler battler)
        {
            BattlerData = battler;
            _battlerId = BattlerData.Character.Id;
        }

        public void Behave(List<BattlerBehaviour> allBattlers)
        {
            BattlerBaseAi ai = GetComponent<BattlerBaseAi>();
            if(ai)
            {
                LockedInAbility = ai.Behave(allBattlers);
            }
        }

        public void FinishedAbilityMovement(BattlerBehaviour target)
        {
            if (LockedInAbility.Category == AbilityCategory.AttackCommand)
            {
                Weapon weapon = BattlerData.Character.RightHand.GetItem() as Weapon;
                LockedInAbility.AnimationId = weapon != null ? weapon.Animation : 0;
            }

            GameObject hitAnimation = Instantiate(FindObjectOfType<AttackAnimationsWrapper>().GetAttackAnimation(LockedInAbility.AnimationId));
            hitAnimation.transform.position = target.transform.position;

            AttackAnimationBehaviour aab = hitAnimation.GetComponent<AttackAnimationBehaviour>();
            aab.AbilityHitEvent.RemoveAllListeners();
            aab.AnimationEndedEvent.RemoveAllListeners();
            aab.AbilityHitEvent.AddListener(ComputeDamage);
            aab.AnimationEndedEvent.AddListener(FinishedTurn);
        }

        private void ComputeDamage()
        {
            int result = DamageFormula.ComputeResult(LockedInAbility.Id,
                                                     BattlerData.Character,
                                                     LockedInAbility.Targets[0].BattlerData.Character);

            LockedInAbility.Targets[0].BattlerData.ChangeHealth(result);

            FindObjectOfType<BattleUiManager>().DisplayDamage(LockedInAbility.Targets[0].transform.position, result);
        }

        public void FinishedTurn()
        {
            LockedInAbility = null;
            FinishedAction = true;
        }

        public void ResetTurn()
        {
            LockedInAbility = null;
            FinishedAction = false;
        }
    }
}
