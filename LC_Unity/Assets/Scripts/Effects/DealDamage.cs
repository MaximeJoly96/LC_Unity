using Abilities;
using Actors;

namespace Effects
{
    public class DealDamage : IEffect
    {
        public int FormulaId { get; set; }

        public string GetDescription()
        {
            return "";
        }

        public void Apply(Character source, Character target)
        {
            target.ChangeHealth(DamageFormula.ComputeResult(FormulaId, source, target));
        }
    }
}
