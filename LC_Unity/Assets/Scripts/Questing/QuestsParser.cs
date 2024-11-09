using System.Xml;
using UnityEngine;
using System;
using Logging;
using System.Linq;
using System.Collections.Generic;
using Inventory;

namespace Questing
{
    public class QuestsParser
    {
        public static Quest ParseQuest(int id, TextAsset file)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(file.text);

                XmlNode mainNode = document.SelectSingleNode("Quests");
                XmlNodeList allQuests = mainNode.SelectNodes("Quest");

                return ParseQuestData(allQuests.OfType<XmlNode>().FirstOrDefault());
            }
            catch(Exception e)
            {
                LogsHandler.Instance.LogError("Could not parse quest with ID " + id + ". Reason: " + e.Message);
                return Quest.DefaultQuest();
            }
        }

        private static Quest ParseQuestData(XmlNode questNode)
        {
            int id = ParseIntAttribute("Id", questNode);
            string name = ParseStringAttribute("Name", questNode);
            string description = ParseStringAttribute("Description", questNode);
            QuestType type = (QuestType)Enum.Parse(typeof(QuestType), questNode.Attributes["Type"].InnerText);
            QuestReward reward = ParseReward(questNode.SelectSingleNode("Reward"));

            Quest quest = new Quest(id, name, description, type, reward);

            XmlNodeList stepsNodes = questNode.SelectSingleNode("Steps").SelectNodes("Step");
            foreach(XmlNode stepNode in stepsNodes)
            {
                QuestStep step = ParseQuestStep(stepNode);
                quest.AddStep(step);
            }

            return quest;
        }

        private static QuestReward ParseReward(XmlNode rewardNode)
        {
            int exp = ParseIntAttribute("Quantity", rewardNode.SelectSingleNode("Exp"));
            int gold = ParseIntAttribute("Quantity", rewardNode.SelectSingleNode("Gold"));

            List<InventoryItem> items = new List<InventoryItem>();

            XmlNodeList itemsNodes = rewardNode.SelectNodes("Item");
            foreach(XmlNode itemNode in itemsNodes)
            {
                items.Add(new InventoryItem(ParseIntAttribute("Id", itemNode),
                                            ParseIntAttribute("Quantity", itemNode)));
            }

            return new QuestReward(exp, gold, items);
        }

        private static QuestStep ParseQuestStep(XmlNode stepNode)
        {
            int id = ParseIntAttribute("Id", stepNode);
            string name = ParseStringAttribute("Name", stepNode);
            string description = ParseStringAttribute("Description", stepNode);

            QuestReward reward = ParseReward(stepNode.SelectSingleNode("Reward"));

            return new QuestStep(id, name, description, reward);
        }

        private static int ParseIntAttribute(string attributeName, XmlNode node)
        {
            return node != null ? int.Parse(node.Attributes[attributeName].InnerText) : 0;
        }

        private static string ParseStringAttribute(string attributeName, XmlNode node)
        {
            return node != null ? node.Attributes[attributeName].InnerText : string.Empty;
        }
    }
}
