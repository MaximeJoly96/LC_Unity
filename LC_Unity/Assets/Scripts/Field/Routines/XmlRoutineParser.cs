using System.Xml;
using UnityEngine;
using System;
using Logging;
using Engine.Movement;

namespace Field.Routines
{
    public class XmlRoutineParser
    {
        public static AgentRoutine ParseRoutine(TextAsset file)
        {
            AgentRoutine routine = new AgentRoutine();

            try
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(file.text);

                XmlNode routineNode = document.SelectSingleNode("Routine");
                
                routine = new AgentRoutine(XmlMovementParser.ParseMoves(routineNode));
            }
            catch(Exception e)
            {
                LogsHandler.Instance.LogError("Could not parse Routine. Reason: " + e.Message);
            }

            return routine;
        }
    }
}
