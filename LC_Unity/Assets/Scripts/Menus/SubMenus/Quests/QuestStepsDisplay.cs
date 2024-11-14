using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Questing;
using System.Collections.Generic;
using Codice.Client.BaseCommands.BranchExplorer;

namespace Menus.SubMenus.Quests
{
    public class QuestStepsDisplay : MonoBehaviour
    {
        [SerializeField]
        private IndividualQuestStepDisplay _stepPrefab;

        private List<IndividualQuestStepDisplay> _steps;

        public List<IndividualQuestStepDisplay> Steps
        {
            get { return _steps; }
        }

        public void Init(List<QuestStep> questSteps)
        {
            Clear();

            for(int i = 0; i < questSteps.Count; i++)
            {

            }
        }

        public void Clear()
        {
            _steps = new List<IndividualQuestStepDisplay>();

            foreach(Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
