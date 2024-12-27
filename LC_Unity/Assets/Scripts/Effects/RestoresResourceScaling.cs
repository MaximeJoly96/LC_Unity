using Actors;
using Language;
using UnityEngine;

namespace Effects
{
    public class RestoresResourceScaling : IEffect
    {
        public Stat Stat { get; set; }
        public float Value { get; set; }

        public string GetDescription()
        {
            return Localizer.Instance.GetString("restoresResourceScaling") + " " + Value + "(" +
                   Localizer.Instance.GetString(LanguageUtility.GetStatLanguageKey(Stat)) + ")";
        }

        public void Apply(Character target)
        {
            float percentage = Value / 100.0f;
            int amount;

            switch (Stat)
            {
                case Stat.HP:
                    amount = Mathf.RoundToInt(percentage * target.Stats.MaxHealth);
                    target.ChangeHealth(-amount);
                    break;
                case Stat.MP:
                    amount = Mathf.RoundToInt(percentage * target.Stats.MaxMana);
                    target.ChangeMana(-amount);
                    break;
                case Stat.EP:
                    amount = Mathf.RoundToInt(percentage * target.Stats.MaxEssence);
                    target.ChangeEssence(-amount);
                    break;
            }
        }
    }
}
