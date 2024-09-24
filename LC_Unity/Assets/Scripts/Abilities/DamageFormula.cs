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
                    return AttackCommandFormula(source, target);
            }

            return 0;
        }

        private static int AttackCommandFormula(Character source, Character target)
        {
            return Mathf.Max(0, Mathf.RoundToInt(((source.BaseStrength + source.BonusStrength) * 2.0f - target.BaseDefense + target.BonusDefense) 
                                * (1 + source.Level * 0.025f)));
        }
    }
}
