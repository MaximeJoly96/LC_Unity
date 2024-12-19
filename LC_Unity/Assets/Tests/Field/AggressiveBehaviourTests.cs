using Field;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

namespace Testing.Field
{
    public class AggressiveBehaviourTests : TestFoundation
    {
        private AggressiveBehaviour CreateAggressiveBehaviour()
        {
            GameObject go = ComponentCreator.CreateEmptyGameObject();

            return go.AddComponent<AggressiveBehaviour>();
        }

        [UnityTest]
        public IEnumerator RaysCanBeComputed()
        {
            AggressiveBehaviour behaviour = CreateAggressiveBehaviour();
            _usedGameObjects.Add(behaviour.gameObject);

            behaviour.FieldOfView = 130.0f;

            yield return null;

            Assert.AreEqual(27, behaviour.AnglesForRays.Count);

            for(int i = 0; i < 27; i++)
            {
                Assert.IsTrue(Mathf.Abs(5 * i - behaviour.AnglesForRays[i]) < 0.01f);
            }
        }
    }
}
