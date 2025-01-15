using UnityEngine;
using System.Collections.Generic;
using Actors;

namespace Abilities
{
    public class DamageFormula
    {
        public static int ComputeResult(int abilityId, Character source, Character target)
        {
            switch(abilityId)
            {
                case 0:
                case 47:
                case 48:
                    return AttackCommandFormula(source, target);
                case 2:
                    return ClawsCommandFormula(source, target);
                case 3:
                    return DrainSlashFormula(source, target);
                case 4:
                    return WeakPointFormula(source, target);
                case 6:
                    return MagicalImpactFormula(source, target);
                case 11:
                    return HolyWrathFormula(source, target);
                case 111:
                    return HolyWrathFormula(source, target) / 2;
                case 12:
                    return SinisterStrikeFormula(source, target);
                case 15:
                    return BloodyMurderFormula(source, target);
                case 151:
                    return BloodyMurderFormula(source, target) / 5;
                case 17:
                    return AxeThrowFormula(source, target);
                case 18:
                    return CleaveFormula(source, target);
                case 19:
                    return CrushFormula(source, target);
                case 20:
                    return BloodbathFormula(source, target);
                case 21:
                    return ExecuteFormula(source, target);
                case 24:
                    return ClawStormFormula(source, target);
                case 25:
                    return CrystalBreach(source, target);
                case 28:
                    return EssenceFluxFormula(source, target);
                case 31:
                    return VoidBeamFormula(source, target);
                case 36:
                    return PainBlastFormula(source, target);
                case 38:
                    return FlareFormula(source, target);
                case 40:
                    return DeadlySpinFormula(source, target);
                case 41:
                    return ShatterFormula(source, target);
                case 42:
                    return ImpairFormula(source, target);
                case 43:
                    return SwiftStrikeFormula(source, target);
                case 45:
                    return MagickaFormula(source, target);
                case 1000:
                    return DynamiteFormula(source, target);
                case 1001:
                    return ThrowableElementalItem(source, target);
                case 1002:
                    return ExplosiveBoulderFormula(source, target);
                case 49:
                    return CursedGazeFormula(source, target);
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                case 56:
                case 57:
                case 58:
                    return Rank1ElementalMagicFormula(source, target);
                case 511:
                case 521:
                case 531:
                case 541:
                case 551:
                case 561:
                case 571:
                case 581:
                    return Mathf.RoundToInt(Rank1ElementalMagicFormula(source, target) * 0.6f);
            }

            return 0;
        }

        private static int AttackCommandFormula(Character source, Character target)
        {
            return Mathf.Max(0, Mathf.RoundToInt(((source.Stats.BaseStrength + source.Stats.BonusStrength) * 2.0f - (target.Stats.BaseDefense + target.Stats.BonusDefense)) 
                                * (1 + source.Stats.Level * 0.025f)));
        }

        private static int ClawsCommandFormula(Character source, Character target)
        {
            return Mathf.Max(0, Mathf.RoundToInt(((source.Stats.BaseStrength + source.Stats.BonusStrength) * 2.0f - (target.Stats.BaseDefense + target.Stats.BonusDefense))
                                * (1 + source.Stats.Level * 0.025f)));
        }

        private static int MagickaFormula(Character source, Character target)
        {
            return Mathf.Max(0, Mathf.RoundToInt(((source.Stats.BaseMagic + source.Stats.BonusMagic) * 2.2f - (target.Stats.BaseMagicDefense + target.Stats.BonusMagicDefense))
                                * (1 + source.Stats.Level * 0.025f)));
        }

        private static int DynamiteFormula(Character source, Character target)
        {
            return Mathf.Max(0, (source.Stats.Level + 1) * 30 - target.Stats.BaseMagicDefense - target.Stats.BonusMagicDefense);
        }

        private static int ThrowableElementalItem(Character source, Character target)
        {
            return Mathf.Max(0, (source.Stats.Level + 1) * 40 - (target.Stats.BaseMagicDefense + target.Stats.BonusMagicDefense) * 2);
        }

        private static int ExplosiveBoulderFormula(Character source, Character target)
        {
            return Mathf.Max(0, (source.Stats.Level + 1) * 60 - (target.Stats.BaseMagicDefense + target.Stats.BonusMagicDefense) * 2);
        }

        private static int CursedGazeFormula(Character source, Character target)
        {
            return Mathf.Max(0, (source.Stats.Level + 1) * 20 - (target.Stats.BaseMagicDefense + target.Stats.BaseMagicDefense));
        }

        private static int Rank1ElementalMagicFormula(Character source, Character target)
        {
            return Mathf.Max(0, (source.Stats.Level + 1) * 10 + source.Stats.BaseMagic + source.Stats.BonusMagic - target.Stats.BaseMagicDefense - target.Stats.BonusMagicDefense);
        }

        private static int DrainSlashFormula(Character source, Character target)
        {
            return 0;
        }

        private static int WeakPointFormula(Character source, Character target)
        {
            return 0;
        }

        private static int MagicalImpactFormula(Character source, Character target)
        {
            return 0;
        }

        private static int HolyWrathFormula(Character source, Character target)
        {
            return 0;
        }

        private static int SinisterStrikeFormula(Character source, Character target)
        {
            return 0;
        }

        private static int BloodyMurderFormula(Character source, Character target)
        {
            return 0;
        }

        private static int AxeThrowFormula(Character source, Character target)
        {
            return 0;
        }

        private static int CleaveFormula(Character source, Character target)
        {
            return 0;
        }

        private static int CrushFormula(Character source, Character target)
        {
            return 0;
        }

        private static int BloodbathFormula(Character source, Character target)
        {
            return 0;
        }

        private static int ExecuteFormula(Character source, Character target)
        {
            return 0;
        }

        private static int ClawStormFormula(Character source, Character target)
        {
            return 0;
        }

        private static int CrystalBreach(Character source, Character target)
        {
            return 0;
        }

        private static int EssenceFluxFormula(Character source, Character target)
        {
            return 0;
        }

        private static int VoidBeamFormula(Character source, Character target)
        {
            return 0;
        }

        private static int PainBlastFormula(Character source, Character target)
        {
            return 0;
        }

        private static int FlareFormula(Character source, Character target)
        {
            return 0;
        }

        private static int DeadlySpinFormula(Character source, Character target)
        {
            return 0;
        }

        private static int ShatterFormula(Character source, Character target)
        {
            return 0;
        }

        private static int ImpairFormula(Character source, Character target)
        {
            return 0;
        }

        private static int SwiftStrikeFormula(Character source, Character target)
        {
            return 0;
        }
    }
}
