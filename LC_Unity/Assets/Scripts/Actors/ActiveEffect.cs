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
        BreakII,
        BreakIII,
        MagicBreakI,
        MagicBreakII,
        MagicBreakIII,
        SlowI,
        SlowII,
        SlowIII,
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
