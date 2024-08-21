using UnityEngine;
using Actors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils
{
    [Serializable]
    public class ElementDisplay
    {
        public Element Element;
        public Sprite Icon;
    }

    public class ElementsWrapper : IconsWrapper
    {
        [SerializeField]
        private List<ElementDisplay> _elements;

        public Sprite GetSpriteFromElement(Element element)
        {
            return _elements.FirstOrDefault(e => e.Element == element).Icon;
        }
    }
}
