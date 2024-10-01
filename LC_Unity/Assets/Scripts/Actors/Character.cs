using Logging;
using Utils;
using UnityEngine;
using System.Collections.Generic;
using Actors.Equipment;
using Abilities;
using System.Text;
using Inventory;
using System.Runtime.InteropServices.WindowsRuntime;
using Effects;
using System.Linq;

namespace Actors
{
    public class Character
    {
        private int _currentHealth;
        private int _currentMana;
        private int _currentEssence;

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

        public int BonusStrength
        {
            get
            {
                return GetStatFromItems("Strength") + 
                       Mathf.RoundToInt((BaseStrength + GetStatFromItems("Strength")) * GetStatMultiplierFromItemEffects(Stat.Strength));
            }
        }

        public int BaseDefense
        {
            get
            {
                return Mathf.RoundToInt(DefenseFunction.Compute(Level));
            }
        }

        public int BonusDefense
        {
            get
            {
                return GetStatFromItems("Defense") +
                       Mathf.RoundToInt((BaseDefense + GetStatFromItems("Defense")) * GetStatMultiplierFromItemEffects(Stat.Defense));
            }
        }

        public int BaseMagic
        {
            get
            {
                return Mathf.RoundToInt(MagicFunction.Compute(Level));
            }
        }

        public int BonusMagic
        {
            get
            {
                return GetStatFromItems("Magic") +
                       Mathf.RoundToInt((BaseMagic + GetStatFromItems("Magic")) * GetStatMultiplierFromItemEffects(Stat.Magic));
            }
        }

        public int BaseMagicDefense
        {
            get
            {
                return Mathf.RoundToInt(MagicDefenseFunction.Compute(Level));
            }
        }

        public int BonusMagicDefense
        {
            get
            {
                return GetStatFromItems("MagicDefense") +
                       Mathf.RoundToInt((BaseMagicDefense + GetStatFromItems("MagicDefense")) * GetStatMultiplierFromItemEffects(Stat.MagicDefense));
            }
        }

        public int BaseAgility
        {
            get
            {
                return Mathf.RoundToInt(AgilityFunction.Compute(Level));
            }
        }

        public int BonusAgility
        {
            get
            {
                return GetStatFromItems("Agility") +
                       Mathf.RoundToInt((BaseAgility + GetStatFromItems("Agility")) * GetStatMultiplierFromItemEffects(Stat.Agility)); ;
            }
        }

        public int BaseLuck
        {
            get
            {
                return Mathf.RoundToInt(LuckFunction.Compute(Level));
            }
        }

        public int BonusLuck
        {
            get
            {
                return GetStatFromItems("Luck") +
                       Mathf.RoundToInt((BaseLuck + GetStatFromItems("Luck")) * GetStatMultiplierFromItemEffects(Stat.Luck)); ;
            }
        }

        public int CritChance
        {
            get
            {
                return Mathf.Clamp(5 + Mathf.RoundToInt(GetStatMultiplierFromItemEffects(Stat.CritChance) * 100), 0, 100);
            }
        }

        public int CritDamage
        {
            get
            {
                return 100 + Mathf.RoundToInt(GetStatMultiplierFromItemEffects(Stat.CritDmg) * 100); ;
            }
        }

        public int Parry
        {
            get
            {
                return Mathf.Clamp(10 + Mathf.RoundToInt(GetStatMultiplierFromItemEffects(Stat.Parry) * 100), 0, 100);
            }
        }

        public int Evasion
        {
            get
            {
                return Mathf.Clamp(5 + Mathf.RoundToInt(GetStatMultiplierFromItemEffects(Stat.Evasion) * 100), 0, 100); 
            }
        }

        public int Provocation
        {
            get
            {
                return Mathf.Clamp(100 + Mathf.RoundToInt(GetStatMultiplierFromItemEffects(Stat.Provocation) * 100), 0, 100);
            }
        }

        public int Accuracy
        {
            get
            {
                return Mathf.Clamp(90 + Mathf.RoundToInt(GetStatMultiplierFromItemEffects(Stat.Accuracy) * 100), 0, 100);
            }
        }

        public int CurrentHealth
        {
            get { return _currentHealth; }
            set
            {
                _currentHealth = Mathf.Min(Mathf.Max(0, value), MaxHealth);
            }
        }

        public int BaseHealth
        {
            get
            {
                return Mathf.RoundToInt(MaxHealthFunction.Compute(Level));
            }
        }

        public int BonusHealth
        {
            get
            {
                return GetStatFromItems("Health") +
                       Mathf.RoundToInt((BaseHealth + GetStatFromItems("Health")) * GetStatMultiplierFromItemEffects(Stat.HP));
            }
        }

        public int MaxHealth { get { return BaseHealth + BonusHealth; } }

        public int CurrentMana
        {
            get { return _currentMana; }
            set
            {
                _currentMana = Mathf.Min(Mathf.Max(0, value), MaxMana);
            }
        }

        public int BaseMana
        {
            get
            {
                return Mathf.RoundToInt(MaxManaFunction.Compute(Level));
            }
        }

