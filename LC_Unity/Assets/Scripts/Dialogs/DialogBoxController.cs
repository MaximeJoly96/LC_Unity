using UnityEngine;
using Engine.Message;

namespace Dialogs
{
    public class DialogBoxController : MonoBehaviour
    {
        [SerializeField]
        private DialogBox _dialogBoxPrefab;
        [SerializeField]
        private Canvas _dialogBoxCanvas;

        public void CreateDialog(DisplayDialog dialog)
        {
            DialogBox inst = Instantiate(_dialogBoxPrefab, _dialogBoxCanvas.transform);

            inst.Feed(dialog);
            inst.Open();
        }
    }
}
