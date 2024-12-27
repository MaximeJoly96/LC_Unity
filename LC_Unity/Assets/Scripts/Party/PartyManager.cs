using System.Collections.Generic;
using System.Linq;
using Actors;
using UnityEngine;
using Inventory;
using Engine.Party;
using Engine.Actor;
using UnityEngine.Events;

namespace Party
{
    public class PartyManager
    {
        private readonly List<Character> _party;
        private List<InventoryItem> _inventory;
        private UnityEvent _inventoryChanged;

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
        public UnityEvent InventoryChanged
        {
            get
            {
                if (_inventoryChanged == null)
                    _inventoryChanged = new UnityEvent();

                return _inventoryChanged;
            }
        }

        private PartyManager()
        {
            _party = new List<Character>();
            _inventory = new List<InventoryItem>();
            Gold = 0;
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
            {
                item.ChangeAmount(change.Quantity);

                if(item.InPossession <= 0)
                    _inventory.Remove(item);
            } 
            else
            {
                ItemsWrapper wrapper = Object.FindObjectOfType<ItemsWrapper>();
                if(wrapper)
                {
                    InventoryItem inventoryItem = new InventoryItem(wrapper.GetItemFromId(change.Id));
                    inventoryItem.ChangeAmount(change.Quantity);
                    _inventory.Add(inventoryItem);
                }
            }

            InventoryChanged.Invoke();
        }

        public List<Character> GetParty()
        {
            return _party;
        }

        public void LoadPartyFromSave(List<Character> characters)
        {
            _party.Clear();

            for(int i = 0; i < characters.Count; i++)
            {
                _party.Add(characters[i]);
            }
        }

        public void SetInventory(List<InventoryItem> inventory)
        {
            _inventory = new List<InventoryItem>();
            _inventory.AddRange(inventory);
        }

        public void ChangeCharacterName(ChangeName change)
        {
            Character character = GetCharacter(change.CharacterId);

            if (character != null)
                character.UpdateName(change.Value);
        }

        public void ChangeCharacterLevel(ChangeLevel change)
        {
            Character character = GetCharacter(change.CharacterId);

            if (character != null)
                character.ChangeLevel(change.Amount);
        }

        public void ChangeCharacterExp(ChangeExp change)
        {
            Character character = GetCharacter(change.CharacterId);

            if (character != null)
                character.GiveExp(change.Amount);
        }

        public void RecoverAll(RecoverAll recoverAll)
        {
            for (int i = 0; i < _party.Count; i++)
            {
                _party[i].Recover();
            }
        }

        public void ChangeEquipment(ChangeEquipment change)
        {
            Character character = GetCharacter(change.CharacterId);

            if (character != null)
                character.ChangeEquipment(change.ItemId);
        }

        public void ChangeSkills(ChangeSkills change)
        {
            Character character = GetCharacter(change.CharacterId);

            if (character != null)
            {
                switch (change.Action)
                {
                    case Engine.Actor.ChangeSkills.ActionType.Forget:
                        character.ForgetSkill(change.SkillId);
                        break;
                    case Engine.Actor.ChangeSkills.ActionType.Learn:
                        character.LearnSkill(change.SkillId);
                        break;
                }
            }
        }

        public Character GetCharacter(int id)
        {
            return _party.FirstOrDefault(c => c.Id == id);
        }

        public void Clear()
        {
            _party.Clear();
            _inventory.Clear();
            Gold = 0;
        }

        public bool IsItemAvailable(int id)
        {
            InventoryItem item = _inventory.FirstOrDefault(i => i.ItemData.Id == id);

            return item != null && item.InPossession > 0;
        }
    }
}
