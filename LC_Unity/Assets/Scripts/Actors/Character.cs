namespace Actors
{
    public class Character
    {
        public Resource Health { get; set; }
        public Resource Mana { get; set; }
        public Resource Essence { get; set; }

        public int Strength { get; set; }
        public int Defense { get; set; }
        public int Magic { get; set; }
        public int MagicDefense { get; set; }
        public int Agility { get; set; }
        public int Luck { get; set; }

        public Character() : this(100, 50, 100, 10, 10, 10, 10, 10, 10)
        {

        }

        public Character(int maxHealth, int maxMana, int maxEssence, int strength, int defense, int magic, int magicDefense, int agility, int luck)
        {
            Health = new Resource(maxHealth);
            Mana = new Resource(maxMana);
            Essence = new Resource(maxEssence);
            Strength = strength;
            Defense = defense;
            Magic = magic;
            MagicDefense = magicDefense;
            Agility = agility;
            Luck = luck;
        }
    }
}
