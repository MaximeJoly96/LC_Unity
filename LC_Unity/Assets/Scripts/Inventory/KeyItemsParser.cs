using System;
using System.Collections.Generic;
using UnityEngine;
using Logging;
using System.Xml;

namespace Inventory
{
    public class KeyItemsParser : ItemParser
    {
        public static List<KeyItem> ParseKeyItems(TextAsset file)
        {
            List<KeyItem> keyItems = new List<KeyItem>();

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(file.text);

                XmlNodeList itemsNode = xmlDoc.SelectSingleNode("Items").SelectNodes("Item");
                foreach (XmlNode item in itemsNode)
                {
                    KeyItem keyItem = ParseNodeIntoKeyItem(item);
                    keyItems.Add(keyItem);
                }
            }
            catch (Exception e)
            {
                LogsHandler.Instance.LogError("Could not parse keyItems. Reason: " + e.Message);
            }

            return keyItems;
        }

        private static KeyItem ParseNodeIntoKeyItem(XmlNode node)
        {
            int id = int.Parse(node.SelectSingleNode("Id").InnerText);
            string name = node.SelectSingleNode("Name").InnerText;
            string description = node.SelectSingleNode("Description").InnerText;
            int icon = int.Parse(node.SelectSingleNode("Icon").InnerText);
            int price = int.Parse(node.SelectSingleNode("Price").InnerText);

            KeyItem keyItem = new KeyItem(id, name, description, icon, price, ItemCategory.KeyItem, ItemUsability.Never);

            return keyItem;
        }
    }
}
