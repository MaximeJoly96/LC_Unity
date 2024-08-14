using Actors;
using System.Collections.Generic;

namespace Effects
{
    public class BonusElementalDamage : IEffect
    {
        public List<Element> Elements { get; private set; }

        public BonusElementalDamage()
        {
            Elements = new List<Element>();
        }

        public void AddAll()
        {
            AddElement(Element.Fire);
            AddElement(Element.Ice);
            AddElement(Element.Water);
            AddElement(Element.Thunder);
            AddElement(Element.Wind);
            AddElement(Element.Earth);
            AddElement(Element.Holy);
            AddElement(Element.Darkness);
            AddElement(Element.Healing);
        }

        public void AddElement(Element element)
        {
            Elements.Add(element);
        }
    }
}
