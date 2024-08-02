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
        Gun,
        Arbalest
    }

    public class Weapon : EquipmentItem
    {
        public int Animation { get; protected set; }
        public WeaponType Type { get; protected set; }

        public Weapon(int id, string name, string description, int icon, int price, int animation, int enchantmentSlots, WeaponType type) : 
            base(id, name, description, icon, price, enchantmentSlots)
        {
            Animation = animation;
            Type = type;
        }
    }
}
