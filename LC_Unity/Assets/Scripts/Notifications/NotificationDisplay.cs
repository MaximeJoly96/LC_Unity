using Actors;
using Engine.Party;
using Inventory;
using UnityEngine;

namespace Notifications
{
    public class NotificationDisplay : MonoBehaviour
    {
        [SerializeField]
        private ItemNotification _itemNotificationPrefab;
        [SerializeField]
        private PartyUpdateNotification _partyUpdateNotificationPrefab;
        [SerializeField]
        private GoldNotification _goldNotificationPrefab;

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

        public void ShowPartyUpdateNotification(ChangePartyMember changeParty)
        {
            PartyUpdateNotification notif = Instantiate(_partyUpdateNotificationPrefab, transform);
            notif.Feed(CharactersManager.Instance.GetCharacter(changeParty.Id), changeParty.Action == ChangePartyMember.ActionType.Add);

            notif.Show();
        }

        public void ShowGoldNotification(ChangeGold changeGold)
        {
            GoldNotification notif = Instantiate(_goldNotificationPrefab, transform);
            notif.Feed(changeGold.Value);

            notif.Show();
        }
    }
}
