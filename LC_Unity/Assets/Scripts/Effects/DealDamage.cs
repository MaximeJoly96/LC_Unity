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
            target.ChangeHealth(Compute(source, target));
        }

        public int Compute(Character source, Character target)
        {
            return DamageFormula.ComputeResult(FormulaId, source, target);
        }
    }
}
