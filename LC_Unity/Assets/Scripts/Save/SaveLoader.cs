using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System;
using Logging;

namespace Save
{
    public class SaveLoader
    {
        private static readonly string SAVE_BASE_PATH = Application.persistentDataPath;

        public Dictionary<string, string> LoadSaveFile(int slotId)
        {
            string path = SAVE_BASE_PATH + "/save" + slotId + ".data";
            return LoadSaveFile(path);
        }

        public Dictionary<string, string> LoadSaveFile(string path)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            string content = "";

            using (StreamReader sr = new StreamReader(path))
            {
                content = sr.ReadToEnd();
            }

            string separator = "";

#if UNITY_ANDROID
            separator = "\n";
#else
            separator = "\r\n";
#endif
            string[] splitContent = content.Split(separator);

            for (int i = 0; i < splitContent.Length; i++)
            {
                if (splitContent[i] != string.Empty)
                {
                    string[] splitLine = splitContent[i].Split('=');
                    data.Add(splitLine[0], splitLine[1]);
                }
            }

            return data;
        }

        public List<int> GetSavesId()
        {
            List<int> ids = new List<int>();

            var files = Directory.GetFiles(SAVE_BASE_PATH).Where(f => f.EndsWith(".data"));
            foreach(string file in files)
            {
#if UNITY_ANDROID
                string[] split = file.Split("/");
                string id = split[split.Length - 1].Replace("save", "").Replace(".data", "");

                try
                {
                    ids.Add(int.Parse(id));
                }
                catch(FormatException e)
                {
                    LogsHandler.Instance.LogError("Could not get save ID. Reason: " + e.Message);
                }
#else
                string[] split = file.Split("\\");
                string id = split[1].Replace("save", "").Replace(".data", "");

                try
                {
                    ids.Add(int.Parse(id));
                }
                catch(FormatException e)
                {
                    LogsHandler.Instance.LogError("Could not get save ID. Reason: " + e.Message);
                }
#endif
            }

            return ids;
        }
    }
}
