using System;
using System.Xml;
using System.Linq;
using Logging;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using Effects;

namespace Inventory
{
    public class ItemParser
    {
        public static ItemRecipe ParseRecipeFromNode(XmlNode node)
        {
            ItemRecipe itemRecipe = new ItemRecipe();

            XmlNodeList componentNodes = node.SelectNodes("Item");
            foreach(XmlNode componentNode in componentNodes)
            {
                itemRecipe.AddComponent(ParseRecipeComponentFromNode(componentNode));
            }

            return itemRecipe;
        }

        public static List<IEffect> ParseEffectsFromNode(XmlNode node)
        {
            List<IEffect> effects = new List<IEffect>();

            XmlNodeList effectNodes = node.ChildNodes;
            foreach(XmlNode effectNode in effectNodes)
            {
                string name = effectNode.Name;
                IEffect effect = null;

                if(name.Equals(typeof(BoundAbility).Name))
                {
                    effect = new BoundAbility
                    {
                        AbilityId = int.Parse(effectNode.Attributes["Id"].InnerText)
                    };
                }
                else if(name.Equals(typeof(CostReduction).Name))
                {
                    effect = new CostReduction
                    {
                        Stat = (Stat)Enum.Parse(typeof(Stat), effectNode.Attributes["Stat"].InnerText),
                        Value = int.Parse(effectNode.Attributes["Value"].InnerText)
                    };
                }
                else if(name.Equals(typeof(Dispel).Name))
                {
                    effect = new Dispel
                    {
                        Value = (Actors.EffectType)Enum.Parse(typeof(Actors.EffectType), effectNode.Attributes["Value"].InnerText)
                    };
                }
                else if(name.Equals(typeof(InflictStatus).Name))
                {
                    effect = new InflictStatus
                    {
                        Value = (Actors.EffectType)Enum.Parse(typeof(Actors.EffectType), effectNode.Attributes["Value"].InnerText)
                    };
                }
                else if(name.Equals(typeof(SelfStatus).Name))
                {
                    effect = new SelfStatus
                    {
                        Value = (Actors.EffectType)Enum.Parse(typeof(Actors.EffectType), effectNode.Attributes["Value"].InnerText)
                    };
                }
                else if(name.Equals(typeof(StatBoost).Name))
                {
                    effect = new StatBoost
                    {
                        Stat = (Stat)Enum.Parse(typeof(Stat), effectNode.Attributes["Stat"].InnerText),
                        Value = float.Parse(effectNode.Attributes["Value"].InnerText, CultureInfo.InvariantCulture)
                    };
                }
                else if(name.Equals(typeof(StatusImmunity).Name))
                {
                    effect = new StatusImmunity
                    {
                        Value = (Actors.EffectType)Enum.Parse(typeof(Actors.EffectType), effectNode.Attributes["Value"].InnerText)
                    };
                }
                else if(name.Equals(typeof(HealingItemsEfficiency).Name))
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
                        Value = float.Parse(effectNode.Attributes["Value"].InnerText, CultureInfo.InvariantCulture)
                    };
                }
                else if (name.Equals(typeof(NegativeStatusBonusDamage).Name))
                {
                    effect = new NegativeStatusBonusDamage
                    {
                        Value = float.Parse(effectNode.Attributes["Value"].InnerText, CultureInfo.InvariantCulture)
                    };
                }

                if (effect != null)
                    effects.Add(effect);
            }

            return effects;
        }

        public static ItemRecipeComponent ParseRecipeComponentFromNode(XmlNode node)
        {
            int id = int.Parse(node.Attributes["Id"].InnerText);
            int quantity = int.Parse(node.Attributes["Quantity"].InnerText);

            return new ItemRecipeComponent(id, quantity);
        }
    }
}
