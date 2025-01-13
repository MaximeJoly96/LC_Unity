using Engine.SceneControl;

namespace BattleSystem
{
    public class BattleDataHolder
    {
        private static BattleDataHolder _instance;

        public static BattleDataHolder Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new BattleDataHolder();

                return _instance;
            }
        }

        public BattleProcessing BattleData { get; set; }
        public bool Loading { get; set; }

        private BattleDataHolder()
        {

        }
    }
}
