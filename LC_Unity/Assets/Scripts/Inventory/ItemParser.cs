using System;
using System.Xml;
using System.Linq;
using Logging;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using Effects;
using Actors;

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

        public static ItemRecipeComponent ParseRecipeComponentFromNode(XmlNode node)
        {
            int id = int.Parse(node.Attributes["Id"].InnerText);
            int quantity = int.Parse(node.Attributes["Quantity"].InnerText);

            return new ItemRecipeComponent(id, quantity);
        }

        public static ItemStats ParseItemStats(XmlNode node)
        {
            return new ItemStats(ParseIntStat("Health", node),
                                 ParseIntStat("Mana", node),
                                 ParseIntStat("Essence", node),
                                 ParseIntStat("Strength", node),
                                 ParseIntStat("Defense", node),
                                 ParseIntStat("Magic", node),
                                 ParseIntStat("MagicDefense", node),
                                 ParseIntStat("Agility", node),
                                 ParseIntStat("Luck", node));
        }

        private static int ParseIntStat(string key, XmlNode node)
        {
            XmlNode subNode = node.SelectSingleNode(key);
            return subNode != null ? int.Parse(subNode.InnerText) : 0;
        }
    }
}
