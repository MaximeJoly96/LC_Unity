using Actors;
using Language;
using System.Collections.Generic;

namespace Effects
{
    public class ElementalAbilitiesCostReduction : IEffect
    {
        public List<Element> Elements { get; private set; }
        public float Value { get; set; }

        public ElementalAbilitiesCostReduction()
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

        public string GetDescription()
        {
            string label = Localizer.Instance.GetString("elementalAbilitiesCostReductionDescription") + "\n";

            foreach (Element element in Elements)
            {
                label += Localizer.Instance.GetString(element.ToString().ToLower()) + " (" + Value.ToString() + ")\n";
            }

            return label;
        }
    }
}
