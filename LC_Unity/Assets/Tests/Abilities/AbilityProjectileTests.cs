using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic;
using Abilities;
using UnityEngine.TestTools;
using System.Collections;

namespace Testing.Abilities
{
    public class AbilityProjectileTests
    {
        private List<GameObject> _usedGameObjects;

        [TearDown]
        public void TearDown()
        {
            for (int i = 0; i < _usedGameObjects.Count; i++)
            {
                GameObject.Destroy(_usedGameObjects[i]);
            }
        }

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            _usedGameObjects = new List<GameObject>();
        }

        [Test]
        public void ProjectileSpeedCanBeSetByOtherObjectTest()
        {
            AbilityProjectile projectile = CreateDefaultProjectile();
            projectile.SetSpeed(3.5f);

            Assert.IsTrue(Mathf.Abs(3.5f - projectile.Speed) < 0.01f);
        }

        [Test]
        public void ProjectileTrajectoryCanBeSetByOtherObjectTest()
        {
            AbilityProjectile projectile = CreateDefaultProjectile();
            ProjectileTrajectory trajectory = new ProjectileTrajectory();
            trajectory.AddCheckpoint(Vector3.zero);
            trajectory.AddCheckpoint(Vector3.left);

            projectile.SetTrajectory(trajectory);

            Assert.AreEqual(2, projectile.Trajectory.Checkpoints.Count);
            Assert.IsTrue(Vector3.Distance(Vector3.zero, projectile.Trajectory.Checkpoints[0]) < 0.01f);
            Assert.IsTrue(Vector3.Distance(Vector3.left, projectile.Trajectory.Checkpoints[1]) < 0.01f);
        }

        [UnityTest]
        public IEnumerator ProjectileMovesAlongItsTrajectoryTest()
        {
            AbilityProjectile projectile = CreateDefaultProjectile();

            ProjectileTrajectory trajectory = new ProjectileTrajectory();
            trajectory.AddCheckpoint(Vector3.zero);
            trajectory.AddCheckpoint(Vector3.left);

            projectile.SetSpeed(3.0f);
            projectile.SetTrajectory(trajectory);
            projectile.StartMoving();

            yield return new WaitForSeconds(5.0f);

            Assert.IsTrue(Vector3.Distance(projectile.transform.position, Vector3.left) < 0.05f);
        }

        [UnityTest]
        public IEnumerator ProjectileShouldStopMovingWhenToldSoTest()
        {
            AbilityProjectile projectile = CreateDefaultProjectile();

            ProjectileTrajectory trajectory = new ProjectileTrajectory();
            trajectory.AddCheckpoint(Vector3.zero);
            trajectory.AddCheckpoint(new Vector3(10000.0f, 0.0f));

            projectile.SetSpeed(3.0f);
            projectile.SetTrajectory(trajectory);
            projectile.StartMoving();

            yield return new WaitForSeconds(0.5f);

            Vector3 currentPosition = projectile.transform.position;
            projectile.StopMoving();

            yield return new WaitForSeconds(1.0f);

            Assert.IsTrue(Vector3.Distance(currentPosition, projectile.transform.position) < 0.05f);
        }

        private AbilityProjectile CreateDefaultProjectile()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);

            AbilityProjectile projectile = go.AddComponent<AbilityProjectile>();
            return projectile;
        }
    }
}
