using UnityEngine;
using Engine.Movement;
using Field;
using System.Collections;

namespace Movement
{
    public class MoveRouteRunner : MonoBehaviour
    {
        public void MoveAgent(SetMoveRoute route)
        {
            Agent agent = AgentsManager.Instance.GetAgent(route.AgentId);

            StartCoroutine(StartMoveAgent(agent, route));
        }

        private IEnumerator StartMoveAgent(Agent agent, SetMoveRoute route)
        {
            if(!route.WaitForCompletion)
            {
                route.Finished.Invoke();
                route.IsFinished = true;
            }

            for (int i = 0; i < route.Moves.Count; i++)
            {
                route.Moves[i].Run(agent);

                yield return new WaitUntil(() => route.Moves[i].IsFinished);
            }

            if(route.WaitForCompletion)
            {
                route.Finished.Invoke();
                route.IsFinished = true;
            }
        }
    }
}
