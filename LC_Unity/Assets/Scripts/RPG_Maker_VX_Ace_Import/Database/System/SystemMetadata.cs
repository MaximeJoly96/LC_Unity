using UnityEngine;
using System.Collections;
using System;

namespace RPG_Maker_VX_Ace_Import.Database.System
{
    public enum ElementalTypes
    {
        Neutral,
        Fire,
        Ice,
        Water,
        Thunder,
        Earth,
        Wind,
        Light,
        Dark,
        Heal
    }

    public enum WeaponTypes
    {
        Sword,
        Dagger,
        Saber,
        Axe,
        Hammer,
        Bow,
        Spear,
        Staff,
        Scepter,
        Tool,
        Gun,
        Claw
    }

    public enum EquipmentParts
    {
        Head,
        Body,
        Feet,
        LeftHand,
        RightHand,
        Accessory
    }

    public enum EnchantTypes
    {
        Orb,
        Stone,
        Jewel,
        Rune
    }

    public enum CharacterStats
    {
        Health,
        Mana,
        Tech,
        Strength,
        Defense,
        Magic,
        MagicDefense,
        Agility,
        Luck,
        Evasion,
        CritChance,
        CritDamage,
        Precision,
        HealthRegen,
        ManaRegen,
        TechRegen
    }

    [Serializable]
    public class SystemMetadata
    {
        public string gameName;
        public string currency;
        public string gameVersion;
    }
}

