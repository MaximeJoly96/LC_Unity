﻿using UnityEngine;
using UnityEngine.Events;

namespace BattleSystem.Behaviours
{
    public class AttackAnimationBehaviour : MonoBehaviour
    {
        private UnityEvent _abilityHitEvent;
        private UnityEvent _animationEndedEvent;

        public UnityEvent AbilityHitEvent
        {
            get
            {
                if (_abilityHitEvent == null)
                    _abilityHitEvent = new UnityEvent();

                return _abilityHitEvent;
            }
        }

        public UnityEvent AnimationEndedEvent
        {
            get
            {
                if(_animationEndedEvent == null)
                    _animationEndedEvent = new UnityEvent();

                return _animationEndedEvent;
            }
        }

        public void Hit()
        {
            AbilityHitEvent.Invoke();
        }

        public void AnimationEnded()
        {
            AnimationEndedEvent.Invoke();
        }
    }
}