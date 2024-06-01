using System.Collections;
using UnityEngine;
using Engine.Message;

namespace Dialogs
{
    public class ChoiceListController : CanvasMessageController
    {
        private ChoiceListBox _currentChoiceList;

        [SerializeField]
        private ChoiceListBox _choiceListPrefab;

        protected override void Awake()
        {
            base.Awake();
        }

        public void CreateChoiceList(DisplayChoiceList list)
        {
            _currentChoiceList = Instantiate(_choiceListPrefab, _canvas.transform);

            _currentChoiceList.Feed(list);
            _currentChoiceList.Open();
        }
    }
}