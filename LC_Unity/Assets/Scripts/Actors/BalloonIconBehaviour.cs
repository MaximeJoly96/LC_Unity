using UnityEngine;
using UnityEngine.Events;

namespace Actors
{
    public class BalloonIconBehaviour : MonoBehaviour
    {
        private UnityEvent<BalloonIconBehaviour> _finishedShowingIcon;

        public UnityEvent<BalloonIconBehaviour> FinishedShowingIcon
        {
            get
            {
                if (_finishedShowingIcon == null)
                    _finishedShowingIcon = new UnityEvent<BalloonIconBehaviour>();

                return _finishedShowingIcon;
            }
        }

        public void FinishedPlaying()
        {
            FinishedShowingIcon.Invoke(this);
        }
    }
}
