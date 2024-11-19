using UnityEngine;
using TMPro;
using Inputs;
using System.Collections.Generic;
using UnityEngine.UI;
using MsgBox;
using Language;
using Core;
using Utils;

namespace Save
{
    public class SaveCanvas : MonoBehaviour
    {
        private const int MAX_SAVES = 15;

        [SerializeField]
        private TMP_Text _tooltip;
        [SerializeField]
        private SelectableSavesList _savesList;

        private SaveManager _saveManager;

        public CanvasGroup CanvasGroup { get { return GetComponent<CanvasGroup>(); } }
        public Animator Animator { get { return GetComponent<Animator>(); } }
        public TMP_Text Tooltip { get { return _tooltip; } set { _tooltip = value; } }
        public SelectableSavesList SavesList { get { return _savesList; } set { _savesList = value; } }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            _saveManager = SaveManager.Instance;
        }

        private void Start()
        {
            _saveManager.SaveStateChanged.AddListener(SaveStateChanged);
            _saveManager.SaveStateChanged.AddListener(UpdateTooltip);

            _savesList.Init();
            _savesList.SelectionCancelled.AddListener(SelectionCancelled);
        }

        private void SaveStateChanged(SaveManager.SaveState state)
        {
            switch(state)
            {
                case SaveManager.SaveState.LoadSave:
                case SaveManager.SaveState.CreateSave:
                    GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.OpeningSaves);
                    Open();
                    break;
            }
        }

        public void Close()
        {
            if(Animator)
                Animator.Play("CloseSaveWindow");
        }

        public void FinishedClosing()
        {
            SaveManager.Instance.FinishedClosing();
        }

        public void FinishedOpening()
        {
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.BrowsingSaves);
        }

        public void Open()
        {
            if(Animator)
                Animator.Play("OpenSaveWindow");
        }

        public void UpdateTooltip(SaveManager.SaveState state)
        {
            string key = string.Empty;

            switch(state)
            {
                case SaveManager.SaveState.LoadSave:
                    key = "loadSaveTooltip";
                    break;
                case SaveManager.SaveState.CreateSave:
                    key = "createSaveTooltip";
                    break;
            }

            if(!string.IsNullOrEmpty(key))
                Tooltip.text = Localizer.Instance.GetString(key);
        }

        private void SelectionCancelled()
        {
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.ClosingSaves);
            Close();
        }

        private void ProceedWithSaveCreation(MessageBoxAnswer result)
        {
            /*if (result == MessageBoxAnswer.Yes)
                SaveManager.Instance.SlotSelected(_cursorPosition);
            else
                GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.SaveMenu);*/
        }
    }
}
