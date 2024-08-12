namespace Inventory
{
    public enum WeaponType
    {
        Dagger,
        Sword,
        Spear,
        Axe,
        Bow,
        Claw,
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
        public int Animation { get; protected set; }
        public WeaponType Type { get; protected set; }

        public Weapon(int id, string name, string description, int icon, int price, ItemCategory category, int animation, int enchantmentSlots, WeaponType type) : 
            base(id, name, description, icon, price, category, enchantmentSlots)
        {
            Animation = animation;
            Type = type;
        }
    }
}
