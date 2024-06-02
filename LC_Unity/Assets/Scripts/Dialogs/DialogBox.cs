using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Engine.Message;

namespace Dialogs
{
    public class DialogBox : UiBox<DisplayDialog>
    {
        private const float DIALOG_BOX_HEIGHT = 200.0f; // pixels

        [SerializeField]
        private Image _border;
        [SerializeField]
        private TMP_Text _text;
        [SerializeField]
        private Image _cursor;
        [SerializeField]
        private LocutorBox _locutorBox;

        public bool HasLocutor { get { return _locutorBox.Displayed; } }
        public Image Background { get { return GetComponent<Image>(); } }
        public float Height
        {
            get
            {
                return DIALOG_BOX_HEIGHT + _locutorBox.Height;
            }
        }

        protected override string OpenAnimatioName { get { return "DialogBoxOpen"; } }
        protected override string CloseAnimationName { get { return "DialogBoxClose"; } }

        public override void  Feed(DisplayDialog element)
        {
            base.Feed(element);
            _element.Message = _element.Message.Replace("\\n", "<br>");

            SetStyle();
            SetPosition();
        }

        public void SetPosition()
        {
            RectTransform rt = GetComponent<RectTransform>();
            float yOffset = rt.anchoredPosition.y;

            switch (_element.BoxPosition)
            {
                case DialogBoxPosition.Bottom:
                    rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, yOffset);
                    break;
                case DialogBoxPosition.Middle:
                    rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, (Screen.height - Height) / 2.0f);
                    break;
                case DialogBoxPosition.Top:
                    rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, Screen.height - yOffset - Height);
                    break;
            }
        }

        public void SetStyle()
        {
            switch(_element.BoxStyle)
            {
                case DialogBoxStyle.Classic:
                    break;
                case DialogBoxStyle.Transparent:
                    SetBackground(Color.clear);
                    _border.gameObject.SetActive(false);
                    break;
            }
        }

        public void SetMessage()
        {
            StartCoroutine(AnimateText(_text, _element.Message));
        }

        public void SetLocutor()
        {
            if(string.IsNullOrEmpty(_element.Locutor))
            {
                _locutorBox.Hide();
                return;
            }

            _locutorBox.SetName(_element.Locutor);
        }

        public void SetBackground(Color color)
        {
            Background.color = color;
        }

        public void SetFaceGraphic()
        {

        }

        public void Animate()
        {
            _cursor.GetComponent<Animator>().Play("Animated");
        }

        public override void Close()
        {
            _locutorBox.Hide();
            _text.gameObject.SetActive(false);
            base.Close();
        }

        public override void FinishedOpening()
        {
            if(_locutorBox.Displayed)
            {
                _locutorBox.Open();
                SetLocutor();
            }

            SetMessage();
            DisplayCursor();
        }

        private void DisplayCursor()
        {
            _cursor.gameObject.SetActive(true);
            Animate();
        }
    }
}
