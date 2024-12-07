using BattleSystem.Fields;
using NUnit.Framework;
using UnityEngine;

namespace Testing.BattleSystem.Fields
{
    public class BattlefieldTests : TestFoundation
    {
        [Test]
        public void FieldIdCanBeObtained()
        {
            GameObject go = ComponentCreator.CreateEmptyGameObject();
            _usedGameObjects.Add(go);

            Battlefield field = go.AddComponent<Battlefield>();
            field.Id = 4;

            Assert.AreEqual(4, field.Id);
        }
    }
}
