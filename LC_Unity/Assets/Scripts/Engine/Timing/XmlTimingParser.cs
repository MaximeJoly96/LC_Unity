using System.Xml;

namespace Engine.Timing
{
    public static class XmlTimingParser
    {
        public static Wait ParseWait(XmlNode data)
        {
            Wait wait = new Wait();

            wait.Duration = int.Parse(data.Attributes["Duration"].InnerText);

            return wait;
        }
    }
}
