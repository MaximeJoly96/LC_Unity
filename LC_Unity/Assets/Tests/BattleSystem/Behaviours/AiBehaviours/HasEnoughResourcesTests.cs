using BattleSystem.Behaviours.AiBehaviours;
using NUnit.Framework;
using UnityEngine;
using Actors;
using Utils;
using Abilities;
using Core.Model;

namespace Testing.BattleSystem.Behaviours.AiBehaviours
{
    public class HasEnoughResourcesTests : TestFoundation
    {
        [Test]
        public void CreateHasEnoughResourcesTest()
        {
            HasEnoughResources hasEnough = new HasEnoughResources(HasEnoughResources.AmountType.FromAbility, 35.0f, global::Effects.Stat.HP);

            Assert.AreEqual(HasEnoughResources.AmountType.FromAbility, hasEnough.Amount);
            Assert.IsTrue(Mathf.Abs(35.0f - hasEnough.Value) < 0.01f);
            Assert.AreEqual(global::Effects.Stat.HP, hasEnough.Resource);
        }

        [Test]
        public void CheckCharacterHasEnoughFlatResourcesTest()
        {
            HasEnoughResources hasEnough = new HasEnoughResources(HasEnoughResources.AmountType.Flat, 10.0f, global::Effects.Stat.MP);
            Character character = CreateDummyCharacter();

            character.Stats.CurrentMana = 20;

            Assert.IsTrue(hasEnough.Check(character));

            character.Stats.CurrentMana = 0;

            Assert.IsFalse(hasEnough.Check(character));
        }

        [Test]
        public void CheckCharacterHasEnoughPercentageResourcesTest()
        {
            HasEnoughResources hasEnough = new HasEnoughResources(HasEnoughResources.AmountType.Percentage, 20.0f, global::Effects.Stat.HP);
            Character character = CreateDummyCharacter();

            character.Stats.CurrentHealth = 100;

            Assert.IsTrue(hasEnough.Check(character));

            character.Stats.CurrentHealth = 10;

            Assert.IsFalse(hasEnough.Check(character));
        }

        [Test]
        public void CheckCharacterHasEnoughResourcesForAbilityTest()
        {
            HasEnoughResources hasEnough = new HasEnoughResources(HasEnoughResources.AmountType.FromAbility, 0, global::Effects.Stat.EP);
            Character character = CreateDummyCharacter();
            Ability ability = new Ability(new ElementIdentifier(0, "test", "testDescription"), 0, 
                                          AbilityUsability.Always, TargetEligibility.Any, AbilityCategory.Skill, 0);
            ability.SetCost(0, 0, 10);

            character.Stats.CurrentEssence = 10;
            
            Assert.IsTrue(hasEnough.Check(character, ability));

            character.Stats.CurrentEssence = 0;

            Assert.IsFalse(hasEnough.Check(character, ability));
        }

        private Character CreateDummyCharacter()
        {
            return new Character(new ElementIdentifier(0, "name", ""),
                                 new QuadraticFunction(10.0f, 10.0f, 10.0f),
                                 new StatScalingFunction(100.0f, 1.0f, 100.0f),
                                 new StatScalingFunction(10.0f, 1.0f, 10.0f),
                                 new StatScalingFunction(10.0f, 1.0f, 10.0f),
                                 new StatScalingFunction(10.0f, 1.0f, 10.0f),
                                 new StatScalingFunction(10.0f, 1.0f, 10.0f),
                                 new StatScalingFunction(10.0f, 1.0f, 10.0f),
                                 new StatScalingFunction(10.0f, 1.0f, 10.0f),
                                 new StatScalingFunction(10.0f, 1.0f, 10.0f),
                                 new StatScalingFunction(10.0f, 1.0f, 10.0f));
        }
    }
}
