using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic;
using Abilities;
using UnityEngine.TestTools;
using System.Collections;

namespace Testing.Abilities
{
    public class AbilityProjectileTests : TestFoundation
    {
        [Test]
        public void ProjectileSpeedCanBeSetByOtherObjectTest()
        {
            AbilityProjectile projectile = ComponentCreator.CreateDefaultProjectile();
            projectile.SetSpeed(3.5f);
            _usedGameObjects.Add(projectile.gameObject);

            Assert.IsTrue(Mathf.Abs(3.5f - projectile.Speed) < 0.01f);
        }

        [Test]
        public void ProjectileTrajectoryCanBeSetByOtherObjectTest()
        {
            AbilityProjectile projectile = ComponentCreator.CreateDefaultProjectile();
            _usedGameObjects.Add(projectile.gameObject);
            ProjectileTrajectory trajectory = new ProjectileTrajectory();
            trajectory.AddCheckpoint(Vector3.zero);
            trajectory.AddCheckpoint(Vector3.left);

            projectile.SetTrajectory(trajectory);

            Assert.AreEqual(2, projectile.Trajectory.Checkpoints.Count);
            Assert.IsTrue(Vector3.Distance(Vector3.zero, projectile.Trajectory.Checkpoints[0]) < 0.01f);
            Assert.IsTrue(Vector3.Distance(Vector3.left, projectile.Trajectory.Checkpoints[1]) < 0.01f);
        }
    }
}
