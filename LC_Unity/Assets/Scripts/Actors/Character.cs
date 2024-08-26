using Logging;
using Utils;
using UnityEngine;
using System.Collections.Generic;
using Actors.Equipment;
using Abilities;
using System.Text;
using Inventory;

namespace Actors
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Exp { get; private set; }
        public List<ElementalAffinity> ElementalAffinities { get; private set; }
        public List<ActiveEffect> ActiveEffects { get; private set; }
        public EquipmentSlot LeftHand { get; private set; }
        public EquipmentSlot RightHand { get; private set; }
        public EquipmentSlot Head { get; private set; }
        public EquipmentSlot Body { get; private set; }
        public EquipmentSlot Accessory { get; private set; }
        public EssenceAffinity EssenceAffinity { get; set; }
        public List<Ability> Abilities { get; private set; }

        #region Stats
        public int Level
        {
            get
            {
                float discriminant = ExpFunction.B * ExpFunction.B - 4 * ExpFunction.A * (ExpFunction.C - Exp);
                return Mathf.FloorToInt((-ExpFunction.B + Mathf.Sqrt(discriminant)) / (2 * ExpFunction.A));
            }
        }

        public int BaseStrength
        {
            get
            {
                return Mathf.RoundToInt(StrengthFunction.Compute(Level));
            }
        }

        public int BaseDefense
        {
            get
            {
                return Mathf.RoundToInt(DefenseFunction.Compute(Level));
            }
        }

        public int BaseMagic
        {
            get
            {
                return Mathf.RoundToInt(MagicFunction.Compute(Level));
            }
        }

        public int BaseMagicDefense
        {
            get
            {
                return Mathf.RoundToInt(MagicDefenseFunction.Compute(Level));
            }
        }

        public int BaseAgility
        {
            get
            {
                return Mathf.RoundToInt(AgilityFunction.Compute(Level));
            }
        }

        public int BaseLuck
        {
            get
            {
                return Mathf.RoundToInt(LuckFunction.Compute(Level));
            }
        }

        public Resource BaseHealth
        {
            get
            {
                return new Resource(Mathf.RoundToInt(MaxHealthFunction.Compute(Level)));
            }
        }

        public Resource BaseMana
        {
            get
            {
                return new Resource(Mathf.RoundToInt(MaxManaFunction.Compute(Level)));
            }
        }

        public Resource BaseEssence
        {
            get
            {
                return new Resource(Mathf.RoundToInt(MaxEssenceFunction.Compute(Level)));
            }
        }
        #endregion

        #region Scaling Functions
        public QuadraticFunction ExpFunction { get; set; }
        public StatScalingFunction StrengthFunction { get; set; }
        public StatScalingFunction DefenseFunction { get; set; }
        public StatScalingFunction MagicFunction { get; set; }
        public StatScalingFunction MagicDefenseFunction { get; set; }
        public StatScalingFunction AgilityFunction { get; set; }
        public StatScalingFunction LuckFunction { get; set; }
        public StatScalingFunction MaxHealthFunction { get; set; }
        public StatScalingFunction MaxManaFunction { get; set; }
        public StatScalingFunction MaxEssenceFunction { get; set; }
        #endregion

        public Character()
        {
            Abilities = new List<Ability>();
            ElementalAffinities = new List<ElementalAffinity>();
            ActiveEffects = new List<ActiveEffect>();

            InitBasicAffinities();
            InitEquipmentSlots();

            LearnSkill(0);
            LearnSkill(1);
        }

        public Character(int id, string name, 
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
            Id = id;
            Name = name;
            Exp = 10;

            ExpFunction = exp;
            MaxHealthFunction = health;
            MaxManaFunction = mana;
            MaxEssenceFunction = essence;
            StrengthFunction = strength;
            DefenseFunction = defense;
            MagicFunction = magic;
            MagicDefenseFunction = magicDefense;
            AgilityFunction = agility;
            LuckFunction = luck;
        }

        public void ChangeLevel(int amount)
        {
            LogsHandler.Instance.LogWarning("ChangeLevel has not been implemented yet.");
        }

        public void GiveExp(int amount)
        {
            Exp += amount;
        }

        public void Recover()
        {
            LogsHandler.Instance.LogWarning("Recover has not been implemented yet.");
        }

        public void ChangeEquipment(int itemId)
        {
            ItemsWrapper wrapper = GameObject.FindObjectOfType<ItemsWrapper>();
            BaseItem item = wrapper.GetItemFromId(itemId);

            if(item == null)
                return;
            
            switch(item.Category)
            {
                case ItemCategory.Weapon:
                    RightHand = new EquipmentSlot(EquipmentPosition.RightHand, item.Id);
                    break;
                case ItemCategory.Armour:
                    switch((item as Armour).Type)
                    {
                        case ArmourType.Head:
                            Head = new EquipmentSlot(EquipmentPosition.Helmet, item.Id);
                            break;
                        case ArmourType.Body:
                            Body = new EquipmentSlot(EquipmentPosition.Body, item.Id);
                            break;
                        case ArmourType.Shield:
                            LeftHand = new EquipmentSlot(EquipmentPosition.LeftHand, item.Id);
                            break;
                    }
                    break;
                case ItemCategory.Accessory:
                    Accessory = new EquipmentSlot(EquipmentPosition.Accessory, item.Id);
                    break;
            }
        }

        public void LearnSkill(int skillId)
        {
            Abilities.Add(AbilitiesManager.Instance.GetAbility(skillId));
        }

        public void ForgetSkill(int skillId)
        {
            LogsHandler.Instance.LogWarning("ForgetSkill has not been implemented yet.");
        }

        public int GetXpForCurrentLevel()
        {
            int requiredXp = GetTotalRequiredXpForLevel(Level);
            return Exp - requiredXp;
        }

        public int GetTotalRequiredXpForLevel(int level)
        {
            return Mathf.FloorToInt(ExpFunction.Compute(level));
        }

        public int GetXpRequiredForLevel(int level)
        {
            int required = GetTotalRequiredXpForLevel(level);
            int requiredNext = GetTotalRequiredXpForLevel(level + 1);

            return requiredNext - required;
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

        private void InitEquipmentSlots()
        {
            Head = new EquipmentSlot(EquipmentPosition.Helmet, -1);
            LeftHand = new EquipmentSlot(EquipmentPosition.LeftHand, -1);
            RightHand = new EquipmentSlot(EquipmentPosition.RightHand, -1);
            Body = new EquipmentSlot(EquipmentPosition.Body, -1);
            Accessory = new EquipmentSlot(EquipmentPosition.Accessory, -1);
        }

        public string Serialize()
        {
            StringBuilder sb = new StringBuilder();

            // First the Exp value. Stats are based off level and equipment, so we only need to store these to get the stats
            sb.Append(Exp);
            sb.Append(',');

            // Now we get the equipment block.
            sb.Append(Head.ItemId);
            sb.Append(',');
            sb.Append(LeftHand.ItemId);
            sb.Append(',');
            sb.Append(RightHand.ItemId);
            sb.Append(',');
            sb.Append(Body.ItemId);
            sb.Append(',');
            sb.Append(Accessory.ItemId);
            // Enf of the equipment block

            return sb.ToString();
        }

        public static Character Deserialize(string id, string serializedCharacter)
        {
            string[] split = serializedCharacter.Split(',');
            int trueId = int.Parse(id.Replace("character", ""));

            Character character = new Character
            {
                Id = trueId,
                Exp = int.Parse(split[0]),
            };

            for (int i = 1; i < split.Length; i++)
                character.ChangeEquipment(int.Parse(split[i]));

            return character;
        }
    }
}
