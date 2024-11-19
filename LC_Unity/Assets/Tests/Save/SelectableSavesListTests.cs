using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using System.Collections.Generic;
using Save;
using Save.Model;
using Core;
using MusicAndSounds;
using Inputs;
using Language;

namespace Testing.Save
{
    public class SelectableSavesListTests : TestFoundation
    {
        private InputController _inputController;

        private SelectableSavesList CreateList()
        {
            AudioPlayer player = ComponentCreator.CreateAudioPlayer();
            _usedGameObjects.Add(player.gameObject);

            GameObject go = ComponentCreator.CreateEmptyGameObject();
            SelectableSavesList list = go.AddComponent<SelectableSavesList>();
            list.SetItemPrefab(ComponentCreator.CreateSaveSlot());

            go.AddComponent<InputReceiver>();

            _usedGameObjects.Add(go);

            return list;
        }

        [SetUp]
        public void Setup()
        {
            _inputController = ComponentCreator.CreateInputController();
            _usedGameObjects.Add(_inputController.gameObject);
        }

        [UnityTest]
        public IEnumerator ListCanBeFedWithSaves()
        {
            Localizer localizer = ComponentCreator.CreateLocalizer("Save/french.csv", global::Language.Language.French);
            _usedGameObjects.Add(localizer.gameObject);

            SelectableSavesList list = CreateList();

            List<SaveDescriptor> saveDescriptors = new List<SaveDescriptor>
            {
                new SaveDescriptor(0, 1000, 59.3f),
                new SaveDescriptor(1, 0, 1407.2f),
                new SaveDescriptor(2, 2, 659.4f),
                new SaveDescriptor(3, 3, 358.7f),
                new SaveDescriptor(4, 4, 12485.66f),
            };

            yield return null;

            list.FeedSaves(saveDescriptors);

            yield return null;

            Assert.AreEqual(5, list.CreatedItems.Count);

            SaveSlot slot0 = list.CreatedItems[0] as SaveSlot;
            Assert.AreEqual("0", slot0.SlotIdLabel.text);
            Assert.AreEqual("Introduction", slot0.Label.text);
            Assert.AreEqual("00:00:59", slot0.InGameTime.text);

            SaveSlot slot1 = list.CreatedItems[1] as SaveSlot;
            Assert.AreEqual("1", slot1.SlotIdLabel.text);
            Assert.AreEqual("Bastion de Haalmikah", slot1.Label.text);
            Assert.AreEqual("00:23:27", slot1.InGameTime.text);

            SaveSlot slot2 = list.CreatedItems[2] as SaveSlot;
            Assert.AreEqual("2", slot2.SlotIdLabel.text);
            Assert.AreEqual("Région de Haalmikah - Ouest", slot2.Label.text);
            Assert.AreEqual("00:10:59", slot2.InGameTime.text);

            SaveSlot slot3 = list.CreatedItems[3] as SaveSlot;
            Assert.AreEqual("3", slot3.SlotIdLabel.text);
            Assert.AreEqual("Région de Haalmikah - Bosquet", slot3.Label.text);
            Assert.AreEqual("00:05:58", slot3.InGameTime.text);

            SaveSlot slot4 = list.CreatedItems[4] as SaveSlot;
            Assert.AreEqual("4", slot4.SlotIdLabel.text);
            Assert.AreEqual("Baie de Gornich - Rive ouest", slot4.Label.text);
            Assert.AreEqual("03:28:05", slot4.InGameTime.text);
        }
    }
}
