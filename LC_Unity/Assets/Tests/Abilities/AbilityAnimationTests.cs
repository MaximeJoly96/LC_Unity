using Abilities;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace Testing.Abilities
{
    public class AbilityAnimationTests : TestFoundation
    {
        [Test]
        public void AbilityProjectileCanBeCreatedTest()
        {
            AbilityProjectile defaultProjectile = ComponentCreator.CreateDefaultProjectile();
            _usedGameObjects.Add(defaultProjectile.gameObject);

            AbilityAnimation animation = new AbilityAnimation("channel", "strike", 0, 1, defaultProjectile);
            ProjectileTrajectory trajectory = new ProjectileTrajectory();
            trajectory.AddCheckpoint(Vector3.zero);
            trajectory.AddCheckpoint(Vector3.up);

            animation.CreateProjectile(new Vector3(2.0f, 2.0f), 3.0f, trajectory);
            AbilityProjectile projectile = Object.FindObjectOfType<AbilityProjectile>();

            Assert.IsTrue(Mathf.Abs(3.0f - projectile.Speed) < 0.01f);
            Assert.IsTrue(Vector3.Distance(new Vector3(2.0f, 2.0f), projectile.transform.position) < 0.01f);
            Assert.IsTrue(Vector3.Distance(Vector3.zero, projectile.Trajectory.Checkpoints[0]) < 0.01f);
            Assert.IsTrue(Vector3.Distance(Vector3.up, projectile.Trajectory.Checkpoints[1]) < 0.01f);
        }
    }
}
