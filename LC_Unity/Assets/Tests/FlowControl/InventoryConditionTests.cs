﻿using NUnit.Framework;
using Testing.Engine;
using Engine.FlowControl;
using GameProgression;
using UnityEngine;
using Engine.Events;
using Party;
using Inventory;
using Engine.Party;
using System.Linq;
using Actors;

namespace Testing.FlowControl
{
    public class InventoryConditionTests : XmlBaseParser
    {
        protected override string TestFilePath { get { return "Assets/Tests/FlowControl/InventoryConditionTests.xml"; } }

        [Test]
        public void RunItemPossessedConditionTest()
        {
            ItemPossessed itemPossessed = XmlFlowControlParser.ParseInventoryCondition(GetDataToParse("InventoryCondition", 0)) as ItemPossessed;
            itemPossessed.Run();

            Assert.AreEqual(0, PersistentDataHolder.Instance.GetData("testItemPossessedCondition"));

            InventoryItem item = new InventoryItem(new BaseItem(0, "item", "desc", 0, 0, ItemCategory.Consumable));
            item.ChangeAmount(1);
            PartyManager.Instance.Inventory.Add(item);

            itemPossessed.Run();

            Assert.AreEqual(1, PersistentDataHolder.Instance.GetData("testItemPossessedCondition"));
        }

        [Test]
        public void RunItemEquippedConditionTest()
        {
            ItemEquipped itemEquipped = XmlFlowControlParser.ParseInventoryCondition(GetDataToParse("InventoryCondition", 1)) as ItemEquipped;
            itemEquipped.Run();

            Assert.AreEqual(0, PersistentDataHolder.Instance.GetData("testItemEquippedCondition"));

            Weapon item = new Weapon(0, "weapon", "itemDesc", 0, 0, ItemCategory.Weapon, 0, 0, WeaponType.Bow);
            Character character = new Character { Id = 0 };
            character.ChangeEquipment(item);

            PartyManager.Instance.GetParty().Add(character);

            itemEquipped.Run();

            Assert.AreEqual(1, PersistentDataHolder.Instance.GetData("testItemEquippedCondition"));
        }

        [SetUp]
        public void SetUp()
        {
            PersistentDataHolder.Instance.Reset();

            GameObject runner = new GameObject("Runner");
            runner.AddComponent<EventsRunner>();
        }
    }
}
