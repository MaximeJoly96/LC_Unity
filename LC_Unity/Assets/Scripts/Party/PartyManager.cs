using System.Collections.Generic;
using System.Linq;
using Actors;
using UnityEngine;
using Inventory;
using Engine.Party;

namespace Party
{
    public class PartyManager
    {
        private readonly List<Character> _party;
        private readonly List<InventoryItem> _inventory;

        private static PartyManager _instance;

        public static PartyManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PartyManager();

                return _instance;
            }
        }

        public int Gold { get; private set; }
        public List<InventoryItem> Inventory { get { return _inventory; } }

        private PartyManager()
        {
            _party = new List<Character>();
            _inventory = new List<InventoryItem>();
            Gold = 0;

            _inventory.Add(new InventoryItem(0, "Potion", ItemCategory.Consumable, null, "Restores 40% of the target's max Health."));
            _inventory.Add(new InventoryItem(1, "Amulet", ItemCategory.Accessory, null, "A worn antique that increases Magic by 10."));
            _inventory.Add(new InventoryItem(2, "Blade", ItemCategory.Weapon, null, "A very simplistic sword."));
            _inventory.Add(new InventoryItem(3, "Helmet", ItemCategory.Armour, null, "A very simplistic helmet for battle."));
            _inventory.Add(new InventoryItem(4, "Iron Armour", ItemCategory.Armour, null, "A heavy armour made of several iron plates."));
            _inventory.Add(new InventoryItem(5, "Secret Note", ItemCategory.KeyItem, null, "A note written by the back alley mrrchant."));
            _inventory.Add(new InventoryItem(6, "Appeus Fluid", ItemCategory.Resource, null, "The substance all Appei are made of."));
            _inventory.Add(new InventoryItem(7, "Ether", ItemCategory.Consumable, null, "Restores 25% of the target's max Mana."));
        }

        public void ChangeGold(ChangeGold change)
        {
            Gold += change.Value;
            Gold = Mathf.Clamp(Gold, 0, int.MaxValue);
        }

        public void ChangePartyMember(ChangePartyMember change)
        {
            switch(change.Action)
            {
                case Engine.Party.ChangePartyMember.ActionType.Add:
                    AddToParty(change);
                    break;
                case Engine.Party.ChangePartyMember.ActionType.Remove:
                    RemoveFromParty(change);
                    break;
            }
        }

        public void AddToParty(ChangePartyMember change)
        {
            Character character = CharactersManager.Instance.GetCharacter(change.Id);

            if (character != null && _party.FirstOrDefault(c => c.Id == change.Id) == null)
                _party.Add(character);
        }

        public void RemoveFromParty(ChangePartyMember change)
        {
            Character character = CharactersManager.Instance.GetCharacter(change.Id);

            if (character != null && _party.FirstOrDefault(c => c.Id == change.Id) != null)
                _party.Remove(character);
        }

        public void ChangeItems(ChangeItems change)
        {
            InventoryItem item = _inventory.FirstOrDefault(i => i.Id == change.Id);

            if(item != null)
                item.ChangeAmount(change.Quantity);
        }

        public List<Character> GetParty()
        {
            return _party;
        }

        public void LoadPartyFromBaseFile(TextAsset dataFile)
        {
            _party.AddRange(XmlCharacterParser.ParseCharacters(dataFile));
        }

        private void LoadPartyFromSave(List<Character> characters)
        {
            _party.AddRange(characters);
        }
    }
}
