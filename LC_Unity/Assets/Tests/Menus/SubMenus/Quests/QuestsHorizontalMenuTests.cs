using Core;
using Menus.SubMenus.Quests;
using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using Questing;
using Inputs;
using MusicAndSounds;

namespace Testing.Menus.SubMenus.Quests
{
    public class QuestsHorizontalMenuTests : TestFoundation
    {
        private InputController _inputController;

        [SetUp]
        public void Setup()
        {
            _inputController = ComponentCreator.CreateInputController();
            _usedGameObjects.Add(_inputController.gameObject);
        }

        [UnityTest]
        public IEnumerator SelectedQuestStatusCanBeObtained()
        {
            AudioPlayer player = ComponentCreator.CreateAudioPlayer();
            _usedGameObjects.Add(player.gameObject);
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.InMenuQuestsTab);

            QuestsHorizontalMenu menu = CreateMenu();
            menu.FeedButtons(new QuestsHorizontalMenuButton[]
            {
                ComponentCreator.CreateQuestsHorizontalMenuButton(QuestStatus.Running),
                ComponentCreator.CreateQuestsHorizontalMenuButton(QuestStatus.Completed),
                ComponentCreator.CreateQuestsHorizontalMenuButton(QuestStatus.Failed),
            });

            _usedGameObjects.Add(menu.gameObject);
            foreach (var obj in menu.Buttons)
                _usedGameObjects.Add(obj.gameObject);

            yield return null;

            Assert.AreEqual(QuestStatus.Running, menu.SelectedQuestStatus);

            _inputController.ButtonClicked.Invoke(InputAction.MoveRight);
            yield return new WaitForSeconds(0.25f);

            Assert.AreEqual(QuestStatus.Completed, menu.SelectedQuestStatus);

            _inputController.ButtonClicked.Invoke(InputAction.MoveRight);
            yield return new WaitForSeconds(0.25f);

            Assert.AreEqual(QuestStatus.Failed, menu.SelectedQuestStatus);

            _inputController.ButtonClicked.Invoke(InputAction.MoveRight);
            yield return new WaitForSeconds(0.25f);

            Assert.AreEqual(QuestStatus.Running, menu.SelectedQuestStatus);

            _inputController.ButtonClicked.Invoke(InputAction.MoveLeft);
            yield return new WaitForSeconds(0.25f);

            Assert.AreEqual(QuestStatus.Failed, menu.SelectedQuestStatus);

            _inputController.ButtonClicked.Invoke(InputAction.MoveLeft);
            yield return new WaitForSeconds(0.25f);

            Assert.AreEqual(QuestStatus.Completed, menu.SelectedQuestStatus);
        }

        private QuestsHorizontalMenu CreateMenu()
        {
            GameObject go = ComponentCreator.CreateEmptyGameObject();
            _usedGameObjects.Add(go);

            QuestsHorizontalMenu menu = go.AddComponent<QuestsHorizontalMenu>();
            go.AddComponent<InputReceiver>();

            return menu;
        }
    }
}
