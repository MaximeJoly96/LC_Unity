using UnityEngine.Events;

namespace TitleScreen
{
    public class BackButton : SpecificTitleScreenOption
    {
        private UnityEvent _backButtonSelected;
        public UnityEvent BackButtonSelected
        {
            get
            {
                if (_backButtonSelected == null)
                    _backButtonSelected = new UnityEvent();

                return _backButtonSelected;
            }
        }

        public override void MoveCursorLeft()
        {

        }

        public override void MoveCursorRight()
        {

        }

        public override void Select()
        {
            BackButtonSelected.Invoke();
        }
    }
}
