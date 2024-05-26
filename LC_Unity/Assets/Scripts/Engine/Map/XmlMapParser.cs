using System;
using System.Xml;

namespace Engine.Map
{
    public static class XmlMapParser
    {
        public static ChangeMapNameDisplay ParseChangeMapNameDisplay(XmlNode data)
        {
            ChangeMapNameDisplay display = new ChangeMapNameDisplay();

            display.Enabled = bool.Parse(data.Attributes["Enabled"].InnerText);

            return display;
        }
    }
}
