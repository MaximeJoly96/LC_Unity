using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Engine.Message;
using System.Collections;
using System.Text;

namespace Dialogs
{
    public class DialogBox : MonoBehaviour
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

        private DisplayDialog _dialog;

        public bool HasLocutor { get { return _locutorBox.Displayed; } }
        public Image Background { get { return GetComponent<Image>(); } }
        public Animator Animator { get { return GetComponent<Animator>(); } }
        public float Height
        {
            get
            {
                return DIALOG_BOX_HEIGHT + _locutorBox.Height;
            }
        }

        public void Feed(DisplayDialog dialog)
        {
            _dialog = dialog;
            _dialog.Message = _dialog.Message.Replace("\\n", "<br>");

            SetStyle();
            SetPosition();
        }

        public void SetPosition()
        {
            RectTransform rt = GetComponent<RectTransform>();
            float yOffset = rt.anchoredPosition.y;

            switch (_dialog.BoxPosition)
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
            switch(_dialog.BoxStyle)
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
            StartCoroutine(AnimateText());
        }

        public void SetLocutor()
        {
            if(string.IsNullOrEmpty(_dialog.Locutor))
            {
                _locutorBox.Hide();
                return;
            }

            _locutorBox.SetName(_dialog.Locutor);
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

        public void Open()
        {
            Animator.Play("DialogBoxOpen");
        }

        public void Close()
        {
            _locutorBox.Hide();
            Animator.Play("DialogBoxClose");
        }

        public void FinishedOpening()
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

        private IEnumerator AnimateText()
        {
            StringBuilder builder = new StringBuilder();
            WaitForEndOfFrame wait = new WaitForEndOfFrame();
            
            for(int i = 0; i < _dialog.Message.Length; i++)
            {
                builder = builder.Append(_dialog.Message[i]);
                _text.text = builder.ToString();
                yield return wait;
            }
        }
    }
}
