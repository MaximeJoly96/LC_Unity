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

        private void OnTriggerEnter2D(Collider2D other)
        {
            BattlerBehaviour collidedWith = other.GetComponent<BattlerBehaviour>();
            if(collidedWith != null && LockedInAbility != null)
            {
                switch (LockedInAbility.TargetEligibility)
                {
                    case TargetEligibility.Any:
                        if (collidedWith.IsEnemy != IsEnemy)
                        {
                            if (LockedInAbility.Category == AbilityCategory.AttackCommand)
                            {
                                Weapon weapon = BattlerData.Character.RightHand.GetItem() as Weapon;
                                LockedInAbility.AnimationId = weapon.Animation;
                            }

                            GameObject hitAnimation = Instantiate(FindObjectOfType<AttackAnimationsWrapper>().GetAttackAnimation(LockedInAbility.AnimationId));
                            hitAnimation.transform.position = collidedWith.transform.position;
                        }
                        break;
                    case TargetEligibility.Enemy:
                    case TargetEligibility.All:
                        if (collidedWith.IsEnemy != IsEnemy)
                            Debug.Log(gameObject.name + " strikes " + collidedWith.name);
                        break;
                }
            }
        }
    }
}
