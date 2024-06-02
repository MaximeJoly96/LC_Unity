using UnityEngine;
using Engine.Events;
using Dialogs;
using UnityEngine.Events;

namespace Engine.Message
{
    public enum DialogBoxStyle
    {
        Classic,
        Transparent
    }

    public enum DialogBoxPosition
    {
        Bottom,
        Middle,
        Top
    }

    public class DisplayDialog : IRunnable
    {
        public string Locutor { get; set; }
        public string Message { get; set; }

        public DialogBoxStyle BoxStyle { get; set; }
        public DialogBoxPosition BoxPosition { get; set; }
        public string FaceGraphics { get; set; }
        public UnityEvent Finished { get; set; }

        public DisplayDialog()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {
            Object.FindObjectOfType<DialogBoxController>().CreateItem(this);
        }
    }
}
