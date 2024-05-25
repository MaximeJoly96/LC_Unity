using UnityEngine;
using Engine.Events;

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
        public Color BackgroundColor { get; set; }
        public string FaceGraphics { get; set; }

        public void Run()
        {
            
        }
    }
}
