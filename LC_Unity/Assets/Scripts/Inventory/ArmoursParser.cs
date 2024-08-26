using System;
using System.Collections.Generic;
using UnityEngine;
using Logging;
using System.Xml;

namespace Inventory
{
    public class ArmoursParser : ItemParser
    {
        public static List<Armour> ParseArmours(TextAsset file)
        {
            List<Armour> armours = new List<Armour>();

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(file.text);

                XmlNodeList itemsNode = xmlDoc.SelectSingleNode("Items").SelectNodes("Item");
                foreach (XmlNode item in itemsNode)
                {
                    Armour armour = ParseNodeIntoArmour(item);
                    armours.Add(armour);
                }
            }
            catch (Exception e)
            {
                LogsHandler.Instance.LogError("Could not parse armours. Reason: " + e.Message);
            }

            return armours;
        }

        private static Armour ParseNodeIntoArmour(XmlNode node)
        {
            int id = int.Parse(node.SelectSingleNode("Id").InnerText);
            string name = node.SelectSingleNode("Name").InnerText;
            string description = node.SelectSingleNode("Description").InnerText;
            int icon = int.Parse(node.SelectSingleNode("Icon").InnerText);
            int price = int.Parse(node.SelectSingleNode("Price").InnerText);
            int enchantmentSlots = int.Parse(node.SelectSingleNode("EnchantmentSlots").InnerText);
            ArmourType type = (ArmourType)Enum.Parse(typeof(ArmourType), node.SelectSingleNode("Type").InnerText);

            Armour armour = new Armour(id, name, description, icon, price, ItemCategory.Armour, enchantmentSlots, type);

            XmlNode effectsNode = node.SelectSingleNode("Effects");
            if (effectsNode != null)
                armour.AddEffects(ParseEffectsFromNode(effectsNode));

            XmlNode statsNode = node.SelectSingleNode("Stats");
            if (statsNode != null)
                armour.Stats = ParseItemStats(statsNode);

            return armour;
        }
    }
}
