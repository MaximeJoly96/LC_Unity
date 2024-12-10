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
using UnityEditor.Animations;
using static UnityEngine.GraphicsBuffer;

namespace BattleSystem
{
    public class BattlerBehaviour : MonoBehaviour
    {
        [SerializeField]
        private int _battlerId;
        [SerializeField]
        private bool _isEnemy;
        private BattleUiManager _uiManager;
        private AttackAnimationsWrapper _attackAnimationsWrapper;

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

        public AttackAnimationsWrapper AttackAnimationsWrapper
        {
            get
            {
                if(!_attackAnimationsWrapper)
                    _attackAnimationsWrapper = FindObjectOfType<AttackAnimationsWrapper>();

                return _attackAnimationsWrapper;
            }
        }

        public Animator Animator
        {
            get { return GetComponent<Animator>(); }
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
                Weapon weapon = BattlerData.Character.Equipment.RightHand.GetItem() as Weapon;
                LockedInAbility.Animation = weapon != null ? weapon.Animation : null;
            }

            ProcessAbilityAfterMovement(LockedInAbility);
        }

        public void ProcessAbilityAfterMovement(Ability ability)
        {
            if(ability.HasChannelAnimation)
                Animator.SetBool(ability.Animation.BattlerChannelAnimationName, true);
            else if(ability.HasStrikeAnimation)
                Animator.SetBool(ability.Animation.BattlerStrikeAnimationName, true);
        }

        public void ChannelBreakpoint()
        {
            if(LockedInAbility.HasProjectile)
            {
                foreach(BattlerBehaviour target in LockedInAbility.Targets)
                {
                    ProjectileTrajectory trajectory = new ProjectileTrajectory();
                    trajectory.AddCheckpoint(gameObject.transform.position);
                    trajectory.AddCheckpoint(target.transform.position);

                    AbilityProjectile projectile = LockedInAbility.Animation.CreateProjectile(gameObject.transform.position,
                                                                                              LockedInAbility.Animation.Projectile.Speed,
                                                                                              trajectory);
                    projectile.TargetEligibility = LockedInAbility.TargetEligibility;
                    projectile.OriginBattler = this;
                    projectile.ProjectileDestroyed.AddListener(ProjectileWasDestroyed);
                    projectile.StartMoving();
                }
            }
        }

        private void ProjectileWasDestroyed(BattlerBehaviour target)
        {
            if(target)
            {
                Strike(false, target);
            }
        }

        public void ShowChannelParticles()
        {
            if(LockedInAbility.HasChannelParticles)
            {
                LockedInAbility.Animation.PlayChannelParticles(gameObject);
            }
        }

        public void ConcludeChannel()
        {
            if (LockedInAbility.HasChannelAnimation)
                Animator.SetBool(LockedInAbility.Animation.BattlerChannelAnimationName, false);
        }

        private void Strike()
        {
            Strike(false);
        }

        private void ShowImpact()
        {
            if (LockedInAbility.HasImpactParticles)
            {
                foreach (BattlerBehaviour target in LockedInAbility.Targets)
                {
                    AttackAnimationBehaviour aab = LockedInAbility.Animation.PlayImpactParticles(target.gameObject);
                    aab.AnimationEndedEvent.RemoveAllListeners();
                    aab.AnimationEndedEvent.AddListener(FinishedTurn);
                }
            }
        }

        private void Strike(bool secondaryHit)
        {
            Strike(secondaryHit, LockedInAbility.Targets[0]);
        }

        private void Strike(bool secondaryHit, BattlerBehaviour target)
        {
            ShowImpact();

            ApplyAbilityEffects(LockedInAbility, secondaryHit);

            int result = DamageFormula.ComputeResult(LockedInAbility.Id,
                                                     BattlerData.Character,
                                                     target.BattlerData.Character);

            target.BattlerData.ChangeHealth(result);

            UiManager.DisplayDamage(target.transform.position, result);
            UiManager.UpdatePlayerGui(target.BattlerData.Character);
        }

        private void ApplyAbilityEffects(Ability ability, bool secondaryHit, BattlerBehaviour target)
        {
            for (int i = 0; i < ability.Effects.Count; i++)
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
                else if (ability.Effects[i] is AutoAttackAfterAbility)
                {
                    // TODO
                }
                else if (ability.Effects[i] is Dispel)
                {
                    Dispel dispel = ability.Effects[i] as Dispel;
                    if (target.BattlerData.Character.ActiveEffects.Any(e => e.Effect == dispel.Value))
                    {
                        dispel.Apply(target.BattlerData.Character);
                        UiManager.RemoveStatus(target.transform.position, dispel.Value);
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
                    if (!target.BattlerData.Character.ActiveEffects.Any(e => e.Effect == inflictStatus.Value))
                    {
                        inflictStatus.Apply(target.BattlerData.Character);
                        UiManager.DisplayStatus(target.transform.position, inflictStatus.Value);
                    }
                }
                else if (ability.Effects[i] is NegativeStatusBonusDamage)
                {
                    // TODO
                }

            }
        }

        private void ApplyAbilityEffects(Ability ability, bool secondaryHit)
        {
            ApplyAbilityEffects(ability, secondaryHit, ability.Targets[0]);
        }

        public void FinishedTurn()
        {
            if(LockedInAbility.HasChannelAnimation)
                Animator.SetBool(LockedInAbility.Animation.BattlerChannelAnimationName, false);

            if (LockedInAbility.HasStrikeAnimation)
                Animator.SetBool(LockedInAbility.Animation.BattlerStrikeAnimationName, false);

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
            if(BattlerData != null)
            {
                if (!IsDead && BattlerData.Character != null && BattlerData.Character.Stats.CurrentHealth <= 0)
                    Die();
            }
        }

        public void Die()
        {
            IsDead = true;
            Animator.Play("Die");
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
