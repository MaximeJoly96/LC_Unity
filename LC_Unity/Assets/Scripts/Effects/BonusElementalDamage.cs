using Actors;
using System.Collections.Generic;
using Language;

namespace Effects
{
    public class BonusElementalDamage : IEffect
    {
        public List<Element> Elements { get; private set; }
        public float Value { get; set; }

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

        public string GetDescription()
        {
            string label = Localizer.Instance.GetString("bonusElementalDamageDescription") + "\n";
            string modifier = Value > 0 ? "+" + Value : Value.ToString();

            foreach(Element element in Elements)
            {
                label += Localizer.Instance.GetString(element.ToString().ToLower()) + " (" + modifier + ")\n";
            }

            return label;
        }
    }
}
