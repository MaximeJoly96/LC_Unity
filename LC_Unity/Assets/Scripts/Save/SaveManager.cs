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
using System.Text;
using Questing;
using Save.Model;
using System.Collections;
using Engine.ScreenEffects;

namespace Save
{
    public class SaveManager
    {
        public enum SaveState { CreateSave, LoadSave, Closed }

        private const int MAX_SAVES = 7;

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

        #region Events
        private UnityEvent<SaveState> _saveStateChanged;
        public UnityEvent<SaveState> SaveStateChanged
        {
            get
            {
                if(_saveStateChanged == null)
                    _saveStateChanged = new UnityEvent<SaveState>();

                return _saveStateChanged;
            }
        }        
        #endregion
        private SaveCanvas _saveCanvasCache;
        private UnityEvent _saveCancelledEvent;
        
        private readonly SaveCreator _creator;
        private readonly SaveLoader _loader;
        private SaveScreenFader _fader;
        
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
        public SaveScreenFader Fader
        {
            get
            {
                if(!_fader)
                    _fader = GameObject.FindObjectOfType<SaveScreenFader>();

                return _fader;
            }
        }

        private SaveManager() 
        {
            _creator = new SaveCreator();
            _loader = new SaveLoader();
        }

        public void CloseSaveWindow()
        {
            SaveCanvasCache.Close();
        }

        private void OpenSaveWindow(SaveState state)
        {
            CurrentSaveState = state;
            SaveStateChanged.Invoke(CurrentSaveState);
        }

        public void InitSaveCreation()
        {
            OpenSaveWindow(SaveState.CreateSave);
        }

        public void InitSaveLoad()
        {
            OpenSaveWindow(SaveState.LoadSave);
        }

        public void FinishedClosing()
        {
            CurrentSaveState = SaveState.Closed;
            SaveCancelledEvent.Invoke();
        }

        public List<SaveDescriptor> GetSaveDescriptors()
        {
            List<SaveDescriptor> emptyData = CreateEmptyDataSlots(MAX_SAVES);
            List<SaveDescriptor> descriptorsWithData = _loader.GetSaveDescriptors();

            List<SaveDescriptor> mergedDescriptors = MergeEmptyDescriptorsWithData(emptyData, descriptorsWithData);

            return mergedDescriptors;
        }

        public List<SaveDescriptor> CreateEmptyDataSlots(int size)
        {
            List<SaveDescriptor> saves = new List<SaveDescriptor>();

            for(int i = 0; i < size; i++)
            {
                saves.Add(new SaveDescriptor(i, -1, 0.0f));
            }

            return saves;
        }

        public List<SaveDescriptor> MergeEmptyDescriptorsWithData(List<SaveDescriptor> emptyData, List<SaveDescriptor> withData)
        {
            for(int i = 0; i < emptyData.Count; i++)
            {
                if (!withData.Any(d => d.Id == emptyData[i].Id))
                    withData.Add(emptyData[i]);
            }

            return withData.OrderBy(d => d.Id).ToList();
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

                GlobalStateMachine.Instance.CurrentMapId = Data.MapID;
                GameObject.FindObjectOfType<GlobalTimer>().InitInGameTimer(Data.InGameTimeSeconds);
                GameObject.FindObjectOfType<AudioPlayer>().StopAllAudio();

                SceneManager.LoadScene("Field");
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

            StringBuilder builder = new StringBuilder();
            Dictionary<string, string> serializedQuests = saveData.Where(d => d.Key.StartsWith("quest")).ToDictionary(d => d.Key, d => d.Value);
            foreach(KeyValuePair<string,string> kvp in serializedQuests)
            {
                builder.AppendLine(kvp.Key + ";" + kvp.Value);
            }

            QuestManager.Instance.Deserialize(builder.ToString());
            QuestManager.Instance.RefreshData();

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

        public void SetPlayerPosition(Vector2 position)
        {
            if (Data == null)
                Data = new SavedData();

            Data.PlayerPosition = position;
        }
    }
}
