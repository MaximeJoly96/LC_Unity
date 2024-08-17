using UnityEngine;
using Engine.SceneControl;
using Core;
using UnityEngine.Events;

namespace Shop
{
    public class ShopLoader : MonoBehaviour
    {
        private UnityEvent _finishedClosingShop;

        public UnityEvent FinishedClosingShop
        {
            get
            {
                if (_finishedClosingShop == null)
                    _finishedClosingShop = new UnityEvent();

                return _finishedClosingShop;
            }
        }

        private CanvasGroup _canvasGroup
        {
            get { return GetComponent<CanvasGroup>(); }
        }

        private Animator _animator
        {
            get { return GetComponent<Animator>(); }
        }

        public void LoadShop(ShopProcessing shop)
        {
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.OpeningShop);
            GetComponent<ShopManager>().SetupShop(shop);
            _animator.Play("OpenShop");
        }

        public void CloseShop()
        {
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.ClosingShop);
            _animator.Play("CloseShop");
        }

        public void FinishedOpening()
        {
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.InShop);
        }

        public void FinishedClosing()
        {
            FinishedClosingShop.Invoke();
        }
    }
}
