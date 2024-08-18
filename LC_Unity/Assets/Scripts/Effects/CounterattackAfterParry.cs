using Language;

namespace Effects
{
    public class CounterattackAfterParry : IEffect
    {
        public string GetDescription()
        {
            return Localizer.Instance.GetString("counterattackAfterParryDescription");
        }
    }
}
