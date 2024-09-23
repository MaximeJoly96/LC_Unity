using UnityEngine;
using System;
using System.Linq;

namespace BattleSystem
{
    [Serializable]
    internal class AttackAnimationDisplay
    {
        public int Id;
        public GameObject AttackAnimation;
    }

    public class AttackAnimationsWrapper : MonoBehaviour
    {
        [SerializeField]
        private AttackAnimationDisplay[] _animations;

        public GameObject GetAttackAnimation(int id)
        {
            return _animations.FirstOrDefault(anim => anim.Id == id).AttackAnimation;
        }
    }
}
