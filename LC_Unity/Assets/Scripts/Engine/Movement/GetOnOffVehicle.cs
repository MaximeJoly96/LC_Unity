using Engine.Events;
using UnityEngine.Events;

namespace Engine.Movement
{
    public class GetOnOffVehicle : IRunnable
    {
        public UnityEvent Finished { get; set; }

        public GetOnOffVehicle()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {

        }
    }
}
