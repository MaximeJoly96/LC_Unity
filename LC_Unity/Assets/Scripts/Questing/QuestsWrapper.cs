﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Questing
{
    [Serializable]
    public struct QuestTypeDisplay
    {
        public QuestType QuestType;
        public Sprite Icon;
    }

    public class QuestsWrapper : MonoBehaviour
    {
        [SerializeField]
        private TextAsset _mainQuests;
        [SerializeField]
        private TextAsset _sideQuests;
        [SerializeField]
        private TextAsset _bounties;
        [SerializeField]
        private TextAsset _professionQuests;

        private List<Quest> _allQuests;

        [SerializeField]
        private QuestTypeDisplay[] _questTypesIcons;

        private List<Quest> AllQuests
        {
            get
            {
                if (_allQuests == null)
                    _allQuests = InitAllQuests();

                return _allQuests;
            }
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public Quest GetQuest(int id)
        {
            return AllQuests.FirstOrDefault(q => q.Id == id);
        }

        private List<Quest> InitAllQuests()
        {
            List<Quest> quests = new List<Quest>();

            quests.AddRange(QuestsParser.ParseAllQuests(_mainQuests));
            quests.AddRange(QuestsParser.ParseAllQuests(_sideQuests));
            quests.AddRange(QuestsParser.ParseAllQuests(_bounties));
            quests.AddRange(QuestsParser.ParseAllQuests(_professionQuests));

            return quests;
        }

        public void Feed(TextAsset mainQuests, TextAsset sideQuests, TextAsset bounties, TextAsset professionQuests)
        {
            _mainQuests = mainQuests;
            _sideQuests = sideQuests;
            _bounties = bounties;
            _professionQuests = professionQuests;
        }

        public Sprite GetSpriteForQuestType(QuestType type)
        {
            return _questTypesIcons.FirstOrDefault(t => t.QuestType == type).Icon;
        }
    }
}
