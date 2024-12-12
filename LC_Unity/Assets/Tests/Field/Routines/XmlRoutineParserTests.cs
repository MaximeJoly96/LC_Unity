using NUnit.Framework;
using Field.Routines;
using UnityEditor;
using UnityEngine;
using Engine.Movement.Moves;
using Movement;

namespace Testing.Field.Routines
{
    public class XmlRoutineParserTests : TestFoundation
    {
        [Test]
        public void RoutineCanBeParsed()
        {
            string path = "Assets/Tests/Field/Routines/TestData/TestRoutine.xml";
            AgentRoutine routine = XmlRoutineParser.ParseRoutine(AssetDatabase.LoadAssetAtPath<TextAsset>(path));

            Assert.AreEqual(8, routine.Count);
            Assert.IsTrue(routine[0] is Through);
            Assert.IsTrue((routine[0] as Through).On);

            Assert.IsTrue(routine[1] is MoveRelative);
            Assert.IsTrue(Mathf.Abs((routine[1] as MoveRelative).DeltaX + 0.8f) < 0.01f);
            Assert.IsTrue(Mathf.Abs((routine[1] as MoveRelative).DeltaY - 0.0f) < 0.01f);

            Assert.IsTrue(routine[2] is Turn);
            Assert.AreEqual(Direction.Down, (routine[2] as Turn).Direction);

            Assert.IsTrue(routine[3] is Wait);
            Assert.IsTrue(Mathf.Abs((routine[3] as Wait).Duration - 5.0f) < 0.01f);

            Assert.IsTrue(routine[4] is MoveRelative);
            Assert.IsTrue(Mathf.Abs((routine[4] as MoveRelative).DeltaX - 1.6f) < 0.01f);
            Assert.IsTrue(Mathf.Abs((routine[4] as MoveRelative).DeltaY - 0.0f) < 0.01f);

            Assert.IsTrue(routine[5] is Turn);
            Assert.AreEqual(Direction.Down, (routine[5] as Turn).Direction);

            Assert.IsTrue(routine[6] is Wait);
            Assert.IsTrue(Mathf.Abs((routine[6] as Wait).Duration - 5.0f) < 0.01f);

            Assert.IsTrue(routine[7] is MoveRelative);
            Assert.IsTrue(Mathf.Abs((routine[7] as MoveRelative).DeltaX + 0.8f) < 0.01f);
            Assert.IsTrue(Mathf.Abs((routine[7] as MoveRelative).DeltaY - 0.0f) < 0.01f);
        }
    }
}
