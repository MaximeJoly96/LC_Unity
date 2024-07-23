using UnityEngine;
using Language;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Globalization;

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
                    _saveCanvasCache = Object.FindObjectOfType<SaveCanvas>();

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

        private SaveManager() 
        {
            _creator = new SaveCreator();
            _loader = new SaveLoader();
        }

        public void OpenSaveWindow()
        {
            SaveCanvasCache.Open();
        }

        public void CloseSaveWindow()
        {
            SaveCanvasCache.Close();
        }

        public void InitSaveCreation(bool fromTitleScreen)
        {
            CurrentSaveState = SaveState.CreateSave;
            _fromTitleScreen = fromTitleScreen;

            SaveCanvasCache.UpdateTooltip(Localizer.Instance.GetString("createSaveTooltip"));
            SaveCanvasCache.Open();
            _creator.CreateSaveFile(0);
        }

        public void InitSaveCreation()
        {
            InitSaveCreation(false);
        }

        public void InitSaveLoad(bool fromTitleScreen)
        {
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
            Dictionary<string, string> saveData = _loader.LoadSaveFile(slotId);

            Data = new SavedData
            {
                PlayerPosition = new Vector2(float.Parse(saveData["positionX"], CultureInfo.InvariantCulture), 
                                             float.Parse(saveData["positionY"], CultureInfo.InvariantCulture))
            };

            CloseSaveWindow();
            SceneManager.LoadScene("Field");
        }

        public void SlotSelected(int slotId)
        {
            switch(CurrentSaveState)
            {
                case SaveState.CreateSave:
                    break;
                case SaveState.LoadSave:
                    LoadSaveFile(slotId);
                    break;
            }
        }
    }
}
