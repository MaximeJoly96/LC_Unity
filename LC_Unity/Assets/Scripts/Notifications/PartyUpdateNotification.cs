using Language;
using TMPro;
using UnityEngine;
using Actors;

namespace Notifications
{
    public class PartyUpdateNotification : Notification
    {
        [SerializeField]
        private TMP_Text _text;

        public void Feed(Character character, bool add)
        {
            string extraKey = add ? "addedToParty" : "removedFromParty";
            _text.text = character.Name + " " + Localizer.Instance.GetString(extraKey);
        }
    }
}
