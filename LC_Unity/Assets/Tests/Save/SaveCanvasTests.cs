using NUnit.Framework;
using Save;
using UnityEngine;
using TMPro;
using Core;
using MusicAndSounds;
using System.Collections;
using UnityEngine.TestTools;
using Inputs;
using Language;

namespace Testing.Save
{
    public class SaveCanvasTests : TestFoundation
    {
        private InputController _inputController;

        private SaveCanvas CreateSaveCanvas()
        {
            AudioPlayer player = ComponentCreator.CreateAudioPlayer();
            GameObject go = ComponentCreator.CreateEmptyGameObject();

            SaveCanvas canvas = go.AddComponent<SaveCanvas>();
            TextMeshProUGUI tooltip = ComponentCreator.CreateText();
            tooltip.transform.SetParent(canvas.transform);
            canvas.Tooltip = tooltip;

            GameObject savesListGo = ComponentCreator.CreateEmptyGameObject();
            SelectableSavesList savesList = savesListGo.AddComponent<SelectableSavesList>();
            savesList.transform.SetParent(canvas.transform);
            canvas.SavesList = savesList;

            savesList.gameObject.AddComponent<InputReceiver>();

            Animator animator = go.AddComponent<Animator>();
            animator.runtimeAnimatorController = ComponentCreator.CreateAnimatorController("Save/TestAnimations/SaveCanvas/SaveCanvasController.controller");

            _usedGameObjects.Add(canvas.gameObject);
            _usedGameObjects.Add(player.gameObject);

            return canvas;
        }

        [SetUp]
        public void Setup()
        {
            SaveManager.Instance.SaveStateChanged.RemoveAllListeners();
            _inputController = ComponentCreator.CreateInputController();
            _usedGameObjects.Add(_inputController.gameObject);

            Localizer localizer = ComponentCreator.CreateLocalizer("Save/french.csv", global::Language.Language.French);
            _usedGameObjects.Add(localizer.gameObject);
        }

        [UnityTest]
        public IEnumerator SaveStateChangesAreDetected()
        {
            SaveCanvas canvas = CreateSaveCanvas();
            SaveManager manager = SaveManager.Instance;

            yield return null;

            manager.InitSaveCreation();
            Assert.AreEqual(GlobalStateMachine.State.OpeningSaves, GlobalStateMachine.Instance.CurrentState);

            manager.InitSaveLoad();
            Assert.AreEqual(GlobalStateMachine.State.OpeningSaves, GlobalStateMachine.Instance.CurrentState);
        }

        [UnityTest]
        public IEnumerator TooltipIsUpdatedWhenSaveStateChanges()
        {
            SaveCanvas canvas = CreateSaveCanvas();
            SaveManager manager = SaveManager.Instance;

            yield return null;

            manager.InitSaveCreation();
            Assert.AreEqual("Sélectionnez un emplacement pour la sauvegarde.", canvas.Tooltip.text);

            manager.InitSaveLoad();
            Assert.AreEqual("Sélectionnez la sauvegarde à charger.", canvas.Tooltip.text);
        }

        [UnityTest]
        public IEnumerator GlobalStateIsUpdatedAfterOpening()
        {
            SaveCanvas canvas = CreateSaveCanvas();
            SaveManager manager = SaveManager.Instance;

            yield return null;

            manager.InitSaveCreation();

            yield return new WaitForSeconds(1.0f);

            Assert.AreEqual(GlobalStateMachine.State.BrowsingSaves, GlobalStateMachine.Instance.CurrentState);
        }

        [UnityTest]
        public IEnumerator CancelEventIsDetected()
        {
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.TitleScreen);

            SaveCanvas canvas = CreateSaveCanvas();
            SaveManager manager = SaveManager.Instance;

            yield return null;

            manager.InitSaveCreation();

            yield return new WaitForSeconds(1.0f);

            _inputController.ButtonClicked.Invoke(InputAction.Cancel);

            Assert.AreEqual(GlobalStateMachine.State.ClosingSaves, GlobalStateMachine.Instance.CurrentState);
            
            yield return new WaitForSeconds(1.0f);

            Assert.AreEqual(GlobalStateMachine.State.TitleScreen, GlobalStateMachine.Instance.CurrentState);
            Assert.AreEqual(SaveManager.SaveState.Closed, manager.CurrentSaveState);
        }
    }
}
