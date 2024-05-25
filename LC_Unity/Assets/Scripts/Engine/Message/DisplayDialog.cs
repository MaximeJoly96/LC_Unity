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
        public string Locutor;
        public string Message;

        public DialogBoxStyle BoxStyle;
        public DialogBoxPosition BoxPosition;
        public Color BackgroundColor;
        public string FaceGraphics;

        public void Run()
        {
            
        }
    }
}
