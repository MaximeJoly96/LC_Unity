using System;
using System.Collections.Generic;
using System.Xml;
using Logging;
using Actors;
using UnityEngine;

namespace Essence
{
    public enum EssenceType
    {
        Vigor,
        Knowledge,
        Might,
        Rage,
        Wisdom,
        Willpower,
        Freedom,
        Hope
    }

    public class EssentialAffinitiesParser
    {
        public static List<EssenceAffinity> GetEssentialAffinities(TextAsset file)
        {
            List<EssenceAffinity> affinities = new List<EssenceAffinity>();

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(file.text);

                XmlNodeList affinitiesNodes = xmlDoc.SelectSingleNode("Affinities").SelectNodes("Affinity");
                foreach(XmlNode affinityNode in affinitiesNodes)
                {
                    affinities.Add(ParseNodeIntoAffinity(affinityNode));
                }
            }
            catch(Exception e)
            {
                LogsHandler.Instance.LogError("Could not parse Essential Affinities. Reason: " + e.Message);
            }

            return affinities;
        }

        private static EssenceAffinity ParseNodeIntoAffinity(XmlNode node)
        {
            int id = int.Parse(node.SelectSingleNode("Id").InnerText);
            string name = node.SelectSingleNode("Name").InnerText;
            string description = node.SelectSingleNode("Description").InnerText;
            EssenceType essence = (EssenceType)Enum.Parse(typeof(EssenceType), node.SelectSingleNode("Essence").InnerText);
            EssentialAffinityEffect effect = ParseAffinityEffect(node.SelectSingleNode("Condition").InnerText);

            return new EssenceAffinity(id, name, description, essence, effect);
        }

        private static EssentialAffinityEffect ParseAffinityEffect(string value)
        {
            if (value == typeof(AllyGetsPositiveStatus).Name)
                return new AllyGetsPositiveStatus();
            else if (value == typeof(CriticalStrikeLanded).Name)
                return new CriticalStrikeLanded();
            else if (value == typeof(DamageDealtToEnemy).Name)
                return new DamageDealtToEnemy();
            else if (value == typeof(DamageTaken).Name)
                return new DamageTaken();
            else if (value == typeof(DifferentConsecutiveMoves).Name)
                return new DifferentConsecutiveMoves();
            else if (value == typeof(EnemySlain).Name)
                return new EnemySlain();
            else if (value == typeof(ExploitElementalWeakness).Name)
                return new ExploitElementalWeakness();
            else if (value == typeof(HealsOtherAlly).Name)
                return new HealsOtherAlly();
            else if (value == typeof(IncreasesEachTurn).Name)
                return new IncreasesEachTurn();
            else if (value == typeof(OtherAllyRevived).Name)
                return new OtherAllyRevived();
            else if (value == typeof(OtherAllyTakesDamage).Name)
                return new OtherAllyTakesDamage();
            else if (value == typeof(ParryOrEvasion).Name)
                return new ParryOrEvasion();
            else if (value == typeof(ScalesWithMovement).Name)
                return new ScalesWithMovement();
            else if (value == typeof(SpendsMp).Name)
                return new SpendsMp();
            else if (value == typeof(StartsTurnWithLowHealth).Name)
                return new StartsTurnWithLowHealth();
            else if (value == typeof(SuffersNegativeStatus).Name)
                return new SuffersNegativeStatus();
            else
                throw new Exception("Provided condition for effect is not supported");
        }
    }
}
