using UnityEngine.SceneManagement;
using Engine.SceneControl;

namespace BattleSystem
{
    public class BattleLoader
    {
        public void LoadBattle(BattleProcessing battle)
        {
            BattleDataHolder.Instance.BattleData = battle;
            SceneManager.LoadScene("Battle");
        }
    }
}
