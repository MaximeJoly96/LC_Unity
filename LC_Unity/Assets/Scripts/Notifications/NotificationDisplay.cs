using Engine.Party;
using Inventory;
using UnityEngine;

namespace Notifications
{
    public class NotificationDisplay : MonoBehaviour
    {
        [SerializeField]
        private ItemNotification _itemNotificationPrefab;

        private ItemsWrapper _itemsWrapper;

        public ItemsWrapper ItemsWrapper
        {
            get
            {
                if(!_itemsWrapper)
                    _itemsWrapper = FindObjectOfType<ItemsWrapper>();

                return _itemsWrapper;
            }
        }

        public void ShowItemNotification(ChangeItems changeItems)
        {
            ItemNotification notif = Instantiate(_itemNotificationPrefab, transform);
            notif.Feed(ItemsWrapper.GetItemFromId(changeItems.Id), changeItems.Quantity);

            notif.Show();
        }
    }
}
