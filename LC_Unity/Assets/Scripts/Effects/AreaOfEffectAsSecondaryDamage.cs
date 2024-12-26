using Actors;
using Language;

namespace Effects
{
    public class AreaOfEffectAsSecondaryDamage : IEffect
    {
        public Element Element { get; set; }
        public int FormulaId { get; set; }

        public string GetDescription()
        {
            return Localizer.Instance.GetString("areaOfEffectAsSecondaryDamageDescription") + " " +
                   Localizer.Instance.GetString(Element.ToString().ToLower()); 
        }
    }
}
