using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Inventory;
using Effects;
using Actors;

namespace Testing.Inventory
{
    public class WeaponsParserTests
    {
        [Test]
        public void WeaponsCanBeParsed()
        {
            List<Weapon> weapons = WeaponsParser.ParseWeapons(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Inventory/TestData/TestWeapons.xml"));

            Assert.AreEqual(3, weapons.Count);

            Assert.AreEqual(1216, weapons[0].Id);
            Assert.AreEqual("sickle", weapons[0].Name);
            Assert.AreEqual("sickleDescription", weapons[0].Description);
            Assert.AreEqual(216, weapons[0].Icon);
            Assert.AreEqual(10, weapons[0].Price);
            Assert.AreEqual(WeaponType.Scythe, weapons[0].Type);
            Assert.AreEqual(2, weapons[0].Rank);
            Assert.AreEqual(3, weapons[0].EnchantmentSlots);
            Assert.AreEqual(200, weapons[0].Range);
            Assert.AreEqual(20, weapons[0].Stats.Health);
            Assert.AreEqual(20, weapons[0].Stats.Mana);
            Assert.AreEqual(5, weapons[0].Stats.Strength);
            Assert.AreEqual(10, weapons[0].Stats.Magic);
            Assert.AreEqual("", weapons[0].Animation.BattlerChannelAnimationName);
            Assert.AreEqual(-1, weapons[0].Animation.BattlerChannelAnimationParticlesId);
            Assert.AreEqual("ScytheStrike", weapons[0].Animation.BattlerStrikeAnimationName);
            Assert.AreEqual(10, weapons[0].Animation.ImpactAnimationParticlesId);

            Assert.AreEqual(1071, weapons[1].Id);
            Assert.AreEqual("steelClaws", weapons[1].Name);
            Assert.AreEqual("steelClawsDescription", weapons[1].Description);
            Assert.AreEqual(71, weapons[1].Icon);
            Assert.AreEqual(0, weapons[1].Price);
            Assert.AreEqual(WeaponType.Claws, weapons[1].Type);
            Assert.AreEqual(3, weapons[1].Rank);
            Assert.AreEqual(3, weapons[1].EnchantmentSlots);
            Assert.AreEqual(100, weapons[1].Range);
            Assert.AreEqual(7, weapons[1].Stats.Strength);
            Assert.AreEqual(6, weapons[1].Stats.Agility);
            Assert.AreEqual("", weapons[1].Animation.BattlerChannelAnimationName);
            Assert.AreEqual(-1, weapons[1].Animation.BattlerChannelAnimationParticlesId);
            Assert.AreEqual("ClawStrike", weapons[1].Animation.BattlerStrikeAnimationName);
            Assert.AreEqual(4, weapons[1].Animation.ImpactAnimationParticlesId);
            Assert.AreEqual(1, weapons[1].Effects.Count);
            Assert.IsTrue(weapons[1].Effects[0] is AdditionalStrike);

            AdditionalStrike strike = weapons[1].Effects[0] as AdditionalStrike;

            Assert.AreEqual(1, strike.Amount);

            Assert.AreEqual(1205, weapons[2].Id);
            Assert.AreEqual("sharur", weapons[2].Name);
            Assert.AreEqual("sharurDescription", weapons[2].Description);
            Assert.AreEqual(205, weapons[2].Icon);
            Assert.AreEqual(10, weapons[2].Price);
            Assert.AreEqual(WeaponType.Mace, weapons[2].Type);
            Assert.AreEqual(12, weapons[2].Rank);
            Assert.AreEqual(3, weapons[2].EnchantmentSlots);
            Assert.AreEqual(100, weapons[2].Range);
            Assert.AreEqual(130, weapons[2].Stats.Mana);
            Assert.AreEqual(35, weapons[2].Stats.Strength);
            Assert.AreEqual(72, weapons[2].Stats.Magic);
            Assert.AreEqual("", weapons[2].Animation.BattlerChannelAnimationName);
            Assert.AreEqual(-1, weapons[2].Animation.BattlerChannelAnimationParticlesId);
            Assert.AreEqual("MaceStrike", weapons[2].Animation.BattlerStrikeAnimationName);
            Assert.AreEqual(8, weapons[2].Animation.ImpactAnimationParticlesId);

            Assert.AreEqual(2, weapons[2].Recipe.Components.Count);
            Assert.AreEqual(1201, weapons[2].Recipe.Components[0].ItemId);
            Assert.AreEqual(1, weapons[2].Recipe.Components[0].Quantity);
            Assert.AreEqual(1203, weapons[2].Recipe.Components[1].ItemId);
            Assert.AreEqual(1, weapons[2].Recipe.Components[0].Quantity);

            Assert.AreEqual(4, weapons[2].Effects.Count);
            Assert.IsTrue(weapons[2].Effects[0] is BonusElementalDamage);
            Assert.IsTrue(weapons[2].Effects[1] is CostReduction);
            Assert.IsTrue(weapons[2].Effects[2] is InflictStatus);
            Assert.IsTrue(weapons[2].Effects[3] is InflictStatus);

            BonusElementalDamage bonus = weapons[2].Effects[0] as BonusElementalDamage;

            Assert.AreEqual(1, bonus.Elements.Count);
            Assert.IsTrue(bonus.Elements.Contains(Element.Healing));
            Assert.IsTrue(Mathf.Abs(80.0f - bonus.Value) < 0.01f);

            CostReduction reduction = weapons[2].Effects[1] as CostReduction;

            Assert.AreEqual(Stat.MP, reduction.Stat);
            Assert.IsTrue(Mathf.Abs(30.0f - reduction.Value) < 0.01f);

            InflictStatus status = weapons[2].Effects[2] as InflictStatus;

            Assert.AreEqual(EffectType.BreakIII, status.Value);

            status = weapons[2].Effects[3] as InflictStatus;

            Assert.AreEqual(EffectType.MagicBreakIII, status.Value);
        }
    }
}
