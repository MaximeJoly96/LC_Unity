namespace RPG_Maker_VX_Ace_Import.Database.System
{
    public class BattlerStat
    {
        private StatEvolutionCurve _curve;
        private CharacterStats _label;

        public StatEvolutionCurve Curve { get { return _curve; } }
        public CharacterStats Label { get { return _label; } }

        public BattlerStat(StatEvolutionCurve curve, CharacterStats label)
        {
            _curve = curve;
            _label = label;
        }

        public float GetValueBasedOnLevel(int level)
        {
            return _curve.GetValueBasedOnLevel(level);
        }
    }
}

