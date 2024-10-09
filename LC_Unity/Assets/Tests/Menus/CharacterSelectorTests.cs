using Menus;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Language;
using UnityEditor;
using UnityEngine.TestTools;
using System.Collections;
using Actors;
using Core.Model;
using Utils;
using Inputs;
using Core;
using MusicAndSounds;

namespace Testing.Menus
{
    public class CharacterSelectorTests
    {
        private List<GameObject> _usedGameObjects;

        [TearDown]
        public void TearDown()
        {
            for (int i = 0; i < _usedGameObjects.Count; i++)
            {
                GameObject.Destroy(_usedGameObjects[i]);
            }
        }

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            _usedGameObjects = new List<GameObject>();
        }

        [UnityTest]
        public IEnumerator PreviewsCanBeFed()
        {
            CreateInputController();
            CreateFrenchLocalizer();
            CharacterSelector selector = CreateDefaultSelector();

            yield return null;

            List<Character> characters = new List<Character>
            {
                CreateLougaCharacter(),
                CreateAgridCharacter()
            };

            selector.SetPreviewPrefab(CreateDefaultPreview());
            selector.Feed(characters);

            Assert.AreEqual(2, selector.Previews.Count);
            Assert.AreEqual("Louga", selector.Previews[0].Name.text);
            Assert.AreEqual("Agrid", selector.Previews[1].Name.text);
        }

        [UnityTest]
        public IEnumerator ChildrenObjectsCanBeCleared()
        {
            CreateInputController();
            CreateFrenchLocalizer();
            CharacterSelector selector = CreateDefaultSelector();

            yield return null;

            List<Character> characters = new List<Character>
            {
                CreateLougaCharacter(),
                CreateAgridCharacter()
            };

            selector.SetPreviewPrefab(CreateDefaultPreview());
            selector.Feed(characters);

            yield return null;

            selector.Clear();

            yield return null;

            Assert.AreEqual(0, selector.transform.childCount);
            Assert.AreEqual(0, selector.Previews.Count);
        }

        [UnityTest]
        public IEnumerator CursorMovingAccordinglyToInputs()
        {
            InputController inputCtrl = CreateInputController();
            CreateFrenchLocalizer();
            CharacterSelector selector = CreateDefaultSelector();
            CreateAudioPlayer();

            yield return null;

            // We create more characters on purpose to handle cursor movement. Also having an odd number is more interesting
            List<Character> characters = new List<Character>
            {
                CreateLougaCharacter(),
                CreateAgridCharacter(),
                CreateAgridCharacter(),
                CreateAgridCharacter(),
                CreateAgridCharacter(),
                CreateAgridCharacter(),
                CreateAgridCharacter()
            };

            selector.SetPreviewPrefab(CreateDefaultPreview());
            selector.Feed(characters);
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.SelectingCharacterPreview);

            yield return null;

            Assert.AreEqual(0, selector.CursorPosition);
            inputCtrl.ButtonClicked.Invoke(InputAction.MoveRight);

            yield return new WaitForSeconds(0.5f);

            Assert.AreEqual(1, selector.CursorPosition);
            inputCtrl.ButtonClicked.Invoke(InputAction.MoveRight);

            yield return new WaitForSeconds(0.5f);

            Assert.AreEqual(2, selector.CursorPosition);
            inputCtrl.ButtonClicked.Invoke(InputAction.MoveLeft);

            yield return new WaitForSeconds(0.5f);

            Assert.AreEqual(1, selector.CursorPosition);
            inputCtrl.ButtonClicked.Invoke(InputAction.MoveDown);

            yield return new WaitForSeconds(0.5f);

            Assert.AreEqual(3, selector.CursorPosition);
            inputCtrl.ButtonClicked.Invoke(InputAction.MoveDown);

            yield return new WaitForSeconds(0.5f);

            Assert.AreEqual(5, selector.CursorPosition);
            inputCtrl.ButtonClicked.Invoke(InputAction.MoveUp);

            yield return new WaitForSeconds(0.5f);

            Assert.AreEqual(3, selector.CursorPosition);
            inputCtrl.ButtonClicked.Invoke(InputAction.MoveUp);

            yield return new WaitForSeconds(0.5f);

            Assert.AreEqual(1, selector.CursorPosition);
            inputCtrl.ButtonClicked.Invoke(InputAction.MoveUp);

            yield return new WaitForSeconds(0.5f);

            Assert.AreEqual(1, selector.CursorPosition);
            inputCtrl.ButtonClicked.Invoke(InputAction.MoveLeft);

