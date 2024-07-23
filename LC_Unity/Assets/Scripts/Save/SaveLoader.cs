using UnityEngine;
using System.IO;
using System.Collections.Generic;

namespace Save
{
    public class SaveLoader
    {
        public Dictionary<string, string> LoadSaveFile(int slotId)
        {
            string path = Application.persistentDataPath + "/save" + slotId + ".data";
            Dictionary<string, string> data = new Dictionary<string, string>();
            string content = "";

            using(StreamReader sr = new StreamReader(path))
            {
                content = sr.ReadToEnd();
            }

            string[] splitContent = content.Split("\r\n");

            for(int i = 0; i < splitContent.Length; i++)
            {
                string[] splitLine = splitContent[i].Split('=');
                data.Add(splitLine[0], splitLine[1]);
            }

            return data;
        }
    }
}
