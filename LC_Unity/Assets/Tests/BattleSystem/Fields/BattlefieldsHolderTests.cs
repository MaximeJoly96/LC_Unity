using UnityEngine;
using NUnit.Framework;
using BattleSystem.Fields;

namespace Testing.BattleSystem.Fields
{
    public class BattlefieldsHolderTests : TestFoundation
    {
        [Test]
        public void FieldCanBeRetrievedById()
        {
            Battlefield[] fields = new Battlefield[]
            {
                ComponentCreator.CreateBattlefield(3),
                ComponentCreator.CreateBattlefield(4),
                ComponentCreator.CreateBattlefield(5)
            };

            foreach(Battlefield bf in fields)
            {
                _usedGameObjects.Add(bf.gameObject);
            }

            GameObject holderGo = ComponentCreator.CreateEmptyGameObject();
            _usedGameObjects.Add(holderGo);
            BattlefieldsHolder holder = holderGo.AddComponent<BattlefieldsHolder>();

            holder.FeedFields(fields);

            Assert.AreEqual(fields[0].GetHashCode(), holder.GetField(3).GetHashCode());
            Assert.AreEqual(fields[1].GetHashCode(), holder.GetField(4).GetHashCode());
            Assert.AreEqual(fields[2].GetHashCode(), holder.GetField(5).GetHashCode());
        }
    }
}
