using UnityEngine;
using Engine.Character;
using Utils;
using Field;

namespace Actors
{
    public class BalloonIconsManager : MonoBehaviour
    {
        public void ShowBalloonIcon(ShowBalloonIcon sbi)
        {
            BalloonIconDisplay icon = FindObjectOfType<BalloonIconsWrapper>().GetBalloonIcon(sbi.BalloonIcon);
            
            Agent agent = AgentsManager.Instance.GetAgent(sbi.AgentId.ToString());
            Transform instIcon = Instantiate(icon.BalloonIcon, agent.BalloonIconSpot, Quaternion.identity);
            Animator animator = instIcon.GetComponent<Animator>();

            BalloonIconBehaviour balloon = instIcon.GetComponent<BalloonIconBehaviour>();

            balloon.FinishedShowingIcon.AddListener((balloon) =>
            {
                sbi.Finished.Invoke();
                sbi.IsFinished = true;
            });
            balloon.FinishedShowingIcon.AddListener(DestroyBalloon);

            animator.Play("Show");
        }

        private void DestroyBalloon(BalloonIconBehaviour balloon)
        {
            Destroy(balloon.gameObject);
        }
    }
}
