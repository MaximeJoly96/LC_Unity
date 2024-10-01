using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using BattleSystem.Model;
using System.Linq;

namespace BattleSystem.UI
{
    public class TimelineUiController : MonoBehaviour
    {
        [SerializeField]
        private Transform _characters;
        [SerializeField]
        private BattlerTimeline _battlerTimelinePrefab;

        private List<BattlerBehaviour> _battlers;
        private List<BattlerTimeline> _battlersTimelines;
        private Animator _animator;

        private Animator Animator
        {
            get
            {
                if(!_animator)
                    _animator = GetComponent<Animator>();

                return _animator;
            }
        }
        public List<BattlerTimeline> Timelines { get { return _battlersTimelines.Where(t => !t.Battler.IsDead).ToList(); } }
        public int BattlersCount { get { return _battlers.Count; } }
        public int TimelinesCount { get { return _battlersTimelines.Count; } }

        public void Feed(List<BattlerBehaviour> battlers)
        {
            Feed(battlers, _characters, _battlerTimelinePrefab);
        }

        public void Feed(List<BattlerBehaviour> battlers, Transform characters, BattlerTimeline timelinePrefab)
        {
            _characters = characters;
            _battlers = battlers;
            _battlersTimelines = new List<BattlerTimeline>();

            for (int i = 0; i < battlers.Count; i++)
            {
                BattlerTimeline timeline = Instantiate(timelinePrefab, characters);
                timeline.Feed(battlers[i]);

                _battlersTimelines.Add(timeline);
            }
        }

        public void Show()
        {
            _characters.gameObject.SetActive(true);
            if(Animator)
                Animator.Play("ShowTimeline");
        }

        public void FinishedOpening()
        {
            _characters.gameObject.SetActive(true);
        }

        public void UpdateTimeline()
        {
            List<TimelineAction> actions = new List<TimelineAction>();

            for(int i = 0; i < _battlers.Count; i++)
            {
                if (_battlers[i].LockedInAbility != null)
                {
                    TimelineAction action = _battlersTimelines[i].ComputeAction(_battlers[i]);
                    action.Priority = _battlers[i].LockedInAbility.Priority;
                    actions.Add(action);
                }  
            }

            var groupedByPriority = actions.GroupBy(a => a.Priority).ToDictionary(group => group.Key, group => group.OrderBy(g => g.StartPoint).ToList());
            List<TimelineSegment> segments = new List<TimelineSegment>();

            foreach (KeyValuePair<int, List<TimelineAction>> kvp in groupedByPriority)
            {
                TimelineSegment segment = new TimelineSegment();
                segment.Priority = kvp.Key;
                segment.Actions = kvp.Value;
                segments.Add(segment);
            }

            float totalLength = segments.Sum(s => s.Length);
            float currentOffset = 0.0f;

            for (int i = 0; i < segments.Count; i++)
            {
                for(int j = 0; j < segments[i].Actions.Count; j++)
                {
                    BattlerTimeline battlerTimeline = _battlersTimelines.FirstOrDefault(bt => bt.Action == segments[i].Actions[j]);
                    if(battlerTimeline != null)
                    {
                        battlerTimeline.Action = new TimelineAction(segments[i].Actions[j].Length,
                                                                    segments[i].Actions[j].StartPoint + currentOffset,
                                                                    segments[i].Actions[j].Priority);
                        battlerTimeline.DrawTimeline(totalLength);
                    }
                }

                currentOffset += segments[i].Length;
            }
        }
    }
}
