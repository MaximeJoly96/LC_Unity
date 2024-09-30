using UnityEngine;
using Abilities;
using System.Collections.Generic;
using System.Linq;

namespace BattleSystem.Behaviours.AiBehaviours
{
    public class BattlerBaseAi : MonoBehaviour
    {
        [SerializeField]
        private TextAsset _behaviourScript;

        private AiScript _aiScript;

        public Ability Behave(List<BattlerBehaviour> allBattlers)
        {
            if(_aiScript == null)
            {
                BehaviourParser parser = new BehaviourParser();
                _aiScript = parser.ParseBehaviour(_behaviourScript);
            }

            Ability ability = new Ability(AbilitiesManager.Instance.GetAbility(_aiScript.PickAction()));
            ability.Targets = ChoseTargets(allBattlers, ability);

            return ability;
        }

        public List<BattlerBehaviour> ChoseTargets(List<BattlerBehaviour> battlers, Ability ability)
        {
            List<BattlerBehaviour> targets = new List<BattlerBehaviour>();
            BattlerBehaviour actor = GetComponent<BattlerBehaviour>();
            List<BattlerBehaviour> allies = battlers.Where(b => b.IsEnemy == actor.IsEnemy).ToList();
            List<BattlerBehaviour> enemies = battlers.Where(b => b.IsEnemy != actor.IsEnemy).ToList();

            switch (ability.TargetEligibility)
            {
                case TargetEligibility.All:
                    targets.AddRange(battlers);
                    break;
                case TargetEligibility.Self:
                    targets.Add(actor);
                    break;
                case TargetEligibility.Enemy:
                    targets.Add(enemies[_aiScript.Rng.Next(enemies.Count())]);
                    break;
                case TargetEligibility.Ally:
                    targets.Add(allies[_aiScript.Rng.Next(enemies.Count())]);
                    break;
                case TargetEligibility.AllAllies:
                    targets.AddRange(allies);
                    break;
                case TargetEligibility.AllEnemies:
                    targets.AddRange(enemies);
                    break;
            }

            return targets;
        }
    }
}
