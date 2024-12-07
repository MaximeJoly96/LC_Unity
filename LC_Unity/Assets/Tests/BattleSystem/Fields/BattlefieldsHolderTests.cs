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
                CreateBattlefield(3),
                CreateBattlefield(4),
                CreateBattlefield(5)
            };

            GameObject holderGo = ComponentCreator.CreateEmptyGameObject();
            _usedGameObjects.Add(holderGo);
            BattlefieldsHolder holder = holderGo.AddComponent<BattlefieldsHolder>();

            holder.FeedFields(fields);

            Assert.AreEqual(fields[0].GetHashCode(), holder.GetField(3).GetHashCode());
            Assert.AreEqual(fields[1].GetHashCode(), holder.GetField(4).GetHashCode());
            Assert.AreEqual(fields[2].GetHashCode(), holder.GetField(5).GetHashCode());
        }

        private Battlefield CreateBattlefield(int id)
        {
            GameObject go = ComponentCreator.CreateEmptyGameObject();
            _usedGameObjects.Add(go);

            Battlefield bf = go.AddComponent<Battlefield>();
            bf.Id = id;

            return bf;
        }
    }
}
