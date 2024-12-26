using Actors;

namespace Effects
{
    public class ElementalDamage : IEffect
    {
        public Element Element { get; set; }

        public string GetDescription()
        {
            return "";
        }
    }
}
