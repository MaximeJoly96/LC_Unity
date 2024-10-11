using NUnit.Framework;
using Core;
using Inputs;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

namespace Testing.Core
{
    public class InputReceiverTests
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

        private InputController CreateInputController()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);
            return go.AddComponent<InputController>();
        }

        [UnityTest]
        public IEnumerator ProperEventsAreRaisedWhenInputsAreRecorded()
        {
            int input = -1;
            InputController inputCtrl = CreateInputController();

            GameObject go = new GameObject();
            _usedGameObjects.Add(go);
            InputReceiver receiver = go.AddComponent<InputReceiver>();

            receiver.OnMoveDown.AddListener(() => input = 0);
            receiver.OnMoveUp.AddListener(() => input = 1);
            receiver.OnMoveLeft.AddListener(() => input = 2);
            receiver.OnMoveRight.AddListener(() => input = 3);
            receiver.OnSelect.AddListener(() => input = 4);
            receiver.OnCancel.AddListener(() => input = 5);
            receiver.OnOpenMenu.AddListener(() => input = 6);

            yield return null;

            Assert.AreEqual(-1, input);
            inputCtrl.ButtonClicked.Invoke(InputAction.MoveLeft);

            yield return new WaitForSeconds(0.3f);

            Assert.AreEqual(2, input);
            inputCtrl.ButtonClicked.Invoke(InputAction.Select);

            yield return new WaitForSeconds(0.3f);

            Assert.AreEqual(4, input);
            inputCtrl.ButtonClicked.Invoke(InputAction.MoveDown);

            yield return new WaitForSeconds(0.3f);

            Assert.AreEqual(0, input);
            inputCtrl.ButtonClicked.Invoke(InputAction.MoveRight);

            yield return new WaitForSeconds(0.3f);

            Assert.AreEqual(3, input);
            inputCtrl.ButtonClicked.Invoke(InputAction.OpenMenu);

            yield return new WaitForSeconds(0.3f);

            Assert.AreEqual(6, input);
            inputCtrl.ButtonClicked.Invoke(InputAction.Cancel);

            yield return new WaitForSeconds(0.3f);

            Assert.AreEqual(5, input);
            inputCtrl.ButtonClicked.Invoke(InputAction.MoveUp);

            yield return new WaitForSeconds(0.3f);

            Assert.AreEqual(1, input);
        }
    }
}
