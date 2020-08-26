using RPG_Maker_VX_Ace_Import.Database.System;
using System.Collections.Generic;

namespace RPG_Maker_VX_Ace_Import.Database.Battlers
{
    public class Battler
    {
        private List<BattlerStat> _stats;
        private int _level;

        public List<BattlerStat> Stats { get { return _stats; } }
        public int Level { get { return _level; } }

        public Battler(int level)
        {
            _stats = new List<BattlerStat>();
            _level = level;
        }

        public void AddNewStat(BattlerStat stat)
        {
            _stats.Add(stat);
        }
    }
}

