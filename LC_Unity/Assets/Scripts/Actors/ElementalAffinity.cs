namespace Actors
{
    public enum Element
    {
        Fire,
        Ice,
        Water,
        Thunder,
        Earth,
        Wind,
        Holy,
        Darkness,
        Healing,
        Neutral
    }

    public class ElementalAffinity
    {
        public Element Element { get; set; }
        public float Multiplier { get; set; }

        public ElementalAffinity(Element element, float multiplier)
        {
            Element = element;
            Multiplier = multiplier;
        }
    }
}
