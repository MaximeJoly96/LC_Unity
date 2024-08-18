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
        private bool _fromTitleScreen;
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

        public void InitSaveCreation(bool fromTitleScreen)
        {
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.SaveMenu);
            CurrentSaveState = SaveState.CreateSave;
            _fromTitleScreen = fromTitleScreen;

            SaveCanvasCache.UpdateTooltip(Localizer.Instance.GetString("createSaveTooltip"));
            SaveCanvasCache.Open();
        }

        public void InitSaveCreation()
        {
            InitSaveCreation(false);
        }

        public void InitSaveLoad(bool fromTitleScreen)
        {
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.SaveMenu);
            CurrentSaveState = SaveState.LoadSave;
            _fromTitleScreen = fromTitleScreen;

            SaveCanvasCache.UpdateTooltip(Localizer.Instance.GetString("loadSaveTooltip"));
            SaveCanvasCache.Open();
        }

        public void InitSaveLoad()
        {
            InitSaveLoad(false);
        }

        public void LoadPreviousState()
        {
            if(_fromTitleScreen)
            {
                SaveCancelledEvent.Invoke();
            }
        }

        public void LoadSaveFile(int slotId)
        {
            try
            {
                Data = GetSavedDataFromSlot(slotId);
                GlobalStateMachine.Instance.CurrentMapId = Data.MapID;
                PartyManager.Instance.SetInventory(Data.Inventory);
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
                Dictionary<string, string> saveData = _creator.CreateSaveFile(slotId);

                Data = new SavedData
                {
                    PlayerPosition = new Vector2(float.Parse(saveData["positionX"], CultureInfo.InvariantCulture),
                                                 float.Parse(saveData["positionY"], CultureInfo.InvariantCulture)),
                    MapID = int.Parse(saveData["mapId"], CultureInfo.InvariantCulture),
                    InGameTimeSeconds = float.Parse(saveData["inGameTime"], CultureInfo.InvariantCulture),
                    Party = RetrievePartyData(saveData),
                    Inventory = RetrieveInventoryData(saveData)
                };
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
            GameObject.FindObjectOfType<GlobalTimer>().InitInGameTimer(Data.InGameTimeSeconds);
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

        private List<InventoryItem>  RetrieveInventoryData(Dictionary<string, string> saveData)
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

            return new SavedData
            {
                PlayerPosition = new Vector2(float.Parse(saveData["positionX"], CultureInfo.InvariantCulture),
                                                 float.Parse(saveData["positionY"], CultureInfo.InvariantCulture)),
                MapID = int.Parse(saveData["mapId"], CultureInfo.InvariantCulture),
                InGameTimeSeconds = float.Parse(saveData["inGameTime"], CultureInfo.InvariantCulture),
                Party = RetrievePartyData(saveData),
                Inventory = RetrieveInventoryData(saveData)
            };
        }
    }
}
