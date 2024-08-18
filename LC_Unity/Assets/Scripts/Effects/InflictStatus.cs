﻿using Actors;
using Language;

namespace Effects
{
    public class InflictStatus : IEffect
    {
        public EffectType Value { get; set; }

        public string GetDescription()
        {
            return Localizer.Instance.GetString("inflictStatusDescription") + " " + 
                   Localizer.Instance.GetString(LanguageUtility.GetEffectTypeLanguageKey(Value));
        }
    }
}
