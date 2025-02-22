﻿using Engine.Party;
using UnityEngine;

namespace Notifications
{
    public class NotificationCenter
    {
        private NotificationDisplay _notificationDisplay;
        private NotificationDisplay NotificationDisplay
        {
            get
            {
                if(!_notificationDisplay)
                    _notificationDisplay = Object.FindObjectOfType<NotificationDisplay>();

                return _notificationDisplay;
            }
        }

        private static NotificationCenter _instance;

        public static NotificationCenter Instance
        {
            get
            {
                if(_instance == null)
                    _instance = new NotificationCenter();

                return _instance;
            }
        }

        private NotificationCenter()
        {
        }

        public void ShowItemNotification(ChangeItems changeItems)
        {
            if(NotificationDisplay)
                NotificationDisplay.ShowItemNotification(changeItems);
        }

        public void ShowPartyUpdateNotification(ChangePartyMember changeParty)
        {
            if (NotificationDisplay)
                NotificationDisplay.ShowPartyUpdateNotification(changeParty);
        }

        public void ShowGoldNotification(ChangeGold changeGold)
        {
            if (NotificationDisplay)
                NotificationDisplay.ShowGoldNotification(changeGold);
        }
    }
}