            yield return new WaitForSeconds(0.5f);

            Assert.AreEqual(0, selector.CursorPosition);
            inputCtrl.ButtonClicked.Invoke(InputAction.MoveLeft);

            yield return new WaitForSeconds(0.5f);

            Assert.AreEqual(0, selector.CursorPosition);
            inputCtrl.ButtonClicked.Invoke(InputAction.MoveDown);

            yield return new WaitForSeconds(0.5f);

            Assert.AreEqual(2, selector.CursorPosition);
            inputCtrl.ButtonClicked.Invoke(InputAction.MoveDown);

            yield return new WaitForSeconds(0.5f);

            Assert.AreEqual(4, selector.CursorPosition);
            inputCtrl.ButtonClicked.Invoke(InputAction.MoveDown);

            yield return new WaitForSeconds(0.5f);

            Assert.AreEqual(6, selector.CursorPosition);
            inputCtrl.ButtonClicked.Invoke(InputAction.MoveDown);

            yield return new WaitForSeconds(0.5f);

            Assert.AreEqual(6, selector.CursorPosition);
            inputCtrl.ButtonClicked.Invoke(InputAction.MoveRight);

            yield return new WaitForSeconds(0.5f);

            Assert.AreEqual(6, selector.CursorPosition);
            inputCtrl.ButtonClicked.Invoke(InputAction.MoveLeft);

            yield return new WaitForSeconds(0.5f);

