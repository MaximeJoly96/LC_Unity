using UnityEngine;
using UI;
using UnityEngine.UI;
using Questing;
using Language;

namespace Menus.SubMenus.Quests
{
    public class SelectableQuest : SelectableItem
    {
        [SerializeField]
        private Image _icon;

        public Quest QuestData { get; private set; }

        public void Feed(Quest quest)
        {
            QuestData = quest;

            _label.text = Localizer.Instance.GetString(quest.NameKey);

            QuestsWrapper wrapper = FindObjectOfType<QuestsWrapper>();
            if(wrapper)
                _icon.sprite = wrapper.GetSpriteForQuestType(quest.Type);
        }

        public override void ShowCursor(bool show)
        {
            base.ShowCursor(show);
            GetComponent<Animator>().Play(show ? "Hovered" : "Idle");
        }
    }
}
