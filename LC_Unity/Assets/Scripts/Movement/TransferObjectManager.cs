using Engine.Movement;
using UnityEngine;
using Core;
using Field;

namespace Movement
{
    public class TransferObjectManager : MonoBehaviour
    {
        public void MoveObject(TransferObject transferObject)
        {
            GlobalStateMachine.Instance.CurrentMapId = transferObject.MapId;
            FindObjectOfType<FieldBuilder>().TransferObject(transferObject);
            transferObject.Finished.Invoke();
            transferObject.IsFinished = true;
        }
    }
}
