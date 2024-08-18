using Language;

namespace Effects
{
    public class PiercingProjectile : IEffect
    {
        public float DamageReduction { get; set; }
        public float MinDamage { get; set; }

        public string GetDescription()
        {
            return Localizer.Instance.GetString("piercingProjectileDescription");
        }
    }
}
