using UnityEngine;
using NUnit.Framework;
using UI;
using System.Collections;
using UnityEngine.TestTools;
using Core;
using Inputs;
using MusicAndSounds;

namespace Testing.UI
{
    public class SelectableListTests : TestFoundation
    {
        private InputController _inputController;

        [SetUp]
        public void Setup()
        {
            _inputController = ComponentCreator.CreateInputController();
            _usedGameObjects.Add(_inputController.gameObject);
        }

        [UnityTest]
        public IEnumerator CursorCanBeMoved()
        {
            AudioPlayer player = ComponentCreator.CreateAudioPlayer();
            _usedGameObjects.Add(player.gameObject);
            SelectableList list = CreateEmptyList();
            list.Init();

            yield return null;

            list.AddItem();
            list.AddItem();
            list.AddItem();
            list.AddItem();
            list.AddItem();

            yield return null;

            list.HoverFirstItem();

            yield return null;

            Assert.AreEqual(0, list.CursorPosition);
            AssertCursorStatus(list);

            _inputController.ButtonClicked.Invoke(InputAction.MoveDown);
            yield return new WaitForSeconds(0.25f);

            Assert.AreEqual(1, list.CursorPosition);
            AssertCursorStatus(list);

            _inputController.ButtonClicked.Invoke(InputAction.MoveDown);
            yield return new WaitForSeconds(0.25f);

            Assert.AreEqual(2, list.CursorPosition);
            AssertCursorStatus(list);

            _inputController.ButtonClicked.Invoke(InputAction.MoveDown);
            yield return new WaitForSeconds(0.25f);

            Assert.AreEqual(3, list.CursorPosition);
            AssertCursorStatus(list);

            _inputController.ButtonClicked.Invoke(InputAction.MoveDown);
            yield return new WaitForSeconds(0.25f);

            Assert.AreEqual(4, list.CursorPosition);
            AssertCursorStatus(list);

            _inputController.ButtonClicked.Invoke(InputAction.MoveDown);
            yield return new WaitForSeconds(0.25f);

            Assert.AreEqual(0, list.CursorPosition);
            AssertCursorStatus(list);

            _inputController.ButtonClicked.Invoke(InputAction.MoveUp);
            yield return new WaitForSeconds(0.25f);

            Assert.AreEqual(4, list.CursorPosition);
            AssertCursorStatus(list);

            _inputController.ButtonClicked.Invoke(InputAction.MoveUp);
            yield return new WaitForSeconds(0.25f);

            Assert.AreEqual(3, list.CursorPosition);
            AssertCursorStatus(list);
        }

        [UnityTest]
        public IEnumerator ItemCanBeAddedToTheList()
        {
            SelectableList list = CreateEmptyList();
            list.Init();

            yield return null;

            list.AddItem();

            yield return null;

            Assert.AreEqual(1, list.CreatedItems.Count);
            Assert.IsFalse(list.CreatedItems[0].Cursor.gameObject.activeInHierarchy);
        }

        [UnityTest]
        public IEnumerator FirstItemCanBeHovered()
        {
            AudioPlayer player = ComponentCreator.CreateAudioPlayer();
            _usedGameObjects.Add(player.gameObject);
            SelectableList list = CreateEmptyList();
            list.Init();

            yield return null;

            list.AddItem();

            yield return null;

            list.HoverFirstItem();

            yield return null;

            Assert.IsTrue(list.CreatedItems[0].Cursor.gameObject.activeInHierarchy);
        }

        private void AssertCursorStatus(SelectableList list)
        {
            for(int i = 0; i < list.CreatedItems.Count; i++)
            {
                Assert.AreEqual(i == list.CursorPosition, list.CreatedItems[i].Cursor.gameObject.activeInHierarchy);
            }
        }

        private SelectableList CreateEmptyList()
        {
            GameObject go = ComponentCreator.CreateEmptyGameObject();
            _usedGameObjects.Add(go);

            SelectableList list = go.AddComponent<SelectableList>();
            SelectableItem item = ComponentCreator.CreateSelectableItem();
            _usedGameObjects.Add(item.gameObject);
            _usedGameObjects.Add(item.Cursor.gameObject);

            list.SetItemPrefab(item);
            list.gameObject.AddComponent<InputReceiver>();

            return list;
        }
    }
}
