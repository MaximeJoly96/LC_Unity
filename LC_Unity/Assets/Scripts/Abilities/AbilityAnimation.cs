using BattleSystem;
using BattleSystem.Behaviours;
using UnityEngine;

namespace Abilities
{
    public class AbilityAnimation
    {
        public string BattlerChannelAnimationName { get; private set; }
        public int BattlerChannelAnimationParticlesId { get; private set; }
        public string BattlerStrikeAnimationName { get; private set; }
        public int ImpactAnimationParticlesId { get; private set; }
        public AbilityProjectile Projectile { get; private set; }

        public AbilityAnimation(string channelAnimationName, string strikeAnimationName,
                                int channelAnimationParticlesId, int impactAnimationParticlesId,
                                AbilityProjectile projectile)
        {
            BattlerChannelAnimationName = channelAnimationName;
            BattlerStrikeAnimationName = strikeAnimationName;
            BattlerChannelAnimationParticlesId = channelAnimationParticlesId;
            ImpactAnimationParticlesId = impactAnimationParticlesId;
            Projectile = projectile;
        }

        public AbilityAnimation(string channelAnimationName, string strikeAnimationName,
                                int channelAnimationParticlesId, int impactAnimationParticlesId,
                                int projectileId) : this(channelAnimationName, strikeAnimationName,
                                                         channelAnimationParticlesId, impactAnimationParticlesId,
                                                         GetProjectileFromId(projectileId))
        { 
        }

        public void PlayChannelAnimation(GameObject battler)
        {
            Animator animator = battler.GetComponent<Animator>();
            if (animator)
                animator.Play(BattlerChannelAnimationName);
        }

        public void PlayStrikeAnimation(GameObject battler)
        {
            Animator animator = battler.GetComponent<Animator>();
            if (animator)
                animator.Play(BattlerStrikeAnimationName);
        }

        public AttackAnimationBehaviour PlayChannelParticles(GameObject battler)
        {
            AttackAnimationsWrapper wrapper = Object.FindObjectOfType<AttackAnimationsWrapper>();
            if(wrapper)
            {
                GameObject animation = Object.Instantiate(wrapper.GetAttackAnimation(BattlerChannelAnimationParticlesId), 
                                                          battler.transform.position, 
                                                          Quaternion.identity);

                return animation.GetComponent<AttackAnimationBehaviour>();
            }

            return null;
        }

        public AttackAnimationBehaviour PlayImpactParticles(GameObject battler)
        {
            AttackAnimationsWrapper wrapper = Object.FindObjectOfType<AttackAnimationsWrapper>();
            if (wrapper)
            {
                GameObject animation = Object.Instantiate(wrapper.GetAttackAnimation(ImpactAnimationParticlesId),
                                                          battler.transform.position,
                                                          Quaternion.identity);

                return animation.GetComponent<AttackAnimationBehaviour>();
            }

            return null;
        }

        public AbilityProjectile CreateProjectile(Vector3 origin, float speed, ProjectileTrajectory trajectory)
        {
            if(Projectile)
            {
                AbilityProjectile instProjectile = Object.Instantiate(Projectile, origin, Quaternion.identity);
                instProjectile.SetSpeed(speed);
                instProjectile.SetTrajectory(trajectory);

                return instProjectile;
            }

            return null;
        }

        private static AbilityProjectile GetProjectileFromId(int id)
        {
            ProjectilesWrapper wrapper = Object.FindObjectOfType<ProjectilesWrapper>();

            return wrapper ? wrapper.GetProjectileById(id) : null;
        }
    }
}
