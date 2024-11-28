using NUnit.Framework;
using Field;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

namespace Testing.Field
{
    public class FieldWithFloorsTests : TestFoundation
    {
        [UnityTest]
        public IEnumerator FloorIndexChangesWithMovement()
        {
            FieldWithFloors field = CreateField();

            GameObject floor1 = ComponentCreator.CreateEmptyGameObject();
            GameObject floor2 = ComponentCreator.CreateEmptyGameObject();
            GameObject floor3 = ComponentCreator.CreateEmptyGameObject();

            _usedGameObjects.Add(floor1);
            _usedGameObjects.Add(floor2);
            _usedGameObjects.Add(floor3);

            field.AddFloor(floor1.transform);
            field.AddFloor(floor2.transform);
            field.AddFloor(floor3.transform);

            yield return null;

            Assert.AreEqual(0, field.CurrentFloorIndex);

            field.ChangeFloor(true);

            Assert.AreEqual(1, field.CurrentFloorIndex);

            field.ChangeFloor(false);

            Assert.AreEqual(0, field.CurrentFloorIndex);

            field.ChangeFloor(false);

            Assert.AreEqual(0, field.CurrentFloorIndex);

            field.ChangeFloor(true);
            field.ChangeFloor(true);
            field.ChangeFloor(true);
            field.ChangeFloor(true);
            field.ChangeFloor(true);

            Assert.AreEqual(2, field.CurrentFloorIndex);
        }

        private FieldWithFloors CreateField()
        {
            GameObject go = ComponentCreator.CreateEmptyGameObject();
            _usedGameObjects.Add(go);

            return go.AddComponent<FieldWithFloors>();
        }
    }
}
