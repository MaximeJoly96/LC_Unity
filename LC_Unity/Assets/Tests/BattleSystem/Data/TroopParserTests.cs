using NUnit.Framework;
using BattleSystem.Data;
using BattleSystem.Model;
using UnityEngine;
using UnityEditor;

namespace Testing.BattleSystem.Data
{
    public class TroopParserTests : TestFoundation
    {
        [Test]
        public void TroopCanBeParsed()
        {
            TroopParser parser = new TroopParser();

            Troop troop = parser.ParseTroop(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/BattleSystem/Data/TestData/TestTroops.xml"), 0);

            Assert.AreEqual(0, troop.Id);
            Assert.AreEqual(1, troop.Members.Count);
            Assert.AreEqual(0, troop.Members[0].Id);
            Assert.IsTrue(Mathf.Abs(troop.Members[0].X + 2.0f) < 0.01f);
            Assert.IsTrue(Mathf.Abs(troop.Members[0].Y - 0.0f) < 0.01f);
            Assert.AreEqual(3, troop.PlayerSpots.Count);
            Assert.AreEqual(0, troop.PlayerSpots[0].Id);
            Assert.IsTrue(Mathf.Abs(troop.PlayerSpots[0].X - 4.0f) < 0.01f);
            Assert.IsTrue(Mathf.Abs(troop.PlayerSpots[0].Y - 0.0f) < 0.01f);
            Assert.AreEqual(1, troop.PlayerSpots[1].Id);
            Assert.IsTrue(Mathf.Abs(troop.PlayerSpots[1].X - 2.0f) < 0.01f);
            Assert.IsTrue(Mathf.Abs(troop.PlayerSpots[1].Y - 1.0f) < 0.01f);
            Assert.AreEqual(2, troop.PlayerSpots[2].Id);
            Assert.IsTrue(Mathf.Abs(troop.PlayerSpots[2].X - 2.0f) < 0.01f);
            Assert.IsTrue(Mathf.Abs(troop.PlayerSpots[2].Y + 1.0f) < 0.01f);
            Assert.AreEqual(0, troop.BattlefieldId);

            troop = parser.ParseTroop(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/BattleSystem/Data/TestData/TestTroops.xml"), 1);

            Assert.AreEqual(1, troop.Id);
            Assert.AreEqual(1, troop.Members.Count);
            Assert.AreEqual(0, troop.Members[0].Id);
            Assert.IsTrue(Mathf.Abs(troop.Members[0].X + 4.0f) < 0.01f);
            Assert.IsTrue(Mathf.Abs(troop.Members[0].Y - 3.0f) < 0.01f);
            Assert.AreEqual(2, troop.PlayerSpots.Count);
            Assert.AreEqual(0, troop.PlayerSpots[0].Id);
            Assert.IsTrue(Mathf.Abs(troop.PlayerSpots[0].X - 4.0f) < 0.01f);
            Assert.IsTrue(Mathf.Abs(troop.PlayerSpots[0].Y - 6.0f) < 0.01f);
            Assert.AreEqual(1, troop.PlayerSpots[1].Id);
            Assert.IsTrue(Mathf.Abs(troop.PlayerSpots[1].X - 5.0f) < 0.01f);
            Assert.IsTrue(Mathf.Abs(troop.PlayerSpots[1].Y - 4.0f) < 0.01f);
            Assert.AreEqual(4, troop.BattlefieldId);
        }
    }
}
