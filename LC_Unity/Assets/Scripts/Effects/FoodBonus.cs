namespace Effects
{
    public enum FoodEffect
    {
        StrengthI,
        MagicI,
        DefenseI,
        MagicDefenseI,
        AgilityI,
        LuckI,
        HealthI,
        ManaI,
        PoisonResist,
        BlindResist,
        SilenceResist,
        SlowResist,
        BreakResist,
        MagicBreakResist,
        FireResistI,
        ThunderResistI,
        WaterResistI,
        IceResistI,
        EarthResistI,
        WindResistI,
        HolyResistI,
        DarknessResistI,
        AccuracyI,
        EvasionI,
        ParryI,
        ProvocationI,
        CritDamageI,
        CritChanceI,
        ReducedStrengthI,
        ReducedDefenseI,
        ReducedAgilityI,
        ReducedAccuracyI
    }

    public class FoodBonus : IEffect
    {
        public FoodEffect Value { get; set; }

        public string GetDescription()
        {
            return "";
        }
    }
}
