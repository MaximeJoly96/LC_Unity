using UnityEngine;
using Language;
using UnityEngine.Events;

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

        private SaveManager() { }

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
    }
}
