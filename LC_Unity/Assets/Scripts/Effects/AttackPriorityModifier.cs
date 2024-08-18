using Language;

namespace Effects
{
    public class AttackPriorityModifier : IEffect
    {
        public int Value { get; set; }

        public string GetDescription()
        {
            string priority = Value > 0 ? "+" + Value.ToString() : Value.ToString();
            return Localizer.Instance.GetString("attackPriorityModifierDescription") + " " + priority;
        }
    }
}
