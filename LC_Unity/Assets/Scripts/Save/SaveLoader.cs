﻿using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Save
{
    public class SaveLoader
    {
        private static readonly string SAVE_BASE_PATH = Application.persistentDataPath;

        public Dictionary<string, string> LoadSaveFile(int slotId)
        {
            string path = SAVE_BASE_PATH + "/save" + slotId + ".data";
            Dictionary<string, string> data = new Dictionary<string, string>();
            string content = "";

            using(StreamReader sr = new StreamReader(path))
            {
                content = sr.ReadToEnd();
            }

            string[] splitContent = content.Split("\r\n");

            for(int i = 0; i < splitContent.Length; i++)
            {
                if(splitContent[i] != string.Empty)
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
                string[] split = file.Split("\\");

                string id = split[1].Replace("save", "").Replace(".data", "");
                ids.Add(int.Parse(id));
            }

            return ids;
        }
    }
}
