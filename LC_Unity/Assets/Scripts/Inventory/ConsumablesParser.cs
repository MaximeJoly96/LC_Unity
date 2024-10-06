using System;
using System.Collections.Generic;
using UnityEngine;
using Logging;
using System.Xml;
using Effects;
using Core.Model;
using Abilities;

namespace Inventory
{
    public class ConsumablesParser : ItemParser
    {
        public static List<Consumable> ParseConsumables(TextAsset file)
        {
            List<Consumable> consumables = new List<Consumable>();

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(file.text);

                XmlNodeList itemsNode = xmlDoc.SelectSingleNode("Items").SelectNodes("Item");
                foreach(XmlNode item in itemsNode)
                {
                    Consumable cons = ParseNodeIntoConsumable(item);
                    consumables.Add(cons);
                }
            }
            catch(Exception e)
            {
                LogsHandler.Instance.LogError("Could not parse consumables. Reason: " + e.Message);
            }

            return consumables;
        }

        private static Consumable ParseNodeIntoConsumable(XmlNode node)
        {
            int id = int.Parse(node.SelectSingleNode("Id").InnerText);
            string name = node.SelectSingleNode("Name").InnerText;
            string description = node.SelectSingleNode("Description").InnerText;
            int icon = int.Parse(node.SelectSingleNode("Icon").InnerText);
            int price = int.Parse(node.SelectSingleNode("Price").InnerText);
            ItemUsability usability = (ItemUsability)Enum.Parse(typeof(ItemUsability), node.SelectSingleNode("Usability").InnerText);
            int priority = int.Parse(node.SelectSingleNode("Priority").InnerText);
            AbilityAnimation animation = ParseAbilityAnimation(node.SelectSingleNode("Animation"));

            Consumable cons = new Consumable(new ElementIdentifier(id, name, description), icon, price, ItemCategory.Consumable, usability, priority, animation);

            XmlNode effectsNode = node.SelectSingleNode("Effects");
            if (effectsNode != null)
                cons.AddEffects(EffectsParser.ParseEffectsFromNode(effectsNode));

            return cons;
        }
    }
}
