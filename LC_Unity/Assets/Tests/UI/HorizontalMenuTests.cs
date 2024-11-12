using NUnit.Framework;
using UnityEngine;
using UI;
using Core;
using System.Collections;
using UnityEngine.TestTools;
using System.Linq;
using Inputs;

namespace Testing.UI
{
    public class HorizontalMenuTests : TestFoundation
    {
        private InputController _inputController;

        [SetUp]
        public void Setup()
        {
            _inputController = ComponentCreator.CreateInputController();
            _usedGameObjects.Add(_inputController.gameObject);
        }

        [UnityTest]
        public IEnumerator MenuIsInitedProperly()
        {
            HorizontalMenu menu = CreateMenu();

            // We need to wait 2 frames here because the Animator component only plays the animation after the first frame
            // and the animation state only updates at the end of the frame, so the result can only be checked during the next one.
            yield return null;
            yield return null;

            Assert.AreEqual(0, menu.CursorPosition);

            CheckButtonsAnimation(menu.Buttons, 0);
        }

        [UnityTest]
        public IEnumerator CursorCanBeMoved()
        {
            ComponentCreator.CreateAudioPlayer();
            HorizontalMenu menu = CreateMenu();

            yield return null;

            menu.Init();

            yield return null;
            yield return null;

            _inputController.ButtonClicked.Invoke(InputAction.MoveRight);

            yield return new WaitForSeconds(0.25f);

            Assert.AreEqual(1, menu.CursorPosition);
            CheckButtonsAnimation(menu.Buttons, 1);

            yield return null;

            _inputController.ButtonClicked.Invoke(InputAction.MoveRight);

            yield return new WaitForSeconds(0.25f);

            Assert.AreEqual(2, menu.CursorPosition);
            CheckButtonsAnimation(menu.Buttons, 2);

            yield return null;

            _inputController.ButtonClicked.Invoke(InputAction.MoveRight);

            yield return new WaitForSeconds(0.25f);

            Assert.AreEqual(0, menu.CursorPosition);
            CheckButtonsAnimation(menu.Buttons, 0);

            yield return null;

            _inputController.ButtonClicked.Invoke(InputAction.MoveRight);

            yield return new WaitForSeconds(0.25f);

            Assert.AreEqual(1, menu.CursorPosition);
            CheckButtonsAnimation(menu.Buttons, 1);

            yield return null;

            _inputController.ButtonClicked.Invoke(InputAction.MoveLeft);

            yield return new WaitForSeconds(0.25f);

            Assert.AreEqual(0, menu.CursorPosition);
            CheckButtonsAnimation(menu.Buttons, 0);

            yield return null;

            _inputController.ButtonClicked.Invoke(InputAction.MoveLeft);

            yield return new WaitForSeconds(0.25f);

            Assert.AreEqual(2, menu.CursorPosition);
            CheckButtonsAnimation(menu.Buttons, 2);
        }

        private void CheckButtonsAnimation(HorizontalMenuButton[] buttons, int selectedButtonIndex)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                Animator animator = buttons[i].GetComponent<Animator>();
                AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

                AnimationClip clip = animator.runtimeAnimatorController
                                             .animationClips
                                             .FirstOrDefault(c => c.name.Contains(i == selectedButtonIndex ? "Hover" : "Idle"));

                Assert.AreEqual(Animator.StringToHash(clip.name), stateInfo.shortNameHash);
            }
        }

        private HorizontalMenu CreateMenu()
        {
            GameObject go = ComponentCreator.CreateEmptyGameObject();
            _usedGameObjects.Add(go);

            HorizontalMenu menu = go.AddComponent<HorizontalMenu>();
            go.AddComponent<InputReceiver>();

            HorizontalMenuButton[] buttons = new HorizontalMenuButton[]
            {
                ComponentCreator.CreateHorizontalMenuButton(),
                ComponentCreator.CreateHorizontalMenuButton(),
                ComponentCreator.CreateHorizontalMenuButton()
            };

            foreach (HorizontalMenuButton but in buttons)
                _usedGameObjects.Add(but.gameObject);

            menu.FeedButtons(buttons);

            return menu;
        }
    }
}
