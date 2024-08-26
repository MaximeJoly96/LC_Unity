namespace Inventory
{
    public class ItemStats
    {
        public int Health { get; set; }
        public int Mana { get; set; }
        public int Essence { get; set; }
        public int Strength { get; set; }
        public int Defense { get; set; }
        public int Magic { get; set; }
        public int MagicDefense { get; set; }
        public int Agility { get; set; }
        public int Luck { get; set; }

        public ItemStats(int health, int mana, int essence, int strength, int defense, int magic, int magicDefense, int agility, int luck)
        {
            Health = health;
            Mana = mana;
            Essence = essence;
            Strength = strength;
            Defense = defense;
            Magic = magic;
            MagicDefense = magicDefense;
            Agility = agility;
            Luck = luck;
        }
    }
}
