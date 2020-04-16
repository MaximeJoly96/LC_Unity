using UnityEngine;

namespace RPG_Maker_VX_Ace_Import.Messages
{
    public class DisplayDialogMessageController : MonoBehaviour
    {
        [SerializeField]
        private DisplayDialogMessageModel _message;

        private DialogBuilder _builder;

        private void Awake()
        {
            _builder = FindObjectOfType<DialogBuilder>();
            _builder.BuildDialog(_message);
        }
    }
}