            Assert.AreEqual(5, selector.CursorPosition);
        }

        [UnityTest]
        public IEnumerator CursorIsSetToInitialPositionWhenHoveringAllCharacters()
        {
            InputController inputCtrl = CreateInputController();
            CreateFrenchLocalizer();
            CharacterSelector selector = CreateDefaultSelector();
            CreateAudioPlayer();

            yield return null;

            List<Character> characters = new List<Character>
            {
                CreateLougaCharacter(),
                CreateAgridCharacter()
            };

            selector.SetPreviewPrefab(CreateDefaultPreview());
            selector.Feed(characters);

            yield return null;

            selector.HoverCharacters();

            Assert.AreEqual(0, selector.CursorPosition);
        }

        [UnityTest]
        public IEnumerator StateMachineIsUpdatedWhenPressingCancel()
        {
            InputController inputCtrl = CreateInputController();
            CreateFrenchLocalizer();
            CharacterSelector selector = CreateDefaultSelector();
            CreateAudioPlayer();

            yield return null;

            List<Character> characters = new List<Character>
            {
                CreateLougaCharacter(),
                CreateAgridCharacter()
            };

            selector.SetPreviewPrefab(CreateDefaultPreview());
            selector.Feed(characters);
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.SelectingCharacterPreview);

            yield return null;

            inputCtrl.ButtonClicked.Invoke(InputAction.Cancel);

            yield return new WaitForSeconds(0.5f);

            Assert.AreEqual(GlobalStateMachine.State.InMenu, GlobalStateMachine.Instance.CurrentState);
        }

        [UnityTest]
        public IEnumerator SelectedCharacterIsStoredInRaisedEvent()
        {
            InputController inputCtrl = CreateInputController();
            CreateFrenchLocalizer();
            CharacterSelector selector = CreateDefaultSelector();
            selector.CharacterSelected.AddListener((c) => Assert.AreEqual("Agrid", c.Name));
            CreateAudioPlayer();

            yield return null;

            List<Character> characters = new List<Character>
            {
                CreateLougaCharacter(),
                CreateAgridCharacter()
            };

            selector.SetPreviewPrefab(CreateDefaultPreview());
            selector.Feed(characters);
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.SelectingCharacterPreview);

            yield return null;

            Assert.AreEqual(0, selector.CursorPosition);
            inputCtrl.ButtonClicked.Invoke(InputAction.MoveRight);

            yield return new WaitForSeconds(0.5f);

            Assert.AreEqual(1, selector.CursorPosition);
            inputCtrl.ButtonClicked.Invoke(InputAction.Select);
        }

        private CharacterSelector CreateDefaultSelector()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);
            return go.AddComponent<CharacterSelector>();
        }

        private CharacterPreview CreateDefaultPreview()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);
            CharacterPreview preview = go.AddComponent<CharacterPreview>();

            GameObject faceSetGo = new GameObject();
            _usedGameObjects.Add(faceSetGo);
            Image faceset = faceSetGo.AddComponent<Image>();
            faceSetGo.transform.SetParent(go.transform);

            GameObject nameGo = new GameObject();
            _usedGameObjects.Add(nameGo);
            TextMeshProUGUI nameText = nameGo.AddComponent<TextMeshProUGUI>();
            nameGo.transform.SetParent(go.transform);

            StatGauge hpGauge = CreateDefaultStatGauge();
            hpGauge.transform.SetParent(go.transform);
            StatGauge manaGauge = CreateDefaultStatGauge();
            manaGauge.transform.SetParent(go.transform);
            StatGauge essenceGauge = CreateDefaultStatGauge();
            essenceGauge.transform.SetParent(go.transform);
            XpGauge xpGauge = CreateDefaultXpGauge();
            xpGauge.transform.SetParent(go.transform);

            preview.Init();

            return preview;
        }

        private XpGauge CreateDefaultXpGauge()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);

            GameObject childImg = new GameObject();
            childImg.AddComponent<Image>();
            childImg.transform.SetParent(go.transform);
            GameObject childText = new GameObject();
            TextMeshProUGUI valueTxt = childText.AddComponent<TextMeshProUGUI>();
            childText.transform.SetParent(go.transform);
            GameObject childTextLvl = new GameObject();
            TextMeshProUGUI levelTxt = childTextLvl.AddComponent<TextMeshProUGUI>();
            childTextLvl.transform.SetParent(go.transform);

            _usedGameObjects.Add(childImg);
            _usedGameObjects.Add(childText);
            _usedGameObjects.Add(childTextLvl);

            XpGauge gauge = go.AddComponent<XpGauge>();
            gauge.Init(childImg.GetComponent<Image>(), valueTxt, levelTxt);

            return gauge;
        }

        private StatGauge CreateDefaultStatGauge()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);

            GameObject childImg = new GameObject();
            childImg.AddComponent<Image>();
            childImg.transform.SetParent(go.transform);
            GameObject childText = new GameObject();
            childText.AddComponent<TextMeshProUGUI>();
            childText.transform.SetParent(go.transform);

            _usedGameObjects.Add(childImg);
            _usedGameObjects.Add(childText);

            StatGauge gauge = go.AddComponent<StatGauge>();
            gauge.Init(childImg.GetComponent<Image>(), childText.GetComponent<TextMeshProUGUI>());

            return gauge;
        }

        private Localizer CreateFrenchLocalizer()
        {
            GameObject localizer = new GameObject("Localizer");
            _usedGameObjects.Add(localizer);
            Localizer component = localizer.AddComponent<Localizer>();

            TextAsset file = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Menus/french.csv");
            component.LoadLanguage(global::Language.Language.French, file);

            return component;
        }

        private Character CreateLougaCharacter()
        {
            return new Character(new ElementIdentifier(0, "Louga", ""),
                                 new QuadraticFunction(450, 250, 10),
                                 new StatScalingFunction(22, 1.2f, 256),
                                 new StatScalingFunction(6, 0.8f, 25),
                                 new StatScalingFunction(0, 1.0f, 100),
                                 new StatScalingFunction(1.5f, 1.2f, 8),
                                 new StatScalingFunction(1.4f, 1.1f, 7),
                                 new StatScalingFunction(0.9f, 1.2f, 3),
                                 new StatScalingFunction(1.2f, 1.2f, 8),
                                 new StatScalingFunction(1.7f, 0.975f, 5),
                                 new StatScalingFunction(4, 0.85f, 6));
        }

        private Character CreateAgridCharacter()
        {
            return new Character(new ElementIdentifier(1, "Agrid", ""),
                                 new QuadraticFunction(450, 250, 10),
                                 new StatScalingFunction(9, 1.3f, 188),
                                 new StatScalingFunction(8, 1.1f, 48),
                                 new StatScalingFunction(0, 1.0f, 100),
                                 new StatScalingFunction(1.2f, 1.1f, 3),
                                 new StatScalingFunction(0.75f, 1.06f, 5),
                                 new StatScalingFunction(2.1f, 1.18f, 8.7f),
                                 new StatScalingFunction(1.8f, 1.12f, 6),
                                 new StatScalingFunction(1.3f, 1.1f, 7.4f),
                                 new StatScalingFunction(3, 0.9f, 4.5f));
        }

        private InputController CreateInputController()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);
            return go.AddComponent<InputController>();
        }

        private AudioPlayer CreateAudioPlayer()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);
            return go.AddComponent<AudioPlayer>();
        }
    }
}
