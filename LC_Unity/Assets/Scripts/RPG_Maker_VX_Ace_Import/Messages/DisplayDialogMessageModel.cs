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
    }
}
