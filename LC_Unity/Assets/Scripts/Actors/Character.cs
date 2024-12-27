using Utils;
using UnityEngine;
using System.Collections.Generic;
using Actors.Equipment;
using Abilities;
using System.Text;
using Inventory;
using Effects;
using System.Linq;
using Core.Model;

namespace Actors
{
    public class Character
    {
        private ElementIdentifier _identifier;
        
        public int Id { get { return _identifier.Id; } }
        public string Name { get { return _identifier.NameKey; } }
        public List<ElementalAffinity> ElementalAffinities { get; private set; }
        public List<ActiveEffect> ActiveEffects { get; private set; }
        public CharacterEquipment Equipment { get; private set; }
        public EssenceAffinity EssenceAffinity { get; set; }
        public List<Ability> Abilities { get; private set; }
        public CharacterStats Stats { get; private set; }

        public Character()
        {
            Abilities = new List<Ability>();
            ElementalAffinities = new List<ElementalAffinity>();
            ActiveEffects = new List<ActiveEffect>();
            Equipment = new CharacterEquipment();

            InitEquipmentSlots();
            InitBasicAffinities();

            LearnSkill(0);
            LearnSkill(46);
            LearnSkill(1);
        }

        public Character(ElementIdentifier identifier, 
                         QuadraticFunction exp, 
                         StatScalingFunction health,
                         StatScalingFunction mana,
                         StatScalingFunction essence,
                         StatScalingFunction strength,
                         StatScalingFunction defense,
                         StatScalingFunction magic,
                         StatScalingFunction magicDefense,
                         StatScalingFunction agility,
                         StatScalingFunction luck) : this()
        {
            _identifier = identifier;
            Stats = new CharacterStats(exp, strength, defense, magic, magicDefense, agility, luck, health, mana, essence, Equipment);
        }

        public void ChangeLevel(int amount)
        {
            int targetLevel = Stats.Level + amount;
            int targetTotalXp = GetTotalRequiredXpForLevel(targetLevel);

            GiveExp(targetTotalXp - Stats.Exp);
        }

        public void GiveExp(int amount)
        {
            int currentLevel = Stats.Level;

            Stats.GiveExperience(amount);

            if (Stats.Level > currentLevel)
                Recover();
        }

        public void Recover()
        {
            Stats.CurrentHealth = Stats.MaxHealth;
            Stats.CurrentMana = Stats.MaxMana;
        }

        public void ChangeEquipment(int itemId)
        {
            ItemsWrapper wrapper = GameObject.FindObjectOfType<ItemsWrapper>();
            BaseItem item = wrapper.GetItemFromId(itemId);

            ChangeEquipment(item);
        }

        public void ChangeEquipment(BaseItem item)
        {
            if (item == null)
                return;

            switch (item.Category)
            {
                case ItemCategory.Weapon:
                    Equipment.RightHand = new EquipmentSlot(EquipmentPosition.RightHand, item.Id);
                    break;
                case ItemCategory.Armour:
                    switch ((item as Armour).Type)
                    {
                        case ArmourType.Head:
                            Equipment.Head = new EquipmentSlot(EquipmentPosition.Helmet, item.Id);
                            break;
                        case ArmourType.Body:
                            Equipment.Body = new EquipmentSlot(EquipmentPosition.Body, item.Id);
                            break;
                        case ArmourType.Shield:
                            Equipment.LeftHand = new EquipmentSlot(EquipmentPosition.LeftHand, item.Id);
                            break;
                    }
                    break;
                case ItemCategory.Accessory:
                    Equipment.Accessory = new EquipmentSlot(EquipmentPosition.Accessory, item.Id);
                    break;
            }
        }

        public void LearnSkill(int skillId)
        {
            Abilities.Add(AbilitiesManager.Instance.GetAbility(skillId));
        }

        public void ForgetSkill(int skillId)
        {
            Ability abilityToForget = AbilitiesManager.Instance.GetAbility(skillId);

            if(Abilities.Contains(abilityToForget))
                Abilities.Remove(abilityToForget);
        }

        public int GetXpForCurrentLevel()
        {
            return Stats.GetXpForCurrentLevel();
        }

        public int GetTotalRequiredXpForLevel(int level)
        {
            return Stats.GetTotalRequiredXpForLevel(level);
        }

        public int GetXpRequiredForLevel(int level)
        {
            int required = GetTotalRequiredXpForLevel(level);
            int requiredNext = GetTotalRequiredXpForLevel(level + 1);

            return requiredNext - required;
        }

        public void ChangeHealth(int change)
        {
            Stats.ChangeHealth(change);
        }

        public void ChangeMana(int change)
        {
            Stats.ChangeMana(change);
        }

        public void ChangeEssence(int change)
        {
            Stats.ChangeEssence(change);
        }

