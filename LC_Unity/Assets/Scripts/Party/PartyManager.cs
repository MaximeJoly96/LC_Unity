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
        private List<InventoryItem> _inventory;

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
            Gold = 100;
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
            InventoryItem item = _inventory.FirstOrDefault(i => i.ItemData.Id == change.Id);

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

        public void SetInventory(List<InventoryItem> inventory)
        {
            _inventory = new List<InventoryItem>();
            _inventory.AddRange(inventory);
        }
    }
}
