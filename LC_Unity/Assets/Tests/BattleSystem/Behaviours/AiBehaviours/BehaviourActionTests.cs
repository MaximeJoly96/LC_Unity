using NUnit.Framework;
using System.Collections.Generic;
using BattleSystem.Behaviours.AiBehaviours;

namespace Testing.BattleSystem.Behaviours.AiBehaviours
{
    public class BehaviourActionTests : TestFoundation
    {
        [Test]
        public void AddAbilityTest()
        {
            BehaviourAction action = new BehaviourAction();

            Assert.AreEqual(0, action.Abilities.Count);

            action.AddAbility(4);

            Assert.AreEqual(1, action.Abilities.Count);
            Assert.AreEqual(4, action.Abilities[0]);
        }

        [Test]
        public void AddAbilitiesTest()
        {
            BehaviourAction action = new BehaviourAction();

            Assert.AreEqual(0, action.Abilities.Count);

            List<int> abilities = new List<int>();
            for(int i = 0; i < 5; i++)
            {
                abilities.Add(i);
            }
            action.AddAbilities(abilities);

            Assert.AreEqual(5, action.Abilities.Count);

            for(int i = 0; i < 5; i++)
            {
                Assert.AreEqual(i, action.Abilities[i]);
            }
        }

        [Test]
        public void SetConditionTest()
        {
            BehaviourAction action = new BehaviourAction();

            Assert.IsNull(action.Condition);

            action.SetCondition(new DefaultCondition());

            Assert.NotNull(action.Condition);
        }
    }
}
