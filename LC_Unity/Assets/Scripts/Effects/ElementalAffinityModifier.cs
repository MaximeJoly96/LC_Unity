using Actors;
using Language;

namespace Effects
{
    public class ElementalAffinityModifier : IEffect
    {
        public float Value { get; set; }
        public Element Element { get; set; }

        public string GetDescription()
        {
            return Localizer.Instance.GetString("elementalAffinityModifierDescription") + " " + 
                   Value.ToString() + "(" + Localizer.Instance.GetString(Element.ToString().ToLower()) + ")";
        }
    }
}
