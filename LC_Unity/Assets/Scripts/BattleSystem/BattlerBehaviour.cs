using UnityEngine;
using BattleSystem.Model;
using BattleSystem.Behaviours;
using Abilities;
using System.Collections.Generic;
using Inventory;
using UnityEngine.Audio;
using MusicAndSounds;
using Effects;
using System;
using System.Collections;
using System.Linq;
using Actions;
using System.Xml.Serialization;
using BattleSystem.Behaviours.AiBehaviours;

namespace BattleSystem
{
    public class BattlerBehaviour : MonoBehaviour
    {
        [SerializeField]
        private int _battlerId;
        [SerializeField]
        private bool _isEnemy;
        private BattleUiManager _uiManager;

        public int BattlerId { get { return _battlerId; } }
        public bool IsEnemy { get { return _isEnemy; } set { _isEnemy = value; } }
        public bool IsDead { get; private set; }

        public Battler BattlerData { get; private set; }
        public Ability LockedInAbility { get; set; }
        public bool FinishedAction { get; private set; }
        public BattleUiManager UiManager
        {
            get
            {
                if (!_uiManager)
                    _uiManager = FindObjectOfType<BattleUiManager>();

                return _uiManager;
            }
        }

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
            if (IsDead)
                return;

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
            aab.AbilityHitEvent.AddListener(Strike);
            aab.AnimationEndedEvent.AddListener(FinishedTurn);
        }

        private void Strike()
        {
            Strike(false);
        }

        private void Strike(bool secondaryHit)
        {
            ApplyAbilityEffects(LockedInAbility, secondaryHit);

            int result = DamageFormula.ComputeResult(LockedInAbility.Id,
                                                     BattlerData.Character,
                                                     LockedInAbility.Targets[0].BattlerData.Character);

            LockedInAbility.Targets[0].BattlerData.ChangeHealth(result);

            UiManager.DisplayDamage(LockedInAbility.Targets[0].transform.position, result);
            UiManager.UpdatePlayerGui(LockedInAbility.Targets[0].BattlerData.Character);
        }

        private void ApplyAbilityEffects(Ability ability, bool secondaryHit)
        {
            for(int i = 0; i < ability.Effects.Count; i++)
            {
                if (!secondaryHit && ability.Effects[i] is AdditionalStrike)
                {
                    if (ability.Category == AbilityCategory.AttackCommand)
                        Strike(true);
                }
                else if (ability.Effects[i] is AreaOfEffectAsSecondaryDamage)
                {
                    // TODO
                }
                else if(ability.Effects[i] is AutoAttackAfterAbility)
                {
                    // TODO
                }
                else if(ability.Effects[i] is Dispel)
                {
                    Dispel dispel = ability.Effects[i] as Dispel;
                    if (ability.Targets[0].BattlerData.Character.ActiveEffects.Any(e => e.Effect == dispel.Value))
                    {
                        dispel.Apply(ability.Targets[0].BattlerData.Character);
                        UiManager.RemoveStatus(ability.Targets[0].transform.position, dispel.Value);
                    }
                }
                else if (ability.Effects[i] is DrainFromDamage)
                {
                    // TODO
                }
                else if (ability.Effects[i] is ElementalAffinityExploitManaRefund)
                {
                    // TODO
                }
                else if (ability.Effects[i] is ElementalAffinityExploitSelfStatus)
                {
                    // TODO
                }
                else if (ability.Effects[i] is HpThresholdBonusDamage)
                {
                    // TODO
                }
                else if (ability.Effects[i] is Effects.InflictStatus)
                {
                    Effects.InflictStatus inflictStatus = ability.Effects[i] as Effects.InflictStatus;
                    if (!ability.Targets[0].BattlerData.Character.ActiveEffects.Any(e => e.Effect == inflictStatus.Value))
                    {
                        inflictStatus.Apply(ability.Targets[0].BattlerData.Character);
                        UiManager.DisplayStatus(ability.Targets[0].transform.position, inflictStatus.Value);
                    }
                }   
                else if (ability.Effects[i] is NegativeStatusBonusDamage)
                {
                    // TODO
                }

            }
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

        private void Update()
        {
            if (!IsDead && BattlerData.Character != null && BattlerData.Character.CurrentHealth <= 0)
                Die();
        }

        public void Die()
        {
            IsDead = true;
            GetComponent<Animator>().Play("Die");
        }

        public void PlaySound(string key)
        {
            FindObjectOfType<AudioPlayer>().PlaySoundEffect(new Engine.MusicAndSounds.PlaySoundEffect
            {
                Name = key,
                Volume = 0.75f,
                Pitch = 1.0f
            });
        }
    }
}
