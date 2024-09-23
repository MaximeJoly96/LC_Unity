using System;
using System.Xml;
using BattleSystem.Model;
using UnityEngine;
using Logging;
using System.Collections.Generic;
using System.Globalization;

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
                                        ParseMembers(troops[i - 1]),
                                        ParsePlayerSpots(troops[i - 1]));

                    return t;
                }
            }
            catch(Exception e)
            {
                LogsHandler.Instance.LogError("Troop with ID " + id + " could not be found. Details = " + e.Message);
            }

            return null;
        }

        private List<TroopMember> ParseMembers(XmlNode troop)
        {
            List<TroopMember> members = new List<TroopMember>();

            XmlNodeList membersXml = troop.SelectNodes("Member");

            for(int i = 0; i < membersXml.Count; i++)
            {
                TroopMember member = new TroopMember(int.Parse(membersXml[i].Attributes["Id"].InnerText),
                                                     float.Parse(membersXml[i].Attributes["X"].InnerText, CultureInfo.InvariantCulture),
                                                     float.Parse(membersXml[i].Attributes["Y"].InnerText, CultureInfo.InvariantCulture));
                members.Add(member);
            }

            return members;
        }

        private List<PlayerSpot> ParsePlayerSpots(XmlNode troop)
        {
            List<PlayerSpot> spots = new List<PlayerSpot>();

            XmlNode spotsList = troop.SelectSingleNode("PlayerSpots");

            foreach(XmlNode node in spotsList.ChildNodes)
            {
                PlayerSpot spot = new PlayerSpot(int.Parse(node.Attributes["Id"].InnerText),
                                                 float.Parse(node.Attributes["X"].InnerText, CultureInfo.InvariantCulture),
                                                 float.Parse(node.Attributes["Y"].InnerText, CultureInfo.InvariantCulture));
                spots.Add(spot);
            }

            return spots;
        }
    }
}
