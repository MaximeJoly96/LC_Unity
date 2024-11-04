using UnityEngine;
using Language;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Globalization;
using Timing;
using Actors;
using Inventory;
using System.Linq;
using System;
using Logging;
using Core;
using Party;
using GameProgression;
using MusicAndSounds;

namespace Save
{
    public class SaveManager
    {
        public enum SaveState { CreateSave, LoadSave, Closed }

        private static SaveManager _instance;

        public static SaveManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SaveManager();

                return _instance;
            }
        }

        private SaveCanvas _saveCanvasCache;
        private UnityEvent _saveCancelledEvent;
        private readonly SaveCreator _creator;
        private readonly SaveLoader _loader;
        
        public SaveCanvas SaveCanvasCache
        {
            get
            {
                if (!_saveCanvasCache)
                    _saveCanvasCache = GameObject.FindObjectOfType<SaveCanvas>();

                return _saveCanvasCache;
            }
        }

        public UnityEvent SaveCancelledEvent
        {
            get
            {
                if (_saveCancelledEvent == null)
                    _saveCancelledEvent = new UnityEvent();

                return _saveCancelledEvent;
            }
        }

        public SaveState CurrentSaveState { get; private set; }
        public SavedData Data { get; private set; }
        public List<int> SavesId { get { return _loader.GetSavesId(); } }

        private SaveManager() 
        {
            _creator = new SaveCreator();
            _loader = new SaveLoader();
        }

        public void OpenSaveWindow()
        {
            SaveCanvasCache.Open();
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.SaveMenu);
        }

        public void CloseSaveWindow()
        {
            SaveCanvasCache.Close();
        }

        public void InitSaveCreation()
        {
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.SaveMenu);
            CurrentSaveState = SaveState.CreateSave;

            SaveCanvasCache.UpdateTooltip(Localizer.Instance.GetString("createSaveTooltip"));
            SaveCanvasCache.Open();
        }

        public void InitSaveLoad()
        {
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.SaveMenu);
            CurrentSaveState = SaveState.LoadSave;

            SaveCanvasCache.UpdateTooltip(Localizer.Instance.GetString("loadSaveTooltip"));
            SaveCanvasCache.Open();
        }

        public void LoadPreviousState()
        {
            SaveCancelledEvent.Invoke();
        }

        public void LoadSaveFile(int slotId)
        {
            try
            {
                PersistentDataHolder.Instance.Reset();
                Data = GetSavedDataFromSlot(slotId);

                PartyManager.Instance.SetInventory(Data.Inventory);
                PartyManager.Instance.ChangeGold(new Engine.Party.ChangeGold { Value = Data.Gold });
                PartyManager.Instance.LoadPartyFromSave(Data.Party);
            }
            catch(Exception e)
            {
                LogsHandler.Instance.LogError("Could not load save file. Reason: " + e.Message);
            }
        }

        public void CreateSaveFile(int slotId)
        {
            try
            {
                Dictionary<string, string> saveData = _creator.CreateSaveData();

                Data = new SavedData
                {
                    PlayerPosition = new Vector2(float.Parse(saveData["positionX"], CultureInfo.InvariantCulture),
                                                 float.Parse(saveData["positionY"], CultureInfo.InvariantCulture)),
                    MapID = int.Parse(saveData["mapId"], CultureInfo.InvariantCulture),
                    InGameTimeSeconds = float.Parse(saveData["inGameTime"], CultureInfo.InvariantCulture),
                    Party = RetrievePartyData(saveData),
                    Inventory = RetrieveInventoryData(saveData),
                    Gold = int.Parse(saveData["gold"])
                };

                _creator.WriteSaveDataToDisk(slotId, saveData);
            }
            catch (Exception e)
            {
                LogsHandler.Instance.LogError("Could not create save file. Reason: " + e.Message);
            }

            CloseSaveWindow();
        }

        public void SlotSelected(int slotId)
        {
            switch(CurrentSaveState)
            {
                case SaveState.CreateSave:
                    CreateSaveFile(slotId);
                    break;
                case SaveState.LoadSave:
                    LoadSaveFile(slotId);
                    break;
            }

            CloseSaveWindow();

            GlobalStateMachine.Instance.CurrentMapId = Data.MapID;
            GameObject.FindObjectOfType<GlobalTimer>().InitInGameTimer(Data.InGameTimeSeconds);
            GameObject.FindObjectOfType<AudioPlayer>().StopAllAudio();
            SceneManager.LoadScene("Field");
        }

        private List<Character> RetrievePartyData(Dictionary<string, string> saveData)
        {
            List<Character> characters = new List<Character>();

            IEnumerable<KeyValuePair<string, string>> charactersData = saveData.Where(s => s.Key.Contains("character"));
            foreach (KeyValuePair<string, string> character in charactersData)
            {
                characters.Add(Character.Deserialize(character.Key, character.Value));
            }

            return characters;
        }

        private List<InventoryItem> RetrieveInventoryData(Dictionary<string, string> saveData)
        {
            List<InventoryItem> items = new List<InventoryItem>();

            IEnumerable<KeyValuePair<string, string>> itemsData = saveData.Where(s => s.Key.Contains("item"));
            foreach(KeyValuePair<string, string> item in itemsData)
            {
                items.Add(InventoryItem.Deserialize(item.Key, item.Value));
            }

            return items;
        }

        public SavedData GetSavedDataFromSlot(int slotId)
        {
            Dictionary<string, string> saveData = _loader.LoadSaveFile(slotId);

            foreach(KeyValuePair<string, string> kvp in saveData)
            {
                if(kvp.Key.StartsWith("persistData-b-"))
                {
                    string key = kvp.Key.Replace("persistData-b-", "");
                    PersistentDataHolder.Instance.StoreData(key, bool.Parse(kvp.Value));
                }
                else if(kvp.Key.StartsWith("persistData-i-"))
                {
                    string key = kvp.Key.Replace("persistData-i-", "");
                    PersistentDataHolder.Instance.StoreData(key, int.Parse(kvp.Value));
                }
                else if(kvp.Key.StartsWith("persistData-f-"))
                {
                    string key = kvp.Key.Replace("persistData-f-", "");
                    PersistentDataHolder.Instance.StoreData(key, float.Parse(kvp.Value, CultureInfo.InvariantCulture));
                }
            }

            return new SavedData
            {
                PlayerPosition = new Vector2(float.Parse(saveData["positionX"], CultureInfo.InvariantCulture),
                                                 float.Parse(saveData["positionY"], CultureInfo.InvariantCulture)),
                MapID = int.Parse(saveData["mapId"], CultureInfo.InvariantCulture),
                InGameTimeSeconds = float.Parse(saveData["inGameTime"], CultureInfo.InvariantCulture),
                Party = RetrievePartyData(saveData),
                Inventory = RetrieveInventoryData(saveData),
                Gold = int.Parse(saveData["gold"])
            };
        }
    }
}
