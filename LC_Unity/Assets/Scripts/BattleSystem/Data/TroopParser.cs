using System;
using System.Xml;
using BattleSystem.Model;
using UnityEngine;
using Logging;
using System.Collections.Generic;

namespace BattleSystem.Data
{
    public class TroopParser
    {
        public Troop ParseTroop(TextAsset file, int id)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(file.text);

                XmlNodeList troops = doc.SelectSingleNode("Troops").SelectNodes("Troop");
                bool found = false;
                int matchingId = -1;
                int i = 0;
                
                for(i = 0; i < troops.Count && !found; i++)
                {
                    matchingId = int.Parse(troops[i].Attributes["Id"].InnerText);
                    found = matchingId == id;
                }

                if(found)
                {
                    Troop t = new Troop(matchingId,
                                        ParseMembers(troops[i - 1]));

                    return t;
                }
            }
            catch(Exception e)
            {
                LogsHandler.Instance.LogError("Troop with ID " + id + " could not be found. Details = " + e.Message);
            }

            return null;
        }

        private List<int> ParseMembers(XmlNode troop)
        {
            List<int> members = new List<int>();

            XmlNodeList membersXml = troop.SelectNodes("Member");

            for(int i = 0; i < membersXml.Count; i++)
            {
                members.Add(int.Parse(membersXml[i].Attributes["Id"].InnerText));
            }

            return members;
        }
    }
}
