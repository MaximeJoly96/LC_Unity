using Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils
{
    [Serializable]
    public class EffectTypeDisplay
    {
        public EffectType Effect;
        public Sprite Icon;
    }

    public class EffectTypesWrapper : IconsWrapper
    {
        [SerializeField]
        private List<EffectTypeDisplay> _effects;

        public Sprite GetSpriteFromEffectType(EffectType effect)
        {
            return _effects.FirstOrDefault(e => e.Effect == effect).Icon;
        }
    }
}
