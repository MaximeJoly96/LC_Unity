namespace Abilities
{
    public class AbilityCost
    {
        public int HealthCost { get; private set; }
        public int ManaCost { get; private set; }
        public int EssenceCost { get; private set; }

        public AbilityCost(int healthCost, int manaCost, int essenceCost)
        {
            HealthCost = healthCost;
            ManaCost = manaCost;
            EssenceCost = essenceCost;
        }
    }
}
