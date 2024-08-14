namespace Actors
{
    public enum EffectType
    {
        Poison,
        BleedI,
        BleedII,
        BleedIII,
        HemoI,
        BreakI,
        MagicBreakI,
        SlowI,
        Blind,
        Silence,
        Shell,
        Protect,
        Regen,
        ManaRegen,
        Bravery,
        Faith,
        Grounded
    }

    public class ActiveEffect
    {
        public EffectType Effect { get; set; }
    }
}
