using NUnit.Framework;
using BattleSystem;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using System.Collections.Generic;
using BattleSystem.Model;
using Actors;

namespace Testing.BattleSystem
{
    public class BattlersHolderTests : TestFoundation
    {
        [UnityTest]
        public IEnumerator BattlerCanBeInstantiated()
        {
            Character c1 = ComponentCreator.CreateDummyCharacter(1);
            Character c2 = ComponentCreator.CreateDummyCharacter(2);
            Character c3 = ComponentCreator.CreateDummyCharacter(3);

            BattlersHolder holder = CreateBattlersHolder();
            Battler battlerModel = new Battler(c1);

            List<BattlerBehaviour> battlers = new List<BattlerBehaviour>
            {
                ComponentCreator.CreateBattlerBehaviour(),
                ComponentCreator.CreateBattlerBehaviour(),
                ComponentCreator.CreateBattlerBehaviour()
            };

            battlers[0].Feed(new Battler(c1));
            battlers[1].Feed(new Battler(c2));
            battlers[2].Feed(new Battler(c3));

            _usedGameObjects.Add(battlers[0].gameObject);
            _usedGameObjects.Add(battlers[1].gameObject);
            _usedGameObjects.Add(battlers[2].gameObject);

            holder.Feed(battlers);

            BattlerBehaviour inst = holder.InstantiateBattler(battlerModel);
            _usedGameObjects.Add(inst.gameObject);

            yield return null;

            Assert.AreEqual(1, inst.BattlerId);
        }

        private BattlersHolder CreateBattlersHolder()
        {
            GameObject go = ComponentCreator.CreateEmptyGameObject();
            _usedGameObjects.Add(go);
            return go.AddComponent<BattlersHolder>();
        }
    }
}