        private void InitBasicAffinities()
        {
            ElementalAffinities.Add(new ElementalAffinity(Element.Neutral, 1.0f));
            ElementalAffinities.Add(new ElementalAffinity(Element.Fire, 1.0f));
            ElementalAffinities.Add(new ElementalAffinity(Element.Ice, 1.0f));
            ElementalAffinities.Add(new ElementalAffinity(Element.Thunder, 1.0f));
            ElementalAffinities.Add(new ElementalAffinity(Element.Water, 1.0f));
            ElementalAffinities.Add(new ElementalAffinity(Element.Earth, 1.0f));
            ElementalAffinities.Add(new ElementalAffinity(Element.Wind, 1.0f));
            ElementalAffinities.Add(new ElementalAffinity(Element.Holy, 1.0f));
            ElementalAffinities.Add(new ElementalAffinity(Element.Darkness, 1.0f));
            ElementalAffinities.Add(new ElementalAffinity(Element.Healing, 1.0f));
        }

        public ElementalAffinity GetElementalAffinity(Element element)
        {
            ElementalAffinity affinity = ElementalAffinities.FirstOrDefault(e => e.Element == element);
            float multiplier = affinity.Multiplier;

            List<IEffect> effects = Equipment.GetAllItemEffects();
            for(int i = 0; i < effects.Count; i++)
            {
                if (effects[i] is ElementalAffinityModifier && (effects[i] as ElementalAffinityModifier).Element == element)
                {
                    multiplier += ((effects[i] as ElementalAffinityModifier).Value / 100.0f);
                }
            }

            return new ElementalAffinity(element, multiplier);
        }

        private void InitEquipmentSlots()
        {
            Equipment.Init();
        }

        public bool HasItemEquipped(int itemId)
        {
            if(Equipment.LeftHand.ItemId == itemId)
                return true;

            if(Equipment.RightHand.ItemId == itemId) 
                return true;

            if (Equipment.Head.ItemId == itemId)
                return true;

            if (Equipment.Body.ItemId == itemId)
                return true;

            if (Equipment.Accessory.ItemId == itemId)
                return true;

            return false;
        }

        public void UpdateName(string newName)
        {
            int currentId = _identifier.Id;
            _identifier = new ElementIdentifier(currentId, newName, "");
        }

        public string Serialize()
        {
            StringBuilder sb = new StringBuilder();

            // First the Exp value. Stats are based off level and equipment, so we only need to store these to get the stats
            sb.Append(Stats.Exp);
            sb.Append(',');

            // Now we get the equipment block.
            sb.Append(Equipment.Head.ItemId);
            sb.Append(',');
            sb.Append(Equipment.LeftHand.ItemId);
            sb.Append(',');
            sb.Append(Equipment.RightHand.ItemId);
            sb.Append(',');
            sb.Append(Equipment.Body.ItemId);
            sb.Append(',');
            sb.Append(Equipment.Accessory.ItemId);
            sb.Append(',');
            // Enf of the equipment block

            // character current HP, MP and EP
            sb.Append(Stats.CurrentHealth.ToString());
            sb.Append(',');
            sb.Append(Stats.CurrentMana.ToString());
            sb.Append(',');
            sb.Append(Stats.CurrentEssence.ToString());
            // end of character block

            return sb.ToString();
        }

        public static Character Deserialize(string id, string serializedCharacter)
        {
            string[] split = serializedCharacter.Split(',');
            int trueId = int.Parse(id.Replace("character", ""));

            Character character = CharactersManager.Instance.GetCharacter(trueId);
            character.Stats.SetExperience(int.Parse(split[0]));

            for (int i = 1; i < 6; i++)
                character.ChangeEquipment(int.Parse(split[i]));

            character.Stats.CurrentHealth = int.Parse(split[6]);
            character.Stats.CurrentMana = int.Parse(split[7]);
            character.Stats.CurrentEssence = int.Parse(split[8]);

            return character;
        }

        public bool EligibleForMenuEffect(IEffect effect)
        {
            if(effect is FoodBonus)
            {
                return Stats.CurrentHealth > 0;
            }
            else if(effect is RestoresResourceScaling)
            {
                RestoresResourceScaling restoringEffect = effect as RestoresResourceScaling;

                if (restoringEffect.Stat == Stat.HP)
                    return Stats.CurrentHealth < Stats.MaxHealth;
                else if (restoringEffect.Stat == Stat.MP)
                    return Stats.CurrentMana < Stats.MaxMana;
                else if (restoringEffect.Stat == Stat.EP)
                    return Stats.CurrentEssence < Stats.MaxEssence;
                else
                    return false;
            }
            else if(effect is Revives)
            {
                return Stats.CurrentHealth <= 0;
            }
            else if(effect is Dispel)
            {
                Dispel dispelEffect = effect as Dispel;
                return ActiveEffects.Any(e => e.Effect == dispelEffect.Value);
            }

            return false;
        }

        public void GiveItem(BaseItem item)
        {
            for(int i = 0; i < item.Effects.Count; i++)
            {
                if (item.Effects[i] is RestoresResourceScaling)
                {
                    RestoresResourceScaling restoreEffect = item.Effects[i] as RestoresResourceScaling;
                    restoreEffect.Apply(this);
                }
            }
        }
    }
}
