﻿using UnityEngine;
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
                    return AttackCommandFormula(source, target);
                case 2:
                    return ClawsCommandFormula(source, target);
                case 45:
                    return MagickaFormula(source, target);
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
    }
}
