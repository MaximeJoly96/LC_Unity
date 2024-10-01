using UnityEngine;
using System.Collections.Generic;

namespace Abilities
{
    public class ProjectileTrajectory
    {
        private List<Vector3> _checkpoints;

        public List<Vector3> Checkpoints { get { return _checkpoints; } }

        public ProjectileTrajectory()
        {
            _checkpoints = new List<Vector3>();
        }

        public void AddCheckpoint(Vector3 checkpoint)
        {
            _checkpoints.Add(checkpoint);
        }
    }
}
