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
        Grounded,
        // Specific ability effects
        WeakPoint,
        KnightsVow,
        KnightsVowGuardian,
        Trance,
        CrystalGuard,
        EvasiveDance,
        Camouflage,
        EndlessRampage,
        HowlingMoon,
        HowlingMoonDebuff,
        Lifeguard,
        MagicCounter,
        Wisdom,
        ManaWell,
        Doom
    }

    public class ActiveEffect
    {
        public EffectType Effect { get; set; }
    }
}
