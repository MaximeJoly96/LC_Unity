using Engine.Events;
using UnityEngine.Events;
using UnityEngine;
using Dialogs;

namespace Engine.Message
{
    public class DisplayInputNumber : IRunnable
    {
        public int DigitsCount { get; set; }
        public UnityEvent Finished { get; set; }

        public DisplayInputNumber()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {
            Object.FindObjectOfType<InputNumberController>().CreateInputNumber(this);
        }
    }
}
