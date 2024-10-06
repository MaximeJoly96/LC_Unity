using Core.Model;
using Abilities;

namespace Inventory
{
    public enum WeaponType
    {
        Dagger,
        Sword,
        Spear,
        Axe,
        Bow,
        Claws,
        Scepter,
        Staff,
        Grimoire,
        Scythe,
        Fist,
        Firearm,
        Arbalest,
        Mace
    }

    public class Weapon : EquipmentItem
    {
        public AbilityAnimation Animation { get; protected set; }
        public WeaponType Type { get; protected set; }
        public int Rank { get; set; }
        public float ProjectileSpeed { get; set; }
        public int Range { get; set; } = 100;

        public Weapon(ElementIdentifier identifier, int icon, int price, ItemCategory category, AbilityAnimation animation, int enchantmentSlots, WeaponType type) : 
            base(identifier, icon, price, category, enchantmentSlots)
        {
            Animation = animation;
            Type = type;
        }
    }
}
