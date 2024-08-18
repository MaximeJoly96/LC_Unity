using Language;

namespace Effects
{
    public class ConeAttackWithBullets : IEffect
    {
        public float Angle { get; set; }
        public int Bullets { get; set; }
        public float DamageReduction { get; set; }

        public string GetDescription()
        {
            string angleLabel = Localizer.Instance.GetString("coneAttackWithBulletsAngle") + " " + Angle + "°";
            string bulletsLabel = Localizer.Instance.GetString("coneAttackWithBulletsBullets") + " " + Bullets;
            string damageReductionLabel = Localizer.Instance.GetString("coneAttackWithBulletsDmgReduction") + " " + DamageReduction + "%";

            return angleLabel + "\n" + bulletsLabel + "\n" + damageReductionLabel;
        }
    }
}
