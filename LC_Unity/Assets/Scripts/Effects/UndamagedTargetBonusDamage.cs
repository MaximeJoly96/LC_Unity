using Language;

namespace Effects
{
    public class UndamagedTargetBonusDamage : IEffect
    {
        public float Value { get; set; }

        public string GetDescription()
        {
            return Localizer.Instance.GetString("undamagedTargetBonusDamage") + " " + Value + "%";
        }
    }
}
