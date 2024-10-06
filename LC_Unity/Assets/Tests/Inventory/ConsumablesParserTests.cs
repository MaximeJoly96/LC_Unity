using NUnit.Framework;
using Inventory;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Effects;
using Actors;

namespace Testing.Inventory
{
    public class ConsumablesParserTests
    {
        [Test]
        public void ConsumablesCanBeParsed()
        {
            List<Consumable> consumables = ConsumablesParser.ParseConsumables(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Inventory/TestData/TestConsumables.xml"));

            Assert.AreEqual(3, consumables.Count);

            Assert.AreEqual(0, consumables[0].Id);
            Assert.AreEqual("potion", consumables[0].Name);
            Assert.AreEqual("potionDescription", consumables[0].Description);
            Assert.AreEqual(0, consumables[0].Icon);
            Assert.AreEqual(10, consumables[0].Price);
            Assert.AreEqual(ItemUsability.Always, consumables[0].Usability);
            Assert.AreEqual(0, consumables[0].Priority);
            Assert.AreEqual("", consumables[0].Animation.BattlerChannelAnimationName);
            Assert.AreEqual(-1, consumables[0].Animation.BattlerChannelAnimationParticlesId);
            Assert.AreEqual("", consumables[0].Animation.BattlerStrikeAnimationName);
            Assert.AreEqual(10, consumables[0].Animation.ImpactAnimationParticlesId);
            Assert.AreEqual(1, consumables[0].Effects.Count);
            Assert.IsTrue(consumables[0].Effects[0] is RestoresResourceScaling);

            RestoresResourceScaling restore = consumables[0].Effects[0] as RestoresResourceScaling;

            Assert.AreEqual(Stat.HP, restore.Stat);
            Assert.IsTrue(Mathf.Abs(40.0f - restore.Value) < 0.01f);

            Assert.AreEqual(4, consumables[1].Id);
            Assert.AreEqual("phoenixAshes", consumables[1].Name);
            Assert.AreEqual("phoenixAshesDescription", consumables[1].Description);
            Assert.AreEqual(4, consumables[1].Icon);
            Assert.AreEqual(500, consumables[1].Price);
            Assert.AreEqual(ItemUsability.Always, consumables[1].Usability);
            Assert.AreEqual(1, consumables[1].Priority);
            Assert.AreEqual("throw", consumables[1].Animation.BattlerChannelAnimationName);
            Assert.AreEqual(3, consumables[1].Animation.BattlerChannelAnimationParticlesId);
            Assert.AreEqual("", consumables[1].Animation.BattlerStrikeAnimationName);
            Assert.AreEqual(4, consumables[1].Animation.ImpactAnimationParticlesId);
            Assert.AreEqual(2, consumables[1].Effects.Count);
            Assert.IsTrue(consumables[1].Effects[0] is Revives);
            Assert.IsTrue(consumables[1].Effects[1] is RestoresResourceScaling);

            restore = consumables[1].Effects[1] as RestoresResourceScaling;

            Assert.AreEqual(Stat.HP, restore.Stat);
            Assert.IsTrue(Mathf.Abs(40.0f - restore.Value) < 0.01f);

            Assert.AreEqual(9, consumables[2].Id);
            Assert.AreEqual("bandage", consumables[2].Name);
            Assert.AreEqual("bandageDescription", consumables[2].Description);
            Assert.AreEqual(7, consumables[2].Icon);
            Assert.AreEqual(500, consumables[2].Price);
            Assert.AreEqual(ItemUsability.Always, consumables[2].Usability);
            Assert.AreEqual(1, consumables[2].Priority);
            Assert.AreEqual("channel", consumables[2].Animation.BattlerChannelAnimationName);
            Assert.AreEqual(6, consumables[2].Animation.BattlerChannelAnimationParticlesId);
            Assert.AreEqual("strike", consumables[2].Animation.BattlerStrikeAnimationName);
            Assert.AreEqual(50, consumables[2].Animation.ImpactAnimationParticlesId);
            Assert.AreEqual(4, consumables[2].Effects.Count);
            Assert.IsTrue(consumables[2].Effects[0] is Dispel);
            Assert.IsTrue(consumables[2].Effects[1] is Dispel);
            Assert.IsTrue(consumables[2].Effects[2] is Dispel);
            Assert.IsTrue(consumables[2].Effects[3] is Dispel);

            Dispel dispel = consumables[2].Effects[0] as Dispel;

            Assert.AreEqual(EffectType.BleedI, dispel.Value);

            dispel = consumables[2].Effects[1] as Dispel;

            Assert.AreEqual(EffectType.BleedII, dispel.Value);

            dispel = consumables[2].Effects[2] as Dispel;

            Assert.AreEqual(EffectType.BleedIII, dispel.Value);

            dispel = consumables[2].Effects[3] as Dispel;

            Assert.AreEqual(EffectType.HemoI, dispel.Value);
        }
    }
}
