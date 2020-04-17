using UnityEngine;
using System;

namespace RPG_Maker_VX_Ace_Import.Messages
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

    [Serializable]
    public class DisplayDialogMessageModel
    {
        public string locutor;
        [TextArea]
        public string message;

        public DialogBoxStyle style;
        public DialogBoxPosition position;
        public Color backgroundColor;

        public DisplayDialogMessageModel(string locutor, string message, DialogBoxStyle style, DialogBoxPosition position, Color backgroundColor)
        {
            this.locutor = locutor;
            this.message = message;
            this.style = style;
            this.position = position;
            this.backgroundColor = backgroundColor;
        }
    }
}
