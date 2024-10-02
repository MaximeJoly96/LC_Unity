using NUnit.Framework;
using System.Collections.Generic;
using System.Xml;
using UnityEditor;
using UnityEngine;
using Effects;
using Actors;
using Actions;

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
            Assert.IsTrue(Mathf.Abs(30.5f - attacks.Value) < 0.01f);
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
            Assert.IsTrue(Mathf.Abs(43.6f - bonusDamage.Value) < 0.01f);
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
            Assert.IsTrue(Mathf.Abs(35.9f - bonusDamage.Value) < 0.01f);
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

            Assert.IsTrue(Mathf.Abs(45.6f - cone.Angle) < 0.01f);
            Assert.AreEqual(4, cone.Bullets);
            Assert.IsTrue(Mathf.Abs(30.9f - cone.DamageReduction) < 0.01f);
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
            List<IEffect> effects = EffectsParser.ParseEffectsFromNode(GetEffectsNode("CounterattackAfterParryData"));

            Assert.IsTrue(effects[0] is CounterattackAfterParry);
        }

        [Test]
        public void DegressiveRangeDamageCanBeParsed()
        {
            List<IEffect> effects = EffectsParser.ParseEffectsFromNode(GetEffectsNode("DegressiveRangeDamageData"));

            Assert.IsTrue(effects[0] is DegressiveRangeDamage);

            DegressiveRangeDamage damage = effects[0] as DegressiveRangeDamage;

            Assert.IsTrue(Mathf.Abs(36.3f - damage.MinDamage) < 0.01f);
        }

        [Test]
        public void DispelCanBeParsed()
        {
            List<IEffect> effects = EffectsParser.ParseEffectsFromNode(GetEffectsNode("DispelData"));

            Assert.IsTrue(effects[0] is Dispel);

            Dispel dispel = effects[0] as Dispel;

            Assert.AreEqual(EffectType.Blind, dispel.Value);
        }

        [Test]
        public void DrainFromDamageCanBeParsed()
        {
            List<IEffect> effects = EffectsParser.ParseEffectsFromNode(GetEffectsNode("DrainFromDamageData"));

            Assert.IsTrue(effects[0] is DrainFromDamage);

            DrainFromDamage drain = effects[0] as DrainFromDamage;

            Assert.AreEqual(Stat.HP, drain.Stat);
            Assert.IsTrue(Mathf.Abs(155.6f - drain.Value) < 0.01f);
        }

        [Test]
        public void ElementalAbilitiesCostReductionCanBeParsed()
        {
            List<IEffect> effects = EffectsParser.ParseEffectsFromNode(GetEffectsNode("ElementalAbilitiesCostReductionData"));

            Assert.IsTrue(effects[0] is ElementalAbilitiesCostReduction);

            ElementalAbilitiesCostReduction reduction = effects[0] as ElementalAbilitiesCostReduction;

            Assert.IsTrue(reduction.Elements.Contains(Element.Wind));
            Assert.IsTrue(Mathf.Abs(15.2f - reduction.Value) < 0.01f);
        }

        [Test]
        public void AllElementalAbilitiesCostReductionCanBeParsed()
        {
            List<IEffect> effects = EffectsParser.ParseEffectsFromNode(GetEffectsNode("AllElementalAbilitiesCostReductionData"));

            Assert.IsTrue(effects[0] is ElementalAbilitiesCostReduction);

            ElementalAbilitiesCostReduction reduction = effects[0] as ElementalAbilitiesCostReduction;

            Assert.IsTrue(reduction.Elements.Contains(Element.Fire));
            Assert.IsTrue(reduction.Elements.Contains(Element.Thunder));
            Assert.IsTrue(reduction.Elements.Contains(Element.Water));
            Assert.IsTrue(reduction.Elements.Contains(Element.Ice));
            Assert.IsTrue(reduction.Elements.Contains(Element.Earth));
            Assert.IsTrue(reduction.Elements.Contains(Element.Wind));
            Assert.IsTrue(reduction.Elements.Contains(Element.Holy));
            Assert.IsTrue(reduction.Elements.Contains(Element.Darkness));
            Assert.IsTrue(reduction.Elements.Contains(Element.Healing));
            Assert.IsTrue(Mathf.Abs(40.9f - reduction.Value) < 0.01f);
        }

        [Test]
        public void ElementalAffinityExploitManaRefundCanBeParsed()
        {
            List<IEffect> effects = EffectsParser.ParseEffectsFromNode(GetEffectsNode("ElementalAffinityExploitManaRefundData"));

            Assert.IsTrue(effects[0] is ElementalAffinityExploitManaRefund);

            ElementalAffinityExploitManaRefund refund = effects[0] as ElementalAffinityExploitManaRefund;

            Assert.IsTrue(refund.Elements.Contains(Element.Darkness));
            Assert.IsTrue(Mathf.Abs(18.4f - refund.Value) < 0.01f);
        }

        [Test]
        public void AllElementalAffinitiesExploitManaRefundCanBeParsed()
        {
            List<IEffect> effects = EffectsParser.ParseEffectsFromNode(GetEffectsNode("AllElementalAffinityExploitManaRefundData"));

            Assert.IsTrue(effects[0] is ElementalAffinityExploitManaRefund);

            ElementalAffinityExploitManaRefund refund = effects[0] as ElementalAffinityExploitManaRefund;

            Assert.IsTrue(refund.Elements.Contains(Element.Fire));
            Assert.IsTrue(refund.Elements.Contains(Element.Thunder));
            Assert.IsTrue(refund.Elements.Contains(Element.Water));
            Assert.IsTrue(refund.Elements.Contains(Element.Ice));
            Assert.IsTrue(refund.Elements.Contains(Element.Earth));
            Assert.IsTrue(refund.Elements.Contains(Element.Wind));
            Assert.IsTrue(refund.Elements.Contains(Element.Holy));
            Assert.IsTrue(refund.Elements.Contains(Element.Darkness));
            Assert.IsTrue(refund.Elements.Contains(Element.Healing));
            Assert.IsTrue(Mathf.Abs(36.5f - refund.Value) < 0.01f);
        }

        [Test]
        public void ElementalAffinityExploitSelfStatusCanBeParsed()
        {
            List<IEffect> effects = EffectsParser.ParseEffectsFromNode(GetEffectsNode("ElementalAffinityExploitSelfStatusData"));

            Assert.IsTrue(effects[0] is ElementalAffinityExploitSelfStatus);

            ElementalAffinityExploitSelfStatus status = effects[0] as ElementalAffinityExploitSelfStatus;

            Assert.AreEqual(EffectType.Shell, status.Value);
            Assert.IsTrue(status.Elements.Contains(Element.Holy));
        }

        [Test]
        public void AllElementalAffinitiesExploitSelfStatusCanBeParsed()
        {
            List<IEffect> effects = EffectsParser.ParseEffectsFromNode(GetEffectsNode("AllElementalAffinityExploitSelfStatusData"));

            Assert.IsTrue(effects[0] is ElementalAffinityExploitSelfStatus);

            ElementalAffinityExploitSelfStatus status = effects[0] as ElementalAffinityExploitSelfStatus;

            Assert.AreEqual(EffectType.Protect, status.Value);
            Assert.IsTrue(status.Elements.Contains(Element.Fire));
            Assert.IsTrue(status.Elements.Contains(Element.Thunder));
            Assert.IsTrue(status.Elements.Contains(Element.Water));
            Assert.IsTrue(status.Elements.Contains(Element.Ice));
            Assert.IsTrue(status.Elements.Contains(Element.Earth));
            Assert.IsTrue(status.Elements.Contains(Element.Wind));
            Assert.IsTrue(status.Elements.Contains(Element.Holy));
            Assert.IsTrue(status.Elements.Contains(Element.Darkness));
            Assert.IsTrue(status.Elements.Contains(Element.Healing));
        }

        [Test]
        public void ElementalAffinityModifierCanBeParsed()
        {
            List<IEffect> effects = EffectsParser.ParseEffectsFromNode(GetEffectsNode("ElementalAffinityModifierData"));

            Assert.IsTrue(effects[0] is ElementalAffinityModifier);

            ElementalAffinityModifier modifier = effects[0] as ElementalAffinityModifier;

            Assert.AreEqual(Element.Earth, modifier.Element);
            Assert.IsTrue(Mathf.Abs(99.9f - modifier.Value) < 0.01f);
        }

        [Test]
        public void HealingItemsEfficiencyCanBeParsed()
        {
            List<IEffect> effects = EffectsParser.ParseEffectsFromNode(GetEffectsNode("HealingItemsEfficiencyData"));

            Assert.IsTrue(effects[0] is HealingItemsEfficiency);

            HealingItemsEfficiency efficiency = effects[0] as HealingItemsEfficiency;

            Assert.IsTrue(Mathf.Abs(55.5f - efficiency.Value) < 0.01f);
        }

        [Test]
        public void HpThresholdBonusDamageCanBeParsed()
        {
            List<IEffect> effects = EffectsParser.ParseEffectsFromNode(GetEffectsNode("HpThresholdBonusDamageData"));

            Assert.IsTrue(effects[0] is HpThresholdBonusDamage);

            HpThresholdBonusDamage damage = effects[0] as HpThresholdBonusDamage;

            Assert.IsTrue(Mathf.Abs(30.5f - damage.Threshold) < 0.01f);
        }

        [Test]
        public void InflictStatusCanBeParsed()
        {
            List<IEffect> effects = EffectsParser.ParseEffectsFromNode(GetEffectsNode("InflictStatusData"));

            Assert.IsTrue(effects[0] is global::Effects.InflictStatus);

            global::Effects.InflictStatus status = effects[0] as global::Effects.InflictStatus;

            Assert.AreEqual(EffectType.BleedII, status.Value);
        }

        [Test]
        public void MagicIgnoresDefenseStatCanBeParsed()
        {
            List<IEffect> effects = EffectsParser.ParseEffectsFromNode(GetEffectsNode("MagicIgnoresDefenseStatData"));

            Assert.IsTrue(effects[0] is MagicIgnoresDefenseStat);

            MagicIgnoresDefenseStat ignore = effects[0] as MagicIgnoresDefenseStat;

            Assert.AreEqual(Stat.MagicDefense, ignore.Stat);
            Assert.IsTrue(Mathf.Abs(48.2f - ignore.Value) < 0.01f);
        }

        [Test]
        public void MeleeDistanceScalingDamageCanBeParsed()
        {
            List<IEffect> effects = EffectsParser.ParseEffectsFromNode(GetEffectsNode("MeleeDistanceScalingDamageData"));

            Assert.IsTrue(effects[0] is MeleeDistanceScalingDamage);

            MeleeDistanceScalingDamage damage = effects[0] as MeleeDistanceScalingDamage;

            Assert.AreEqual(1050, damage.DistanceCap);
            Assert.IsTrue(Mathf.Abs(19.4f - damage.Value) < 0.01f);
        }

        [Test]
        public void NegativeStatusBonusDamageCanBeParsed()
        {
            List<IEffect> effects = EffectsParser.ParseEffectsFromNode(GetEffectsNode("NegativeStatusBonusDamageData"));

            Assert.IsTrue(effects[0] is NegativeStatusBonusDamage);

            NegativeStatusBonusDamage damage = effects[0] as NegativeStatusBonusDamage;

            Assert.IsTrue(Mathf.Abs(89.7f - damage.Value) < 0.01f);
        }

        [Test]
        public void PiercingProjectileCanBeParsed()
        {
            List<IEffect> effects = EffectsParser.ParseEffectsFromNode(GetEffectsNode("PiercingProjectileData"));

            Assert.IsTrue(effects[0] is PiercingProjectile);

            PiercingProjectile projectile = effects[0] as PiercingProjectile;

            Assert.IsTrue(Mathf.Abs(55.2f - projectile.MinDamage) < 0.01f);
            Assert.IsTrue(Mathf.Abs(48.8f - projectile.DamageReduction) < 0.01f);
        }

        [Test]
        public void RefillOnParryCanBeParsed()
        {
            List<IEffect> effects = EffectsParser.ParseEffectsFromNode(GetEffectsNode("RefillOnParryData"));

            Assert.IsTrue(effects[0] is RefillOnParry);

            RefillOnParry refill = effects[0] as RefillOnParry;

            Assert.AreEqual(Stat.MP, refill.Stat);
            Assert.IsTrue(Mathf.Abs(12.6f - refill.Value) < 0.01f);
        }

        [Test]
        public void RestoresResourceScalingCanBeParsed()
        {
            List<IEffect> effects = EffectsParser.ParseEffectsFromNode(GetEffectsNode("RestoresResourceScalingData"));

            Assert.IsTrue(effects[0] is RestoresResourceScaling);

            RestoresResourceScaling restore = effects[0] as RestoresResourceScaling;

            Assert.AreEqual(Stat.MP, restore.Stat);
            Assert.IsTrue(Mathf.Abs(50.6f - restore.Value) < 0.01f);
        }

        [Test]
        public void RevivesCanBeParsed()
        {
            List<IEffect> effects = EffectsParser.ParseEffectsFromNode(GetEffectsNode("RevivesData"));

            Assert.IsTrue(effects[0] is Revives);
        }

        [Test]
        public void SelfStatusCanBeParsed()
        {
            List<IEffect> effects = EffectsParser.ParseEffectsFromNode(GetEffectsNode("SelfStatusData"));

            Assert.IsTrue(effects[0] is SelfStatus);

            SelfStatus status = effects[0] as SelfStatus;

            Assert.AreEqual(EffectType.Silence, status.Value);
        }

        [Test]
        public void StatBoostCanBeParsed()
        {
            List<IEffect> effects = EffectsParser.ParseEffectsFromNode(GetEffectsNode("StatBoostData"));

            Assert.IsTrue(effects[0] is StatBoost);

            StatBoost boost = effects[0] as StatBoost;

            Assert.AreEqual(Stat.Strength, boost.Stat);
            Assert.IsTrue(Mathf.Abs(25.4f - boost.Value) < 0.01f);
        }

        [Test]
        public void StatusImmunityCanBeParsed()
        {
            List<IEffect> effects = EffectsParser.ParseEffectsFromNode(GetEffectsNode("StatusImmunityData"));

            Assert.IsTrue(effects[0] is StatusImmunity);

            StatusImmunity immunity = effects[0] as StatusImmunity;

            Assert.AreEqual(EffectType.Poison, immunity.Value);
        }

        [Test]
        public void TargetEffectivenessCanBeParsed()
        {
            List<IEffect> effects = EffectsParser.ParseEffectsFromNode(GetEffectsNode("TargetEffectivenessData"));

            Assert.IsTrue(effects[0] is TargetEffectiveness);

            TargetEffectiveness effectiveness = effects[0] as TargetEffectiveness;

            Assert.AreEqual(TargetTribe.Human, effectiveness.Type);
            Assert.IsTrue(Mathf.Abs(69.3f - effectiveness.Value) < 0.01f);
        }

        [Test]
        public void UndamagedTargetBonusDamageCanBeParsed()
        {
            List<IEffect> effects = EffectsParser.ParseEffectsFromNode(GetEffectsNode("UndamagedTargetBonusDamageData"));

            Assert.IsTrue(effects[0] is UndamagedTargetBonusDamage);

            UndamagedTargetBonusDamage damage = effects[0] as UndamagedTargetBonusDamage;

            Assert.IsTrue(Mathf.Abs(25.1f - damage.Value) < 0.01f);
        }
    }
}
