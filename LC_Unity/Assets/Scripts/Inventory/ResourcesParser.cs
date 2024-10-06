using System;
using System.Collections.Generic;
using UnityEngine;
using Logging;
using System.Xml;
using Core.Model;

namespace Inventory
{
    public class ResourcesParser : ItemParser
    {
        public static List<Resource> ParseResources(TextAsset file)
        {
            List<Resource> resources = new List<Resource>();

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(file.text);

                XmlNodeList itemsNode = xmlDoc.SelectSingleNode("Items").SelectNodes("Item");
                foreach (XmlNode item in itemsNode)
                {
                    Resource resource = ParseNodeIntoResource(item);
                    resources.Add(resource);
                }
            }
            catch (Exception e)
            {
                LogsHandler.Instance.LogError("Could not parse resources. Reason: " + e.Message);
            }

            return resources;
        }

        private static Resource ParseNodeIntoResource(XmlNode node)
        {
            int id = int.Parse(node.SelectSingleNode("Id").InnerText);
            string name = node.SelectSingleNode("Name").InnerText;
            string description = node.SelectSingleNode("Description").InnerText;
            int icon = int.Parse(node.SelectSingleNode("Icon").InnerText);
            int price = int.Parse(node.SelectSingleNode("Price").InnerText);

            Resource resource = new Resource(new ElementIdentifier(id, name, description), icon, price, ItemCategory.Resource);

            return resource;
        }
    }
}
