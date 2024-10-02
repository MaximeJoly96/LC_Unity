using System.Xml;
using System.Collections.Generic;
using Actors;
using System.Globalization;
using System;

namespace Effects
{
    public static class EffectsParser
    {
        public static List<IEffect> ParseEffectsFromNode(XmlNode node)
        {
            List<IEffect> effects = new List<IEffect>();

            XmlNodeList effectNodes = node.ChildNodes;
            foreach (XmlNode effectNode in effectNodes)
            {
                string name = effectNode.Name;
                IEffect effect = null;

                if (name.Equals(typeof(BoundAbility).Name))
                {
                    effect = new BoundAbility
                    {
                        AbilityId = int.Parse(effectNode.Attributes["Id"].InnerText)
                    };
                }
                else if (name.Equals(typeof(CostReduction).Name))
                {
                    effect = new CostReduction
                    {
                        Stat = (Stat)Enum.Parse(typeof(Stat), effectNode.Attributes["Stat"].InnerText),
                        Value = float.Parse(effectNode.Attributes["Value"].InnerText, CultureInfo.InvariantCulture)
                    };
                }
                else if (name.Equals(typeof(Dispel).Name))
                {
                    effect = new Dispel
                    {
                        Value = (Actors.EffectType)Enum.Parse(typeof(Actors.EffectType), effectNode.Attributes["Value"].InnerText)
                    };
                }
                else if (name.Equals(typeof(InflictStatus).Name))
                {
                    effect = new InflictStatus
                    {
                        Value = (Actors.EffectType)Enum.Parse(typeof(Actors.EffectType), effectNode.Attributes["Value"].InnerText)
                    };
                }
                else if (name.Equals(typeof(SelfStatus).Name))
                {
                    effect = new SelfStatus
                    {
                        Value = (Actors.EffectType)Enum.Parse(typeof(Actors.EffectType), effectNode.Attributes["Value"].InnerText)
                    };
                }
                else if (name.Equals(typeof(StatBoost).Name))
                {
                    effect = new StatBoost
                    {
                        Stat = (Stat)Enum.Parse(typeof(Stat), effectNode.Attributes["Stat"].InnerText),
                        Value = float.Parse(effectNode.Attributes["Value"].InnerText, CultureInfo.InvariantCulture)
                    };
                }
                else if (name.Equals(typeof(StatusImmunity).Name))
                {
                    effect = new StatusImmunity
                    {
                        Value = (Actors.EffectType)Enum.Parse(typeof(Actors.EffectType), effectNode.Attributes["Value"].InnerText)
                    };
                }
                else if (name.Equals(typeof(HealingItemsEfficiency).Name))
                {
                    effect = new HealingItemsEfficiency
                    {
                        Value = float.Parse(effectNode.Attributes["Value"].InnerText, CultureInfo.InvariantCulture)
                    };
                }
                else if (name.Equals(typeof(TargetEffectiveness).Name))
                {
                    effect = new TargetEffectiveness
                    {
                        Type = (TargetTribe)Enum.Parse(typeof(TargetTribe), effectNode.Attributes["Type"].InnerText),
                        Value = float.Parse(effectNode.Attributes["Value"].InnerText, CultureInfo.InvariantCulture)
                    };
                }
                else if (name.Equals(typeof(ElementalAffinityModifier).Name))
                {
                    effect = new ElementalAffinityModifier
                    {
                        Value = float.Parse(effectNode.Attributes["Value"].InnerText, CultureInfo.InvariantCulture),
                        Element = (Element)Enum.Parse(typeof(Element), effectNode.Attributes["Element"].InnerText)
                    };
                }
                else if (name.Equals(typeof(NegativeStatusBonusDamage).Name))
                {
                    effect = new NegativeStatusBonusDamage
                    {
                        Value = float.Parse(effectNode.Attributes["Value"].InnerText, CultureInfo.InvariantCulture)
                    };
                }
                else if (name.Equals(typeof(DrainFromDamage).Name))
                {
                    effect = new DrainFromDamage
                    {
                        Stat = (Stat)Enum.Parse(typeof(Stat), effectNode.Attributes["Stat"].InnerText),
                        Value = float.Parse(effectNode.Attributes["Value"].InnerText, CultureInfo.InvariantCulture)
                    };
                }
                else if (name.Equals(typeof(HpThresholdBonusDamage).Name))
                {
                    effect = new HpThresholdBonusDamage
                    {
                        Threshold = float.Parse(effectNode.Attributes["Threshold"].InnerText, CultureInfo.InvariantCulture)
                    };
                }
                else if (name.Equals(typeof(BonusElementalDamage).Name))
                {
                    effect = new BonusElementalDamage();
                    string elementValue = effectNode.Attributes["Element"].InnerText;
                    if (elementValue == "All")
                        (effect as BonusElementalDamage).AddAll();
                    else
                        (effect as BonusElementalDamage).AddElement((Element)Enum.Parse(typeof(Element), elementValue));

                    (effect as BonusElementalDamage).Value = float.Parse(effectNode.Attributes["Value"].InnerText, CultureInfo.InvariantCulture);
                }
                else if (name.Equals(typeof(ElementalAbilitiesCostReduction).Name))
                {
                    effect = new ElementalAbilitiesCostReduction();
                    string elementValue = effectNode.Attributes["Element"].InnerText;
                    if (elementValue == "All")
                        (effect as ElementalAbilitiesCostReduction).AddAll();
                    else
                        (effect as ElementalAbilitiesCostReduction).AddElement((Element)Enum.Parse(typeof(Element), elementValue));

                    (effect as ElementalAbilitiesCostReduction).Value = float.Parse(effectNode.Attributes["Value"].InnerText, CultureInfo.InvariantCulture);
                }
                else if (name.Equals(typeof(ElementalAffinityExploitManaRefund).Name))
                {
                    effect = new ElementalAffinityExploitManaRefund();
                    string elementValue = effectNode.Attributes["Element"].InnerText;
                    if (elementValue == "All")
                        (effect as ElementalAffinityExploitManaRefund).AddAll();
                    else
                        (effect as ElementalAffinityExploitManaRefund).AddElement((Element)Enum.Parse(typeof(Element), elementValue));

                    (effect as ElementalAffinityExploitManaRefund).Value = float.Parse(effectNode.Attributes["Value"].InnerText, CultureInfo.InvariantCulture);
                }
                else if (name.Equals(typeof(ElementalAffinityExploitSelfStatus).Name))
                {
                    effect = new ElementalAffinityExploitSelfStatus();
                    string elementValue = effectNode.Attributes["Element"].InnerText;
                    if (elementValue == "All")
                        (effect as ElementalAffinityExploitSelfStatus).AddAll();
                    else
                        (effect as ElementalAffinityExploitSelfStatus).AddElement((Element)Enum.Parse(typeof(Element), elementValue));

                    (effect as ElementalAffinityExploitSelfStatus).Value = (EffectType)Enum.Parse(typeof(EffectType), effectNode.Attributes["Value"].InnerXml);
                }
                else if (name.Equals(typeof(CastPositiveStatusDurationExtension).Name))
                {
                    effect = new CastPositiveStatusDurationExtension
                    {
                        Turns = int.Parse(effectNode.Attributes["Turns"].InnerText)
                    };
                }
                else if (name.Equals(typeof(MagicIgnoresDefenseStat).Name))
                {
                    effect = new MagicIgnoresDefenseStat
                    {
                        Value = float.Parse(effectNode.Attributes["Value"].InnerText, CultureInfo.InvariantCulture),
                        Stat = (Stat)Enum.Parse(typeof(Stat), effectNode.Attributes["Stat"].InnerText)
                    };
                }
                else if (name.Equals(typeof(RefillOnParry).Name))
                {
                    effect = new RefillOnParry
                    {
                        Value = float.Parse(effectNode.Attributes["Value"].InnerText, CultureInfo.InvariantCulture),
                        Stat = (Stat)Enum.Parse(typeof(Stat), effectNode.Attributes["Stat"].InnerText)
                    };
                }
                else if (name.Equals(typeof(RefundsOnKill).Name))
                {
                    effect = new RefundsOnKill
                    {
                        Value = float.Parse(effectNode.Attributes["Value"].InnerText, CultureInfo.InvariantCulture),
                        Stat = (Stat)Enum.Parse(typeof(Stat), effectNode.Attributes["Stat"].InnerText)
                    };
                }
                else if (name.Equals(typeof(DegressiveRangeDamage).Name))
                {
                    effect = new DegressiveRangeDamage()
                    {
                        MinDamage = float.Parse(effectNode.Attributes["MinDamage"].InnerText, CultureInfo.InvariantCulture)
                    };
                }
                else if (name.Equals(typeof(UndamagedTargetBonusDamage).Name))
                {
                    effect = new UndamagedTargetBonusDamage()
                    {
                        Value = float.Parse(effectNode.Attributes["Value"].InnerText, CultureInfo.InvariantCulture)
                    };
                }
                else if (name.Equals(typeof(AttacksIgnoreDefenseStat).Name))
                {
                    effect = new AttacksIgnoreDefenseStat()
                    {
                        Value = float.Parse(effectNode.Attributes["Value"].InnerText, CultureInfo.InvariantCulture),
                        Stat = (Stat)Enum.Parse(typeof(Stat), effectNode.Attributes["Stat"].InnerText)
                    };
                }
                else if (name.Equals(typeof(AdditionalStrike).Name))
                {
                    effect = new AdditionalStrike()
                    {
                        Amount = int.Parse(effectNode.Attributes["Amount"].InnerText)
                    };
                }
                else if (name.Equals(typeof(AreaOfEffectAsSecondaryDamage).Name))
                {
                    effect = new AreaOfEffectAsSecondaryDamage()
                    {
                        Element = (Element)Enum.Parse(typeof(Element), effectNode.Attributes["Element"].InnerText),
                        BaseDamage = int.Parse(effectNode.Attributes["BaseDamage"].InnerText)
                    };
                }
                else if (name.Equals(typeof(BonusDamageToShields).Name))
                {
                    effect = new BonusDamageToShields()
                    {
                        Value = float.Parse(effectNode.Attributes["Value"].InnerText, CultureInfo.InvariantCulture)
                    };
                }
                else if (name.Equals(typeof(CounterattackAfterParry).Name))
                {
                    effect = new CounterattackAfterParry();
                }
                else if (name.Equals(typeof(MeleeDistanceScalingDamage).Name))
                {
                    effect = new MeleeDistanceScalingDamage()
                    {
                        DistanceCap = int.Parse(effectNode.Attributes["DistanceCap"].InnerText),
                        Value = float.Parse(effectNode.Attributes["Value"].InnerText, CultureInfo.InvariantCulture)
                    };
                }
                else if (name.Equals(typeof(AttackPriorityModifier).Name))
                {
                    effect = new AttackPriorityModifier()
                    {
                        Value = int.Parse(effectNode.Attributes["Value"].InnerText)
                    };
                }
                else if (name.Equals(typeof(ConeAttackWithBullets).Name))
                {
                    effect = new ConeAttackWithBullets()
                    {
                        Angle = float.Parse(effectNode.Attributes["Angle"].InnerText, CultureInfo.InvariantCulture),
                        Bullets = int.Parse(effectNode.Attributes["Bullets"].InnerText),
                        DamageReduction = float.Parse(effectNode.Attributes["DamageReduction"].InnerText, CultureInfo.InvariantCulture)
                    };
                }
                else if (name.Equals(typeof(PiercingProjectile).Name))
                {
                    effect = new PiercingProjectile()
                    {
                        DamageReduction = float.Parse(effectNode.Attributes["DamageReduction"].InnerText, CultureInfo.InvariantCulture),
                        MinDamage = float.Parse(effectNode.Attributes["MinDamage"].InnerText, CultureInfo.InvariantCulture)
                    };
                }
                else if (name.Equals(typeof(AutoAttackAfterAbility).Name))
                {
                    effect = new AutoAttackAfterAbility();
                }
                else if (name.Equals(typeof(RestoresResourceScaling).Name))
                {
                    effect = new RestoresResourceScaling()
                    {
                        Stat = (Stat)Enum.Parse(typeof(Stat), effectNode.Attributes["Stat"].InnerText),
                        Value = float.Parse(effectNode.Attributes["Value"].InnerText, CultureInfo.InvariantCulture)
                    };
                }
                else if (name.Equals(typeof(Revives).Name))
                {
                    effect = new Revives();
                }

                if (effect != null)
                    effects.Add(effect);
            }

            return effects;
        }
    }
}
