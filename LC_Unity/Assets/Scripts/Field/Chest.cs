using UnityEngine;
using Inventory;
using Engine.Party;
using Engine.Message;

namespace Field
{
    public class Chest : RunnableAgent
    {
        [SerializeField]
        private int _gold;
        [SerializeField]
        private ItemCategory _itemCategory;
        [SerializeField]
        private int _itemId;
        [SerializeField]
        private int _quantity;

        public override void RunSequence()
        {
            GetComponent<Animator>().Play("Open");
        }

        public void FinishedOpening()
        {
            string message = "Received:\n";
            if (_gold > 0)
                message += _gold + " G\n";

            if (_quantity > 0)
                message += _quantity + " " + _itemId;

            ChangeGold changeGold = new ChangeGold
            {
                Value = _gold
            };

            ChangeItems changeItem = new ChangeItems
            {
                Id = _itemId,
                Quantity = _quantity
            };

            changeGold.Run();
            changeItem.Run();

            DisplayDialog dialog = new DisplayDialog
            {
                Message = message,
                BoxPosition = DialogBoxPosition.Bottom,
                BoxStyle = DialogBoxStyle.Classic,
                Locutor = null,
                FaceGraphics = null
            };

            dialog.Finished.AddListener(() => FinishedSequence.Invoke());
            dialog.Run();
        }
    }
}
