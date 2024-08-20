using Language;

namespace Effects
{
    public class Revives : IEffect
    {
        public string GetDescription()
        {
            return Localizer.Instance.GetString("revivesDescription");
        }
    }
}
