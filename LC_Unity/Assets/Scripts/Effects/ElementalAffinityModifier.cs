using Actors;

namespace Effects
{
    public class ElementalAffinityModifier : IEffect
    {
        public float Value { get; set; }
        public Element Element { get; set; }
    }
}
