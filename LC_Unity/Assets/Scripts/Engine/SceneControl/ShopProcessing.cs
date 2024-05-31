using Engine.Events;
using UnityEngine.Events;

namespace Engine.SceneControl
{
    public class ShopProcessing : IRunnable
    {
        public int MerchantId { get; set; }
        public UnityEvent Finished { get; set; }

        public ShopProcessing()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {

        }
    }
}
