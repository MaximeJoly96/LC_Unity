using UnityEngine;
using UnityEngine.UI;
using NUnit.Framework;
using TMPro;
using Save;
using System.Collections.Generic;
using Core;
using GameProgression;
using Party;
using Actors;
using Inventory;
using Language;
using Save.Model;

namespace Testing.Save
{
    public class SaveSlotTests : TestFoundation
    {
        [SetUp]
        public void Setup()
        {
            PersistentDataHolder.Instance.Reset();
            PartyManager.Instance.Clear();
            GlobalStateMachine.Instance.CurrentMapId = -1;
        }

        [Test]
        public void SlotCanBeInited()
        {
            Localizer localizer = ComponentCreator.CreateLocalizer("Save/french.csv", global::Language.Language.French);
            _usedGameObjects.Add(localizer.gameObject);

            SaveDescriptor descriptor = new SaveDescriptor(1, 1000, 10.0f);

            SaveSlot slot = ComponentCreator.CreateSaveSlot();
            _usedGameObjects.Add(slot.gameObject);

            slot.Init(descriptor);

            Assert.AreEqual("Introduction", slot.Label.text);
            Assert.AreEqual("00:00:10", slot.InGameTime.text);
            Assert.AreEqual("1", slot.SlotIdLabel.text);
        }
    }
}
