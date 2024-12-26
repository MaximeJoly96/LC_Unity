using UnityEngine;
using System.Xml;
using UnityEditor;

namespace Testing.Engine
{
    public abstract class XmlBaseParser : TestFoundation
    {
        protected abstract string TestFilePath { get; }

        protected XmlNode GetDataToParse(string type)
        {
            return GetDataToParse(type, 0);
        }

        protected XmlNode GetDataToParse(string type, int nodeId)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(AssetDatabase.LoadAssetAtPath<TextAsset>(TestFilePath).text);

            XmlNode sequence = document.SelectSingleNode("EventsSequence");
            return sequence.SelectNodes(type)[nodeId];
        }
    }
}
