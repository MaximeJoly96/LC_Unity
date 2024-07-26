using System.Collections.Generic;

namespace BattleSystem
{
    public class EnemiesManager
    {
        private List<BattlerBehaviour> _enemies;

        public EnemiesManager(List<BattlerBehaviour> battlers) 
        { 
            _enemies = battlers;
        }

        public void LockAbilities(List<BattlerBehaviour> allBattlers)
        {
            for(int i = 0 ; i < _enemies.Count; i++)
            {
                _enemies[i].Behave(allBattlers);
            }
        }
    }
}
