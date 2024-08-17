using Engine.Events;
using UnityEngine.Events;
using UnityEngine;
using Shop;

namespace Engine.SceneControl
{
    public class ShopProcessing : IRunnable
    {
        public int MerchantId { get; set; }
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        public ShopProcessing()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {
            ShopLoader loader = Object.FindObjectOfType<ShopLoader>();
            loader.FinishedClosingShop.AddListener(Conclude);
            loader.LoadShop(this);
        }

        private void Conclude()
        {
            Finished.Invoke();
            IsFinished = true;
        }
    }
}
