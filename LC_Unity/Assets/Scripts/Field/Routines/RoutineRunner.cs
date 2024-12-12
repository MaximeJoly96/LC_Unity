using Engine.Movement.Moves;
using UnityEngine;

namespace Field.Routines
{
    public class RoutineRunner : MonoBehaviour
    {
        [SerializeField]
        private TextAsset _routineData;

        private AgentRoutine _routine;
        private int _routineIndex;

        public TextAsset RoutineData { get { return _routineData; } set { _routineData = value; } }
        public Agent Agent { get { return GetComponent<Agent>(); } }

        private void Start()
        {
            _routine = ParseRoutine();
            _routine[_routineIndex].Run(Agent);
        }

        private AgentRoutine ParseRoutine()
        {
            return XmlRoutineParser.ParseRoutine(_routineData);
        }

        private void Update()
        {
            Move currentMove = _routine[_routineIndex];
                
            if(currentMove.IsFinished)
            {
                if(_routineIndex == _routine.Count - 1)
                {
                    _routineIndex = 0;
                    _routine.ForEach(r => r.IsFinished = false);
                }
                else
                {
                    _routineIndex++;
                }

                _routine[_routineIndex].Run(Agent);
            }
        }
    }
}
