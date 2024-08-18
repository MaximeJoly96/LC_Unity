using Actors;
using Effects;

namespace Language
{
    public enum Language { English, French }

    public static class LanguageUtility
    {
        public static string TranslateLanguageLabel(Language language)
        {
            switch (language)
            {
                case Language.English:
                    return "English";
                case Language.French:
                    return "Français";
            }

            return "English";
        }

        public static string GetStatLanguageKey(Stat stat)
        {
            switch (stat)
            {
                case Stat.HP:
                    return "health";
                case Stat.MP:
                    return "mana";
                case Stat.Strength:
                    return "strength";
                case Stat.Defense:
                    return "defense";
                case Stat.MagicDefense:
                    return "magicDefense";
                case Stat.Magic:
                    return "magic";
                case Stat.Agility:
                    return "agility";
                case Stat.Luck:
                    return "luck";
                case Stat.CritChance:
                    return "critChance";
                case Stat.Evasion:
                    return "evasion";
                case Stat.Parry:
                    return "parry";
                case Stat.Provocation:
                    return "provocation";
                case Stat.Accuracy:
                    return "accuracy";
                case Stat.CritDmg:
                    return "critDmg";
                default:
                    return "";
            }
        }

        public static string GetEffectTypeLanguageKey(EffectType effect)
        {
            switch (effect)
            {
                case EffectType.BleedI:
                    return "bleedI";
                case EffectType.BleedII:
                    return "bleedII";
                case EffectType.BleedIII:
                    return "bleedIII";
                case EffectType.Poison:
                    return "poison";
                case EffectType.HemoI:
                    return "hemoI";
                case EffectType.BreakI:
                    return "breakI";
                case EffectType.BreakII:
                    return "breakII";
                case EffectType.BreakIII:
                    return "breakIII";
                case EffectType.MagicBreakI:
                    return "magicBreakI";
                case EffectType.MagicBreakII:
                    return "magicBreakII";
                case EffectType.MagicBreakIII:
                    return "magicBreakIII";
                case EffectType.SlowI:
                    return "slowI";
                case EffectType.SlowII:
                    return "slowII";
                case EffectType.SlowIII:
                    return "slowIII";
                case EffectType.Blind:
                    return "blind";
                case EffectType.Silence:
                    return "silence";
                case EffectType.Shell:
                    return "shell";
                case EffectType.Protect:
                    return "protect";
                case EffectType.Regen:
                    return "regen";
                case EffectType.ManaRegen:
                    return "manaRegen";
                case EffectType.Bravery:
                    return "bravery";
                case EffectType.Faith:
                    return "faith";
                case EffectType.Grounded:
                    return "grounded";
                default:
                    return "";
            }
        }

        public static string GetTribeLanguageKey(TargetTribe tribe)
        {
            switch (tribe)
            {
                case TargetTribe.Human:
                    return "human";
                case TargetTribe.Undead:
                    return "undead";
                default:
                    return "";
            }
        }
    }
}
