using Language;

namespace Effects
{
    public class HealingItemsEfficiency : IEffect
    {
        public float Value { get; set; }

        public string GetDescription()
        {
            return Localizer.Instance.GetString("healingItemsEfficiencyDescription") + " +" + Value + "%";
        }
    }
}
