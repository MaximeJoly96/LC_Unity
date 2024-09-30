using BattleSystem.Behaviours.AiBehaviours;
using NUnit.Framework;
using UnityEngine;
using Actors;
using Utils;
using Abilities;

namespace Testing.BattleSystem.Behaviours.AiBehaviours
{
    public class HasEnoughResourcesTests
    {
        [Test]
        public void CreateHasEnoughResourcesTest()
        {
            HasEnoughResources hasEnough = new HasEnoughResources(HasEnoughResources.AmountType.FromAbility, 35.0f, Effects.Stat.HP);

            Assert.AreEqual(HasEnoughResources.AmountType.FromAbility, hasEnough.Amount);
            Assert.IsTrue(Mathf.Abs(35.0f - hasEnough.Value) < 0.01f);
            Assert.AreEqual(Effects.Stat.HP, hasEnough.Resource);
        }

        [Test]
        public void CheckCharacterHasEnoughFlatResourcesTest()
        {
            HasEnoughResources hasEnough = new HasEnoughResources(HasEnoughResources.AmountType.Flat, 10.0f, Effects.Stat.MP);
            Character character = CreateDummyCharacter();

            character.CurrentMana = 20;

            Assert.IsTrue(hasEnough.Check(character));

            character.CurrentMana = 0;

            Assert.IsFalse(hasEnough.Check(character));
        }

        [Test]
        public void CheckCharacterHasEnoughPercentageResourcesTest()
        {
            HasEnoughResources hasEnough = new HasEnoughResources(HasEnoughResources.AmountType.Percentage, 20.0f, Effects.Stat.HP);
            Character character = CreateDummyCharacter();

            character.CurrentHealth = 100;

            Assert.IsTrue(hasEnough.Check(character));

            character.CurrentHealth = 10;

            Assert.IsFalse(hasEnough.Check(character));
        }

        [Test]
        public void CheckCharacterHasEnoughResourcesForAbilityTest()
        {
            HasEnoughResources hasEnough = new HasEnoughResources(HasEnoughResources.AmountType.FromAbility, 0, Effects.Stat.EP);
            Character character = CreateDummyCharacter();
            Ability ability = new Ability(0, "test", "testDescription", new AbilityCost(5, 5, 5), 
                                          AbilityUsability.Always, 0, TargetEligibility.Any, AbilityCategory.Skill);

            character.CurrentEssence = 10;
            
            Assert.IsTrue(hasEnough.Check(character, ability));

            character.CurrentEssence = 0;

            Assert.IsFalse(hasEnough.Check(character, ability));
        }

        private Character CreateDummyCharacter()
        {
            return new Character(0, "name",
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
