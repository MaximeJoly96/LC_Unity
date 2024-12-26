using NUnit.Framework;
using Party;
using Engine.Party;
using Actors;
using UnityEngine;
using UnityEditor;
using Essence;
using System.Collections.Generic;
using Inventory;
using Core.Model;
using Engine.Actor;
using Abilities;

namespace Testing.Party
{
    public class PartyManagerTests : TestFoundation
    {
        [SetUp]
        public void Setup()
        {
            PartyManager.Instance.Inventory.Clear();
            PartyManager.Instance.GetParty().Clear();

            CreateEssencesWrapper();
        }

        [Test]
        public void PartyManagerCanBeCreated()
        {
            PartyManager manager = PartyManager.Instance;

            Assert.AreEqual(0, manager.Gold);
            Assert.AreEqual(0, manager.Inventory.Count);
            Assert.AreEqual(0, manager.GetParty().Count);
        }

        [Test]
        public void GoldValueCanBeChanged()
        {
            PartyManager manager = PartyManager.Instance;

            Assert.AreEqual(0, manager.Gold);
            manager.ChangeGold(new ChangeGold { Value = 100 });

            Assert.AreEqual(100, manager.Gold);
            manager.ChangeGold(new ChangeGold { Value = -50 });

            Assert.AreEqual(50, manager.Gold);
            manager.ChangeGold(new ChangeGold { Value = -200 });

            Assert.AreEqual(0, manager.Gold);
        }

