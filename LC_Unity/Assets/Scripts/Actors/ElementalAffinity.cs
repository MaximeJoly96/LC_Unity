using System;

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
            if (multiplier < 0.0f)
                throw new ArgumentException("Provided multiplier for elemental affinity " + element.ToString() + " is less than 0.");

            Element = element;
            Multiplier = multiplier;
        }
    }
}
