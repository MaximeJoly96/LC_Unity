using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace Testing
{
    public class TestFoundation
    {
        protected List<GameObject> _usedGameObjects;

        [TearDown]
        public void TearDown()
        {
            for (int i = 0; i < _usedGameObjects.Count; i++)
            {
                Object.Destroy(_usedGameObjects[i]);
            }
        }

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            _usedGameObjects = new List<GameObject>();
        }
    }
}
