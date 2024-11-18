using UnityEngine;
using System.IO;
using System.Collections.Generic;
using Movement;
using System.Globalization;
using Timing;
using Actors;
using Party;
using Inventory;
using Utils;
using GameProgression;
using Core;
using System;
using Questing;

namespace Save
{
    public class SaveCreator
    {
        public Dictionary<string, string> CreateSaveData()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();

            data = StorePlayerPosition(data);
            data = StoreInGameTimer(data);
            data = StorePartyData(data);
            data = StoreInventoryData(data);
            data = StorePersistentData(data);
            data = StoreTimers(data);
            data = StoreQuests(data);

            return data;
        }

        public void WriteSaveDataToDisk(string path, Dictionary<string, string> data)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach (KeyValuePair<string, string> kvp in data)
                {
                    string line = kvp.Key + "=" + kvp.Value;
                    sw.WriteLine(line);
                }
            }
        }

        public void WriteSaveDataToDisk(int slotId, Dictionary<string, string> data)
        {
            WriteSaveDataToDisk(Application.persistentDataPath + "/save" + slotId + ".data", data);
        }

        public Dictionary<string, string> StorePlayerPosition(Dictionary<string, string> baseData)
        {
            PlayerController pc = UnityEngine.Object.FindObjectOfType<PlayerController>();
            Vector2 playerPos = new Vector2();

            if (pc)
                playerPos = pc.transform.position;

            baseData.Add("positionX", playerPos.x.ToString(CultureInfo.InvariantCulture));
            baseData.Add("positionY", playerPos.y.ToString(CultureInfo.InvariantCulture));
            baseData.Add("mapId", GlobalStateMachine.Instance.CurrentMapId == -1 ?
                        CommonLocations.INITIAL_MAP_ID.ToString(CultureInfo.InvariantCulture) :
                        GlobalStateMachine.Instance.CurrentMapId.ToString());

            return baseData;
        }

        public Dictionary<string, string> StoreInGameTimer(Dictionary<string, string> baseData)
        {
            GlobalTimer globalTimer = UnityEngine.Object.FindObjectOfType<GlobalTimer>();
            baseData.Add("inGameTime", globalTimer ? globalTimer.InGameTimeSeconds.ToString(CultureInfo.InvariantCulture) : "0");

            return baseData;
        }

        public Dictionary<string, string> StorePartyData(Dictionary<string, string> baseData)
        {
            List<Character> party = PartyManager.Instance.GetParty();

            foreach (Character c in party)
                baseData.Add("character" + c.Id, c.Serialize());

            return baseData;
        }

        public Dictionary<string, string> StoreInventoryData(Dictionary<string, string> baseData)
        {
            baseData.Add("gold", PartyManager.Instance.Gold.ToString());

            List<InventoryItem> inventory = PartyManager.Instance.Inventory;

            foreach (InventoryItem i in inventory)
                baseData.Add("item" + i.ItemData.Id, i.Serialize());

            return baseData;
        }

        public Dictionary<string, string> StorePersistentData(Dictionary<string, string> baseData)
        {
            Dictionary<string, object> persistentData = PersistentDataHolder.Instance.GetCompleteData();

            foreach (KeyValuePair<string, object> kvp in persistentData)
            {
                string prefix = "persistData-";
                string value = kvp.Value.ToString();

                if (kvp.Value.GetType() == typeof(bool))
                    prefix += "b-";
                else if (kvp.Value.GetType() == typeof(float))
                {
                    prefix += "f-";
                    value = Convert.ToSingle(kvp.Value).ToString(CultureInfo.InvariantCulture);
                }      
                else if (kvp.Value.GetType() == typeof(int))
                    prefix += "i-";

                baseData.Add(prefix + kvp.Key, value);
            }

            return baseData;
        }

        public Dictionary<string, string> StoreTimers(Dictionary<string, string> baseData)
        {
            TimersManager timersManager = UnityEngine.Object.FindObjectOfType<TimersManager>();

            if (timersManager)
            {
                foreach (Timer timer in timersManager.Timers)
                    baseData.Add("timer-" + timer.Key, timer.ToString());
            }

            return baseData;
        }

        public Dictionary<string, string> StoreQuests(Dictionary<string, string> baseData)
        {
#if UNITY_ANDROID
            string[] serialized = QuestManager.Instance.Serialize().Split("\n");
#else
            string[] serialized = QuestManager.Instance.Serialize().Split("\r\n");
#endif
            for(int i = 0; i < serialized.Length; i++)
            {
                if (serialized[i].Length > 0 && serialized[i].Contains(';'))
                {
                    string[] split = serialized[i].Split(';');
                    baseData.Add(split[0], split[1]);
                }
            }

            return baseData;
        }
    }
}
