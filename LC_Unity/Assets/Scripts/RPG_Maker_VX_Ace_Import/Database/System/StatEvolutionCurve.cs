using System;
using UnityEngine;

namespace RPG_Maker_VX_Ace_Import.Database.System
{
    [Serializable]
    public class StatEvolutionCurve
    {
        [SerializeField]
        private float _squareXCoeff;
        [SerializeField]
        private float _xCoeff;
        [SerializeField]
        private float _independantTerm;

        public float SquareXCoeff { get { return _squareXCoeff; } }
        public float XCoeff { get { return _xCoeff; } }
        public float IndependantTerm { get { return _independantTerm; } }

        public float GetValueBasedOnLevel(int level)
        {
            return Mathf.Pow(level, 2) * SquareXCoeff + level * XCoeff + IndependantTerm;
        }

        public StatEvolutionCurve(float squareX, float x, float indieTerm)
        {
            _squareXCoeff = squareX;
            _xCoeff = x;
            _independantTerm = indieTerm;
        }
    }
}
