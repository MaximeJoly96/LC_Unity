using System;
using System.Xml;
using System.Collections.Generic;
using UnityEngine;
using Logging;
using Inventory;
using Core.Model;

namespace Shop
{
    public class MerchantParser
    {
        public List<Merchant> ParseMerchants(TextAsset file)
        {
            List<Merchant> merchants = new List<Merchant>();
            ItemsWrapper itemsWrapper = GameObject.FindObjectOfType<ItemsWrapper>();

            try
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(file.text);

                XmlNode merchantsNode = document.SelectSingleNode("Merchants");
                XmlNodeList allMerchants = merchantsNode.SelectNodes("Merchant");

                foreach(XmlNode merchant in allMerchants)
                {
                    merchants.Add(ParseMerchantFromNode(merchant, itemsWrapper));
                }
            }
            catch(Exception e)
            {
                LogsHandler.Instance.LogError("Could not parse merchants. Reason: " + e.Message);
            }

            return merchants;
        }

        private Merchant ParseMerchantFromNode(XmlNode node, ItemsWrapper itemsWrapper)
        {
            Merchant merchant = new Merchant(new ElementIdentifier(int.Parse(node.Attributes["Id"].InnerText), 
                                                                   node.SelectSingleNode("Name").InnerText,
                                                                   node.SelectSingleNode("Description").InnerText));

            XmlNodeList itemsNodes = node.SelectSingleNode("Items").SelectNodes("Item");
            foreach(XmlNode item in itemsNodes)
            {
                BaseItem baseItem = itemsWrapper.GetItemFromId(int.Parse(item.Attributes["Id"].InnerText));
                if (baseItem != null)
                    merchant.AddItem(baseItem);
            }

            return merchant;
        }
    }
}
