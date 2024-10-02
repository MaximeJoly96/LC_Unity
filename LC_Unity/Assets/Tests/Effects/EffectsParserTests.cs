using NUnit.Framework;
using System.Collections.Generic;
using System.Xml;
using UnityEditor;
using UnityEngine;
using Effects;
using Actors;

namespace Testing.Effects
{
    public class EffectsParserTests
    {
        private readonly string BASE_FILE_PATH = "Assets/Tests/Effects/TestFiles/";

        private XmlNode GetEffectsNode(string fileName)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(AssetDatabase.LoadAssetAtPath<TextAsset>(BASE_FILE_PATH + fileName + ".xml").text);

            return document.SelectSingleNode("Effects");
        }

        [Test]
        public void EffectsCanBeExtractedFromXmlNode()
        {
            List<IEffect> effects = EffectsParser.ParseEffectsFromNode(GetEffectsNode("GlobalParserData"));

            Assert.AreEqual(5, effects.Count);
            Assert.IsTrue(effects[0] is Dispel);
            Assert.IsTrue(effects[1] is RefundsOnKill);
            Assert.IsTrue(effects[2] is Revives);
            Assert.IsTrue(effects[3] is DegressiveRangeDamage);
            Assert.IsTrue(effects[4] is AttackPriorityModifier);
        }

        [Test]
        public void AdditionalStrikeCanBeParsed()
        {
            List<IEffect> effects = EffectsParser.ParseEffectsFromNode(GetEffectsNode("AdditionalStrikeData"));

            Assert.IsTrue(effects[0] is AdditionalStrike);

            AdditionalStrike addStrike = effects[0] as AdditionalStrike;

            Assert.AreEqual(1, addStrike.Amount);
        }

        [Test]
        public void AreaOfEffectAsSecondaryDamageCanBeParsed()
        {
            List<IEffect> effects = EffectsParser.ParseEffectsFromNode(GetEffectsNode("AreaOfEffectAsSecondaryDamageData"));

            Assert.IsTrue(effects[0] is AreaOfEffectAsSecondaryDamage);

            AreaOfEffectAsSecondaryDamage aoe = effects[0] as AreaOfEffectAsSecondaryDamage;

            Assert.AreEqual(100, aoe.BaseDamage);
            Assert.AreEqual(Element.Ice, aoe.Element);
        }

        [Test]
        public void AttackPriorityModifierCanBeParsed()
        {
            List<IEffect> effects = EffectsParser.ParseEffectsFromNode(GetEffectsNode("AttackPriorityModifierData"));

            Assert.IsTrue(effects[0] is AttackPriorityModifier);

            AttackPriorityModifier priority = effects[0] as AttackPriorityModifier;

            Assert.AreEqual(3, priority.Value);
        }

        [Test]
        public void AttacksIgnoreDefenseStatCanBeParsed()
        {
            List<IEffect> effects = EffectsParser.ParseEffectsFromNode(GetEffectsNode("AttacksIgnoreDefenseStatData"));

            Assert.IsTrue(effects[0] is AttacksIgnoreDefenseStat);

            AttacksIgnoreDefenseStat attacks = effects[0] as AttacksIgnoreDefenseStat;

            Assert.AreEqual(Stat.Defense, attacks.Stat);
            Assert.IsTrue(Mathf.Abs(30.0f - attacks.Value) < 0.01f);
        }

        [Test]
        public void AutoAttackAfterAbilityCanBeParsed()
        {
            List<IEffect> effects = EffectsParser.ParseEffectsFromNode(GetEffectsNode("AutoAttackAfterAbilityData"));

            Assert.IsTrue(effects[0] is AutoAttackAfterAbility);
        }

        [Test]
        public void BonusDamageToShieldsCanBeParsed()
        {
            List<IEffect> effects = EffectsParser.ParseEffectsFromNode(GetEffectsNode("BonusDamageToShieldsData"));

            Assert.IsTrue(effects[0] is BonusDamageToShields);

            BonusDamageToShields bonusToShields = effects[0] as BonusDamageToShields;

            Assert.IsTrue(Mathf.Abs(20.5f - bonusToShields.Value) < 0.01f);
        }

        [Test]
        public void BonusElementalDamageCanBeParsed()
        {
            List<IEffect> effects = EffectsParser.ParseEffectsFromNode(GetEffectsNode("BonusElementalDamageData"));

            Assert.IsTrue(effects[0] is BonusElementalDamage);

            BonusElementalDamage bonusDamage = effects[0] as BonusElementalDamage;

            Assert.IsTrue(bonusDamage.Elements.Contains(Element.Fire));
            Assert.IsTrue(Mathf.Abs(40.0f - bonusDamage.Value) < 0.01f);
        }

