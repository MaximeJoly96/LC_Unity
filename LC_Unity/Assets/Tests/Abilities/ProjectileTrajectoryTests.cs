using Abilities;
using NUnit.Framework;
using UnityEngine;

namespace Testing.Abilities
{
    public class ProjectileTrajectoryTests
    {
        [Test]
        public void CheckpointsCanBeAddedToTrajectoryTest()
        {
            ProjectileTrajectory trajectory = new ProjectileTrajectory();

            trajectory.AddCheckpoint(new Vector3(1.0f, 0.0f));
            trajectory.AddCheckpoint(new Vector3(2.0f, -1.0f));
            trajectory.AddCheckpoint(new Vector3(4.5f, 6.7f));

            Assert.AreEqual(3, trajectory.Checkpoints.Count);
            Assert.IsTrue(Vector3.Distance(trajectory.Checkpoints[0], new Vector3(1.0f, 0.0f)) < 0.01f);
            Assert.IsTrue(Vector3.Distance(trajectory.Checkpoints[1], new Vector3(2.0f, -1.0f)) < 0.01f);
            Assert.IsTrue(Vector3.Distance(trajectory.Checkpoints[2], new Vector3(4.5f, 6.7f)) < 0.01f);
        }
    }
}
