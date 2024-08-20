using System;
using System.Collections.Generic;
using UnityEngine;
using Logging;
using System.Xml;

namespace Inventory
{
    public class AccessoriesParser : ItemParser
    {
        public static List<Accessory> ParseAccessories(TextAsset file)
        {
            List<Accessory> accessories = new List<Accessory>();

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(file.text);

                XmlNodeList itemsNode = xmlDoc.SelectSingleNode("Items").SelectNodes("Item");
                foreach (XmlNode item in itemsNode)
                {
                    Accessory accessory = ParseNodeIntoAccessory(item);
                    accessories.Add(accessory);
                }
            }
            catch (Exception e)
            {
                LogsHandler.Instance.LogError("Could not parse accessories. Reason: " + e.Message);
            }

            return accessories;
        }

        private static Accessory ParseNodeIntoAccessory(XmlNode node)
        {
            int id = int.Parse(node.SelectSingleNode("Id").InnerText);
            string name = node.SelectSingleNode("Name").InnerText;
            string description = node.SelectSingleNode("Description").InnerText;
            int icon = int.Parse(node.SelectSingleNode("Icon").InnerText);
            int price = int.Parse(node.SelectSingleNode("Price").InnerText);
            int enchantmentSlots = int.Parse(node.SelectSingleNode("EnchantmentSlots").InnerText);

            Accessory accessory = new Accessory(id, name, description, icon, price, ItemCategory.Accessory, enchantmentSlots);

            XmlNode effectsNode = node.SelectSingleNode("Effects");
            if (effectsNode != null)
                accessory.AddEffects(ParseEffectsFromNode(effectsNode));

            return accessory;
        }
    }
}
