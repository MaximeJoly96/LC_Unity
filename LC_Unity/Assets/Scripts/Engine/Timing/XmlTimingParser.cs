using System.Xml;
using System.Globalization;

namespace Engine.Timing
{
    public static class XmlTimingParser
    {
        public static Wait ParseWait(XmlNode data)
        {
            Wait wait = new Wait();

            wait.Duration = float.Parse(data.Attributes["Duration"].InnerText, CultureInfo.InvariantCulture);

            return wait;
        }
    }
}