        [Test]
        public void BonusToAllElementalDamageCanBeParsed()
        {
            List<IEffect> effects = EffectsParser.ParseEffectsFromNode(GetEffectsNode("BonusToAllElementalDamageData"));

            Assert.IsTrue(effects[0] is BonusElementalDamage);

            BonusElementalDamage bonusDamage = effects[0] as BonusElementalDamage;

            Assert.IsTrue(bonusDamage.Elements.Contains(Element.Fire));
            Assert.IsTrue(bonusDamage.Elements.Contains(Element.Thunder));
            Assert.IsTrue(bonusDamage.Elements.Contains(Element.Water));
            Assert.IsTrue(bonusDamage.Elements.Contains(Element.Ice));
            Assert.IsTrue(bonusDamage.Elements.Contains(Element.Earth));
            Assert.IsTrue(bonusDamage.Elements.Contains(Element.Wind));
            Assert.IsTrue(bonusDamage.Elements.Contains(Element.Holy));
            Assert.IsTrue(bonusDamage.Elements.Contains(Element.Darkness));
            Assert.IsTrue(bonusDamage.Elements.Contains(Element.Healing));
            Assert.IsTrue(Mathf.Abs(35.0f - bonusDamage.Value) < 0.01f);
        }

        [Test]
        public void BoundAbilityCanBeParsed()
        {
            List<IEffect> effects = EffectsParser.ParseEffectsFromNode(GetEffectsNode("BoundAbilityData"));

            Assert.IsTrue(effects[0] is BoundAbility);

            BoundAbility boundAbility = effects[0] as BoundAbility;

            Assert.AreEqual(42, boundAbility.AbilityId);
        }

        [Test]
        public void CastPositiveStatusDurationExtensionCanBeParsed()
        {
            List<IEffect> effects = EffectsParser.ParseEffectsFromNode(GetEffectsNode("CastPositiveStatusDurationExtensionData"));

            Assert.IsTrue(effects[0] is CastPositiveStatusDurationExtension);

            CastPositiveStatusDurationExtension extension = effects[0] as CastPositiveStatusDurationExtension;

            Assert.AreEqual(5, extension.Turns);
        }

        [Test]
        public void ConeAttackWithBulletsCanBeParsed()
        {
            List<IEffect> effects = EffectsParser.ParseEffectsFromNode(GetEffectsNode("ConeAttackWithBulletsData"));

            Assert.IsTrue(effects[0] is ConeAttackWithBullets);

            ConeAttackWithBullets cone = effects[0] as ConeAttackWithBullets;

            Assert.IsTrue(Mathf.Abs(45.0f - cone.Angle) < 0.01f);
            Assert.AreEqual(4, cone.Bullets);
            Assert.IsTrue(Mathf.Abs(30.0f - cone.DamageReduction) < 0.01f);
        }

        [Test]
        public void CostReductionCanBeParsed()
        {
            List<IEffect> effects = EffectsParser.ParseEffectsFromNode(GetEffectsNode("CostReductionData"));

            Assert.IsTrue(effects[0] is CostReduction);

            CostReduction reduction = effects[0] as CostReduction;

            Assert.AreEqual(Stat.EP, reduction.Stat);
            Assert.IsTrue(Mathf.Abs(15.5f - reduction.Value) < 0.01f);
        }

        [Test]
        public void CounterattackAfterParryCanBeParsed()
        {

        }

        [Test]
        public void DegressiveRangeDamageCanBeParsed()
        {

        }

        [Test]
        public void DispelCanBeParsed()
        {

        }

        [Test]
        public void DrainFromDamageCanBeParsed()
        {

        }

        [Test]
        public void ElementalAbilitiesCostReductionCanBeParsed()
        {

        }

        [Test]
        public void ElementalAffinityExploitManaRefundCanBeParsed()
        {

        }

        [Test]
        public void ElementalAffinityExploitSelfStatusCanBeParsed()
        {

        }

        [Test]
        public void ElementalAffinityModifierCanBeParsed()
        {

        }

        [Test]
        public void HealingItemsEfficiencyCanBeParsed()
        {

        }

        [Test]
        public void HpThresholdBonusDamageCanBeParsed()
        {

        }

        [Test]
        public void InflictStatusCanBeParsed()
        {

        }

        [Test]
        public void MagicIgnoresDefenseStatCanBeParsed()
        {

        }

        [Test]
        public void MeleeDistanceScalingDamageCanBeParsed()
        {

        }

        [Test]
        public void NegativeStatusBonusDamageCanBeParsed()
        {

        }

        [Test]
        public void PiercingProjectileCanBeParsed()
        {

        }

        [Test]
        public void RefillOnParryCanBeParsed()
        {

        }

        [Test]
        public void RestoresResourceScalingCanBeParsed()
        {

        }

        [Test]
        public void RevivesCanBeParsed()
        {

        }

        [Test]
        public void SelfStatusCanBeParsed()
        {

        }

        [Test]
        public void StatBoostCanBeParsed()
        {

        }

        [Test]
        public void StatusImmunityCanBeParsed()
        {

        }

        [Test]
        public void TargetEffectivenessCanBeParsed()
        {

        }

        [Test]
        public void UndamagedTargetBonusDamageCanBeParsed()
        {

        }
    }
}
