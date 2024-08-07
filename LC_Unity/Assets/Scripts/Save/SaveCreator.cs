﻿using UnityEngine;
using System.IO;
using System.Collections.Generic;
using Movement;
using System.Globalization;
using Timing;
using Actors;
using Party;
using Inventory;

namespace Save
{
    public class SaveCreator
    {
        public Dictionary<string, string> CreateSaveFile(int slotId)
        {
            string path = Application.persistentDataPath + "/save" + slotId + ".data";
            Dictionary<string, string> data = new Dictionary<string, string>();

            PlayerController pc = Object.FindObjectOfType<PlayerController>();
            Vector2 playerPos = new Vector2();

            if(pc)
                playerPos = pc.transform.position;

            data.Add("positionX", playerPos.x.ToString(CultureInfo.InvariantCulture));
            data.Add("positionY", playerPos.y.ToString(CultureInfo.InvariantCulture));
            data.Add("mapId", 0.ToString(CultureInfo.InvariantCulture));
            data.Add("inGameTime", Object.FindObjectOfType<GlobalTimer>().InGameTimeSeconds.ToString(CultureInfo.InvariantCulture));

            List<Character> party = PartyManager.Instance.GetParty();
            List<InventoryItem> inventory = PartyManager.Instance.Inventory;

            foreach (Character c in party)
                data.Add("character" + c.Id, c.Serialize());

            foreach(InventoryItem i in inventory)
                data.Add("item" + i.ItemData.Id, i.Serialize());

            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach(KeyValuePair<string, string> kvp in data)
                {
                    string line = kvp.Key + "=" + kvp.Value;
                    sw.WriteLine(line);
                }
            }

            return data;
        }
    }
}
