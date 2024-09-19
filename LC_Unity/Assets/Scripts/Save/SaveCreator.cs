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
            data.Add("mapId", GlobalStateMachine.Instance.CurrentMapId == -1 ? 
                              CommonLocations.INITIAL_MAP_ID.ToString(CultureInfo.InvariantCulture) :
                              GlobalStateMachine.Instance.CurrentMapId.ToString());
            data.Add("inGameTime", Object.FindObjectOfType<GlobalTimer>().InGameTimeSeconds.ToString(CultureInfo.InvariantCulture));

            List<Character> party = PartyManager.Instance.GetParty();
            List<InventoryItem> inventory = PartyManager.Instance.Inventory;
            Dictionary<string, object> persistentData = PersistentDataHolder.Instance.GetCompleteData();
            TimersManager timersManager = Object.FindObjectOfType<TimersManager>();

            foreach (Character c in party)
                data.Add("character" + c.Id, c.Serialize());

            foreach(InventoryItem i in inventory)
                data.Add("item" + i.ItemData.Id, i.Serialize());

            data.Add("gold", PartyManager.Instance.Gold.ToString());

            foreach(KeyValuePair<string, object> kvp in persistentData)
                data.Add("persistData-" +  kvp.Key, kvp.Value.ToString());

            if(timersManager)
            {
                foreach (Timer timer in timersManager.Timers)
                    data.Add("timer-" + timer.Key, timer.ToString());
            }

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