        [Test]
        public void PartyMemberCanBeAdded()
        {
            PartyManager manager = PartyManager.Instance;
            manager.Clear();
            CharactersManager.Instance.Characters.Clear();

            CharactersManager.Instance.LoadCharactersFromFile(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Party/TestCharacters.xml"));

            Assert.AreEqual(0, manager.GetParty().Count);

            manager.ChangePartyMember(new ChangePartyMember
            {
                Action = ChangePartyMember.ActionType.Add,
                Id = 2,
                Initialize = true
            });

            Assert.AreEqual(1, manager.GetParty().Count);
            Assert.AreEqual("Kolibri", manager.GetParty()[0].Name);
        }

        [Test]
        public void PartyMemberCanBeRemoved()
        {
            PartyManager manager = PartyManager.Instance;
            manager.Clear();
            CharactersManager.Instance.Characters.Clear();

            CharactersManager.Instance.LoadCharactersFromFile(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Party/TestCharacters.xml"));

            manager.ChangePartyMember(new ChangePartyMember
            {
                Action = ChangePartyMember.ActionType.Add,
                Id = 2,
                Initialize = true
            });
            manager.ChangePartyMember(new ChangePartyMember
            {
                Action = ChangePartyMember.ActionType.Add,
                Id = 4,
                Initialize = true
            });

            Assert.AreEqual(2, manager.GetParty().Count);
            Assert.AreEqual("Kolibri", manager.GetParty()[0].Name);
            Assert.AreEqual("Magnus", manager.GetParty()[1].Name);

            manager.ChangePartyMember(new ChangePartyMember
            {
                Action = ChangePartyMember.ActionType.Remove,
                Id = 2
            });

            Assert.AreEqual(1, manager.GetParty().Count);
            Assert.AreEqual("Magnus", manager.GetParty()[0].Name);
        }

        [Test]
        public void ItemsCanBeChanged()
        {
            PartyManager manager = PartyManager.Instance;
            manager.Clear();

            ItemsWrapper wrapper = CreateEmptyWrapper();
            wrapper.FeedConsumables(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Party/TestConsumables.xml"));

            Assert.AreEqual(0, manager.Inventory.Count);

            manager.ChangeItems(new ChangeItems
            {
                Id = 0,
                Quantity = 3
            });

            Assert.AreEqual(1, manager.Inventory.Count);
            Assert.AreEqual("potion", manager.Inventory[0].ItemData.Name);
            Assert.AreEqual(3, manager.Inventory[0].InPossession);

            manager.ChangeItems(new ChangeItems
            {
                Id = 0,
                Quantity = 2
            });

            Assert.AreEqual(1, manager.Inventory.Count);
            Assert.AreEqual("potion", manager.Inventory[0].ItemData.Name);
            Assert.AreEqual(5, manager.Inventory[0].InPossession);

            manager.ChangeItems(new ChangeItems
            {
                Id = 0,
                Quantity = -1
            });

            Assert.AreEqual(1, manager.Inventory.Count);
            Assert.AreEqual("potion", manager.Inventory[0].ItemData.Name);
            Assert.AreEqual(4, manager.Inventory[0].InPossession);

            manager.ChangeItems(new ChangeItems
            {
                Id = 0,
                Quantity = -4
            });

            Assert.AreEqual(0, manager.Inventory.Count);
        }

        [Test]
        public void InventoryCanBeSet()
        {
            PartyManager manager = PartyManager.Instance;
            manager.Clear();

            List<InventoryItem> items = new List<InventoryItem>()
            {
                new InventoryItem(new BaseItem(new ElementIdentifier(0, "item", "itemDesc"), 0, 0, ItemCategory.Consumable)),
                new InventoryItem(new BaseItem(new ElementIdentifier(1, "item", "itemDesc"), 0, 0, ItemCategory.Consumable)),
                new InventoryItem(new BaseItem(new ElementIdentifier(2, "item", "itemDesc"), 0, 0, ItemCategory.Consumable))
            };

            manager.SetInventory(items);

            Assert.AreEqual(3, manager.Inventory.Count);
            Assert.AreEqual(0, manager.Inventory[0].ItemData.Id);
            Assert.AreEqual(1, manager.Inventory[1].ItemData.Id);
            Assert.AreEqual(2, manager.Inventory[2].ItemData.Id);
        }

        [Test]
        public void CharacterNameCanBeChanged()
        {
            PartyManager manager = PartyManager.Instance;
            manager.Clear();
            CharactersManager.Instance.Characters.Clear();

            CharactersManager.Instance.LoadCharactersFromFile(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Party/TestCharacters.xml"));
            manager.ChangePartyMember(new ChangePartyMember { Action = ChangePartyMember.ActionType.Add, Id = 0, Initialize = true });

            Assert.AreEqual("Louga", manager.GetCharacter(0).Name);

            manager.ChangeCharacterName(new ChangeName { CharacterId = 0, Value = "Louga2" });

            Assert.AreEqual("Louga2", manager.GetCharacter(0).Name);
        }

        [Test]
        public void CharacterLevelCanBeChanged()
        {
            PartyManager manager = PartyManager.Instance;
            manager.Clear();
            CharactersManager.Instance.Characters.Clear();

            CharactersManager.Instance.LoadCharactersFromFile(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Party/TestCharacters.xml"));
            manager.ChangePartyMember(new ChangePartyMember { Action = ChangePartyMember.ActionType.Add, Id = 0, Initialize = true });

            Assert.AreEqual(0, manager.GetCharacter(0).Stats.Level);

            manager.ChangeCharacterLevel(new ChangeLevel { CharacterId = 0, Amount = 3 });

            Assert.AreEqual(3, manager.GetCharacter(0).Stats.Level);
        }

        [Test]
        public void CharacterExpCanBeChanged()
        {
            PartyManager manager = PartyManager.Instance;
            manager.Clear();
            CharactersManager.Instance.Characters.Clear();

            CharactersManager.Instance.LoadCharactersFromFile(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Party/TestCharacters.xml"));
            manager.ChangePartyMember(new ChangePartyMember { Action = ChangePartyMember.ActionType.Add, Id = 0, Initialize = true });

            Assert.AreEqual(10, manager.GetCharacter(0).Stats.Exp);

            manager.ChangeCharacterExp(new ChangeExp { CharacterId = 0, Amount = 200 });

            Assert.AreEqual(210, manager.GetCharacter(0).Stats.Exp);
        }

        [Test]
        public void WholePartyCanRecover()
        {
            PartyManager manager = PartyManager.Instance;
            manager.Clear();
            CharactersManager.Instance.Characters.Clear();

            CharactersManager.Instance.LoadCharactersFromFile(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Party/TestCharacters.xml"));
            manager.ChangePartyMember(new ChangePartyMember { Action = ChangePartyMember.ActionType.Add, Id = 0, Initialize = true });
            manager.ChangePartyMember(new ChangePartyMember { Action = ChangePartyMember.ActionType.Add, Id = 1, Initialize = true });
            manager.ChangePartyMember(new ChangePartyMember { Action = ChangePartyMember.ActionType.Add, Id = 2, Initialize = true });

            manager.GetCharacter(0).ChangeHealth(20);
            manager.GetCharacter(1).ChangeMana(10);
            manager.GetCharacter(2).ChangeEssence(30);

            Assert.AreEqual(236, manager.GetCharacter(0).Stats.CurrentHealth);
            Assert.AreEqual(38, manager.GetCharacter(1).Stats.CurrentMana);
            Assert.AreEqual(70, manager.GetCharacter(2).Stats.CurrentEssence);

            manager.RecoverAll(new RecoverAll());

            Assert.AreEqual(256, manager.GetCharacter(0).Stats.CurrentHealth);
            Assert.AreEqual(48, manager.GetCharacter(1).Stats.CurrentMana);
            Assert.AreEqual(70, manager.GetCharacter(2).Stats.CurrentEssence);
        }

        [Test]
        public void EquipmentCanBeChanged()
        {
            PartyManager manager = PartyManager.Instance;
            manager.Clear();
            CharactersManager.Instance.Characters.Clear();

            CharactersManager.Instance.LoadCharactersFromFile(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Party/TestCharacters.xml"));
            ItemsWrapper wrapper = CreateEmptyWrapper();
            wrapper.FeedWeapons(new List<TextAsset> { AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Party/TestWeapons.xml") });

            manager.ChangePartyMember(new ChangePartyMember { Action = ChangePartyMember.ActionType.Add, Id = 0, Initialize = true });

            Assert.IsFalse(manager.GetCharacter(0).HasItemEquipped(1216));

            manager.ChangeEquipment(new ChangeEquipment { CharacterId = 0, ItemId = 1216 });

            Assert.IsTrue(manager.GetCharacter(0).HasItemEquipped(1216));
        }

        [Test]
        public void CharacterSkillsCanBeChanged()
        {
            AbilitiesManager.Instance.Abilities.Clear();
            AbilitiesManager.Instance.Init(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Engine/Actor/TestAbilities.xml"));
            PartyManager manager = PartyManager.Instance;
            manager.Clear();
            CharactersManager.Instance.Characters.Clear();

            CharactersManager.Instance.LoadCharactersFromFile(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Party/TestCharacters.xml"));

            manager.ChangePartyMember(new ChangePartyMember { Action = ChangePartyMember.ActionType.Add, Id = 0, Initialize = true });

            Assert.AreEqual(2, manager.GetCharacter(0).Abilities.Count);
            Assert.AreEqual(AbilityCategory.AttackCommand, manager.GetCharacter(0).Abilities[0].Category);
            Assert.AreEqual(AbilityCategory.FleeCommand, manager.GetCharacter(0).Abilities[1].Category);

            manager.ChangeSkills(new ChangeSkills { CharacterId = 0, Action = ChangeSkills.ActionType.Learn, SkillId = 2 });

            Assert.AreEqual(3, manager.GetCharacter(0).Abilities.Count);
            Assert.AreEqual(AbilityCategory.Skill, manager.GetCharacter(0).Abilities[2].Category);

            manager.ChangeSkills(new ChangeSkills { CharacterId = 0, Action = ChangeSkills.ActionType.Forget, SkillId = 2 });

            Assert.AreEqual(2, manager.GetCharacter(0).Abilities.Count);
            Assert.AreEqual(AbilityCategory.AttackCommand, manager.GetCharacter(0).Abilities[0].Category);
            Assert.AreEqual(AbilityCategory.FleeCommand, manager.GetCharacter(0).Abilities[1].Category);
        }

        [Test]
        public void PartyCanBeLoadedFromSave()
        {
            PartyManager manager = PartyManager.Instance;
            manager.Clear();
            CharactersManager.Instance.Characters.Clear();

            CharactersManager.Instance.LoadCharactersFromFile(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Party/TestCharacters.xml"));
            ItemsWrapper wrapper = CreateEmptyWrapper();
            wrapper.FeedWeapons(new List<TextAsset> { AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Party/TestWeapons.xml") });
            wrapper.FeedAccessories(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Party/TestAccessories.xml"));
            wrapper.FeedArmours(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Party/TestArmours.xml"));

            List<Character> fromSave = new List<Character>
            {
                CharactersManager.Instance.GetCharacter(0),
                CharactersManager.Instance.GetCharacter(1),
                CharactersManager.Instance.GetCharacter(2),
            };

            fromSave[0].ChangeEquipment(1216);
            fromSave[0].ChangeEquipment(2000);
            fromSave[0].ChangeEquipment(3000);
            fromSave[0].GiveExp(15000);
            fromSave[1].ChangeEquipment(1071);
            fromSave[1].ChangeEquipment(2002);
            fromSave[1].ChangeEquipment(3000);
            fromSave[1].GiveExp(9000);
            fromSave[2].ChangeEquipment(1205);
            fromSave[2].ChangeEquipment(2001);
            fromSave[2].GiveExp(23000);

            manager.LoadPartyFromSave(fromSave);

            Assert.AreEqual(5, manager.GetCharacter(0).Stats.Level);
            Assert.AreEqual(4, manager.GetCharacter(1).Stats.Level);
            Assert.AreEqual(6, manager.GetCharacter(2).Stats.Level);

            Assert.IsTrue(manager.GetCharacter(0).HasItemEquipped(1216));
            Assert.IsTrue(manager.GetCharacter(0).HasItemEquipped(2000));
            Assert.IsTrue(manager.GetCharacter(0).HasItemEquipped(3000));

            Assert.IsTrue(manager.GetCharacter(1).HasItemEquipped(1071));
            Assert.IsTrue(manager.GetCharacter(1).HasItemEquipped(2002));
            Assert.IsTrue(manager.GetCharacter(1).HasItemEquipped(3000));

            Assert.IsTrue(manager.GetCharacter(2).HasItemEquipped(1205));
            Assert.IsTrue(manager.GetCharacter(2).HasItemEquipped(2001));
        }

        private EssencesWrapper CreateEssencesWrapper()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);

            EssencesWrapper wrapper = go.AddComponent<EssencesWrapper>();
            wrapper.FeedAffinities(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Party/Essences.xml"));

            return wrapper;
        }

        private ItemsWrapper CreateEmptyWrapper()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);
            return go.AddComponent<ItemsWrapper>();
        }
    }
}