        public int BonusMana
        {
            get
            {
                return GetStatFromItems("Mana") +
                       Mathf.RoundToInt((BaseMana + GetStatFromItems("Mana")) * GetStatMultiplierFromItemEffects(Stat.MP));
            }
        }

        public int MaxMana { get { return BaseMana + BonusMana; } }

        public int CurrentEssence
        {
            get { return _currentEssence; }
            set
            {
                _currentEssence = Mathf.Min(Mathf.Max(0, value), MaxEssence);
            }
        }

        public int BaseEssence
        {
            get
            {
                return Mathf.RoundToInt(MaxEssenceFunction.Compute(Level));
            }
        }

        public int BonusEssence
        {
            get
            {
                return GetStatFromItems("Essence") +
                       Mathf.RoundToInt((BaseEssence + GetStatFromItems("Essence")) * GetStatMultiplierFromItemEffects(Stat.EP));
            }
        }

        public int MaxEssence { get { return BaseEssence + BonusEssence; } }
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

            CurrentHealth = MaxHealth;
            CurrentMana = MaxMana;
            CurrentEssence = MaxEssence;
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

            ChangeEquipment(item);
        }

        public void ChangeEquipment(BaseItem item)
        {
            if (item == null)
                return;

            switch (item.Category)
            {
                case ItemCategory.Weapon:
                    RightHand = new EquipmentSlot(EquipmentPosition.RightHand, item.Id);
                    break;
                case ItemCategory.Armour:
                    switch ((item as Armour).Type)
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

        public void ChangeHealth(int change)
        {
            CurrentHealth -= change;
        }

        public void ChangeExp(int change)
        {
            Exp += change;
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

            List<IEffect> effects = GetAllItemEffects();
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
            Head = new EquipmentSlot(EquipmentPosition.Helmet, -1);
            LeftHand = new EquipmentSlot(EquipmentPosition.LeftHand, -1);
            RightHand = new EquipmentSlot(EquipmentPosition.RightHand, -1);
            Body = new EquipmentSlot(EquipmentPosition.Body, -1);
            Accessory = new EquipmentSlot(EquipmentPosition.Accessory, -1);
        }

        private int GetStatFromItems(string propertyName)
        {
            int stat = 0;

            EquipmentItem rightHand = RightHand.GetItem();
            if (rightHand != null && rightHand.Stats != null)
                stat += (int)rightHand.Stats.GetType().GetProperty(propertyName).GetValue(rightHand.Stats, null);

            EquipmentItem leftHand = LeftHand.GetItem();
            if (leftHand != null && leftHand.Stats != null)
                stat += (int)leftHand.Stats.GetType().GetProperty(propertyName).GetValue(leftHand.Stats, null);

            EquipmentItem head = Head.GetItem();
            if (head != null && head.Stats != null)
                stat += (int)head.Stats.GetType().GetProperty(propertyName).GetValue(head.Stats, null);

            EquipmentItem body = Body.GetItem();
            if (body != null && body.Stats != null)
                stat += (int)body.Stats.GetType().GetProperty(propertyName).GetValue(body.Stats, null);

            EquipmentItem accessory = Accessory.GetItem();
            if (accessory != null && accessory.Stats != null)
                stat += (int)accessory.Stats.GetType().GetProperty(propertyName).GetValue(accessory.Stats, null);

            return stat;
        }

        private float GetStatMultiplierFromItemEffects(Stat stat)
        {
            float multiplier = 0.0f;
            List<IEffect> allEffects = GetAllItemEffects();

            for (int i = 0; i < allEffects.Count; i++)
            {
                if (allEffects[i] is StatBoost && (allEffects[i] as StatBoost).Stat == stat)
                    multiplier += (allEffects[i] as StatBoost).Value;
            }

            return multiplier / 100.0f;
        }

        private List<IEffect> GetAllItemEffects()
        {
            List<IEffect> effects = new List<IEffect>();

            effects.AddRange(GetEffectsFromEquipmentSlot(RightHand));
            effects.AddRange(GetEffectsFromEquipmentSlot(LeftHand));
            effects.AddRange(GetEffectsFromEquipmentSlot(Head));
            effects.AddRange(GetEffectsFromEquipmentSlot(Body));
            effects.AddRange(GetEffectsFromEquipmentSlot(Accessory));

            return effects;
        }

        private List<IEffect> GetEffectsFromEquipmentSlot(EquipmentSlot slot)
        {
            List<IEffect> effects = new List<IEffect>();

            BaseItem item = slot.GetItem();
            if (item != null)
            {
                foreach (IEffect e in item.Effects)
                    effects.Add(e);
            }

            return effects;
        }

        public bool HasItemEquipped(int itemId)
        {
            if(LeftHand.ItemId == itemId)
                return true;

            if(RightHand.ItemId == itemId) 
                return true;

            if (Head.ItemId == itemId)
                return true;

            if (Body.ItemId == itemId)
                return true;

            if (Accessory.ItemId == itemId)
                return true;

            return false;
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
