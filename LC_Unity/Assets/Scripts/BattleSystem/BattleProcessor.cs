using UnityEngine;
using BattleSystem.UI;
using System.Collections.Generic;
using System.Collections;
using Abilities;
using Utils;

namespace BattleSystem
{
    public class BattleProcessor : MonoBehaviour
    {
        private List<BattlerTimeline> _timelines;
        private bool _battleOn;
        private float _battleTimer;

        public void ProcessBattle(List<BattlerTimeline> timelines)
        {
            _timelines = timelines;
            _timelines.ForEach(t => t.Processed = false);
            _battleOn = true;
            _battleTimer = 0.0f;
        }

        private void Update()
        {
            if(_battleOn)
            {
                for(int i = 0; i < _timelines.Count; i++)
                {
                    if (!_timelines[i].Processed && _timelines[i].Action.StartPoint <= _battleTimer)
                    {
                        ProcessTimeline(_timelines[i]);
                    }
                }

                _battleTimer += Time.deltaTime;
            }
        }

        private void ProcessTimeline(BattlerTimeline timeline)
        {
            timeline.Processed = true;

            if (timeline.Battler.LockedInAbility.Targets.Count > 0)
            {
                StartCoroutine(MoveToTarget(timeline.Battler,
                                            timeline.Battler.LockedInAbility.Targets[0],
                                            timeline.Battler.LockedInAbility.Range));
            }
            else
                timeline.Battler.FinishedTurn();
        }

        private IEnumerator MoveToTarget(BattlerBehaviour source, BattlerBehaviour target, int range)
        {
            Animator animator = source.GetComponent<Animator>();
            animator.SetBool("Moving", true);

            while(Vector2.Distance(source.transform.position, target.transform.position) > MeasuresConverter.RangeToWorldUnits(range))
            {
                Vector2 direction = (target.transform.position - source.transform.position).normalized;
                animator.SetFloat("X", direction.x);
                animator.SetFloat("Y", direction.y);

                source.transform.Translate(direction * Time.deltaTime);
                yield return null;
            }

            animator.SetBool("Moving", false);
            source.FinishedAbilityMovement(target);
        }
    }
}
