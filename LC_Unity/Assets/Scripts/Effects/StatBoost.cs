namespace Effects
{
    public enum Stat
    {
        HP,
        MP,
        EP,
        Strength,
        Defense,
        Magic,
        MagicDefense,
        Agility,
        Luck,
        Accuracy,
        CritChance,
        CritDmg,
        Evasion,
        Provocation,
        Parry
    }

    public class StatBoost : IEffect
    {
        public Stat Stat { get; set; }
        public float Value { get; set; }
    }
}
