using UnityEngine;
using RPG_Maker_VX_Ace_Import.Messages;

namespace LC_Unity.Interactions
{
    public class InteractibleNpc : InteractibleTerrainElement
    {
        [SerializeField]
        private DisplayDialogMessageController _displayMessage;

        public override void Interact(GameObject source)
        {
            _displayMessage.DisplayDialog();
        }
    }
}

