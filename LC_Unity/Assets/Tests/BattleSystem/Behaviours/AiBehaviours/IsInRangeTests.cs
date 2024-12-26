using BattleSystem;
using BattleSystem.Behaviours.AiBehaviours;
using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Testing.BattleSystem.Behaviours.AiBehaviours
{
    public class IsInRangeTests : TestFoundation
    {
        [Test]
        public void CreateBasicIsInRangeTest()
        {
            IsInRange inRange = new IsInRange();

            Assert.AreEqual(0, inRange.MinTargetCount);
            Assert.AreEqual(0, inRange.MaxTargetCount);
            Assert.AreEqual(0, inRange.Range);
        }

        [Test]
        public void SetValuesTest()
        {
            IsInRange inRange = new IsInRange(3, 0, 400);

            Assert.AreEqual(3, inRange.MinTargetCount);
            Assert.AreEqual(0, inRange.MaxTargetCount);
            Assert.AreEqual(400, inRange.Range);
        }

        [UnityTest]
        public IEnumerator CheckIsInRangeTest()
        {
            IsInRange inRange = new IsInRange(3, 0, 500);

            GameObject source = new GameObject("Source");
            _usedGameObjects.Add(source);
            source.transform.position = Vector3.zero;

            GameObject battler1 = CreateBattler("Battler1", new Vector3(0.1f, 0.1f));
            GameObject battler2 = CreateBattler("Battler2", new Vector3(10.0f, 0.0f));
            GameObject battler3 = CreateBattler("Battler3", new Vector3(-0.5f, 0.2f));

            yield return null;

            Assert.IsFalse(inRange.Check(source));

            battler2.transform.position = new Vector3(-0.1f, -0.2f);

            yield return null;

            Assert.IsTrue(inRange.Check(source));

            inRange.SetValues(1, 2, 500);

            Assert.IsFalse(inRange.Check(source));

            inRange.SetValues(1, 0, 100000);
            battler2.transform.position = new Vector3(10.0f, 0.0f);

            yield return null;

            Assert.IsTrue(inRange.Check(source));
        }

        private GameObject CreateBattler(string name, Vector3 position)
        {
            GameObject battler = new GameObject(name);
            _usedGameObjects.Add(battler);
            battler.AddComponent<BattlerBehaviour>();
            battler.AddComponent<BoxCollider2D>();

            battler.transform.position = position;

            return battler;
        }
    }
}
