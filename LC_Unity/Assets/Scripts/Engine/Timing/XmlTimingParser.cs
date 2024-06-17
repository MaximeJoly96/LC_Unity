using System.Xml;
using System.Globalization;
using Logging;
using System;

namespace Engine.Timing
{
    public static class XmlTimingParser
    {
        public static Wait ParseWait(XmlNode data)
        {
            Wait wait = new Wait();

            try
            {
                wait.Duration = float.Parse(data.Attributes["Duration"].InnerText, CultureInfo.InvariantCulture);
            }
            catch(Exception e)
            {
                LogsHandler.Instance.LogFatalError("XmlGameProgressionParser cannot parse ControlSwitch. Exception: " + e.Message);
            }

            return wait;
        }
    }
}
