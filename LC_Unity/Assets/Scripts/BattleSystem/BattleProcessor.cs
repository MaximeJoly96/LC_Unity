﻿using UnityEngine;
using BattleSystem.UI;
using System.Collections.Generic;
using System.Collections;
using Abilities;

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

            if(timeline.Battler.LockedInAbility.Id == 0)
            {
                StartCoroutine(MoveToTarget(timeline.Battler.gameObject, timeline.Battler.LockedInAbility.Targets[0].gameObject));
            }
        }

        private IEnumerator MoveToTarget(GameObject source, GameObject target)
        {
            while(Vector2.Distance(source.transform.position, target.transform.position) > 0.05f)
            {
                source.transform.Translate((target.transform.position - source.transform.position).normalized * Time.deltaTime);
                yield return null;
            }
        }
    }
}
