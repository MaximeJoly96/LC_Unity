using UnityEngine;
using UnityEngine.UI;

namespace RPG_Maker_VX_Ace_Import.Messages
{
    public class DialogBuilder : MonoBehaviour
    {
        private static Vector2 LOCUTOR_BOX_STANDARD_SIZE { get { return new Vector2(300.0f, 60.0f); } }
        private static Vector2 BOX_STANDARD_SIZE { get { return new Vector2(960.0f, 200.0f); } }
        private static float SCREEN_MIDDLE_X { get { return Screen.width / 2.0f; } }
        private static float LOCUTOR_BOX_Y_OFFSET { get { return BOX_STANDARD_SIZE.y / 2.0f + LOCUTOR_BOX_STANDARD_SIZE.y / 2.0f; } }

        private const float LOCUTOR_MARGIN_Y = 10.0f;
        private const float LOCUTOR_MARGIN_X = 15.0f;
        private const float MARGIN = 20.0f;
        private const int FONT_SIZE = 32;
        private const float PIXELS_PER_UNIT_MULTIPLIER = 0.75f;

        [SerializeField]
        private Sprite _windowBorder;
        [SerializeField]
        private Canvas _canvas;
        [SerializeField]
        private Font _font;

        private Image _boxBackground;
        private Text _dialogText;

        private Image _locutorBoxBackground;
        private Text _locutorText;

        private Image _locutorFace;

        public void BuildDialog(DisplayDialogMessageModel model)
        {
            CreateBaseDialog(model);
            CreateText(model);
            CreateLocutor(model);
        }

        private void CreateBaseDialog(DisplayDialogMessageModel model)
        {
            GameObject child = new GameObject("Dialog box");
            child.transform.parent = _canvas.transform;

            _boxBackground = child.AddComponent<Image>();

            if(model.style == DialogBoxStyle.Classic)
            {
                _boxBackground.sprite = _windowBorder;
                _boxBackground.type = Image.Type.Sliced;
                _boxBackground.pixelsPerUnitMultiplier = PIXELS_PER_UNIT_MULTIPLIER;
            }
            else
                _boxBackground.color = new Color(_boxBackground.color.r,
                                                 _boxBackground.color.g,
                                                 _boxBackground.color.b,
                                                 0.0f);

            _boxBackground.transform.position = GetDialogPosition(model.position);
            _boxBackground.rectTransform.sizeDelta = BOX_STANDARD_SIZE;

            GameObject extraBgGO = new GameObject("Background");
            extraBgGO.transform.parent = child.transform;

            Image bg = extraBgGO.AddComponent<Image>();
            bg.color = model.backgroundColor * 0.5f;
            bg.transform.position = _boxBackground.transform.position;
            bg.rectTransform.sizeDelta = _boxBackground.rectTransform.sizeDelta;
        }

        private Vector2 GetDialogPosition(DialogBoxPosition position)
        {
            switch(position)
            {
                case DialogBoxPosition.Middle:
                    return new Vector2(SCREEN_MIDDLE_X, Screen.height / 2.0f);
                case DialogBoxPosition.Top:
                    return new Vector2(SCREEN_MIDDLE_X, Screen.height - BOX_STANDARD_SIZE.y / 2.0f - MARGIN);
                case DialogBoxPosition.Bottom:
                default:
                    return new Vector2(SCREEN_MIDDLE_X, BOX_STANDARD_SIZE.y / 2.0f + MARGIN);
            }
        }

        private void CreateText(DisplayDialogMessageModel model)
        {
            GameObject textGO = new GameObject("Dialog Text");
            textGO.transform.parent = _boxBackground.transform;

            _dialogText = textGO.AddComponent<Text>();
            _dialogText.text = model.message;

            _dialogText.transform.localPosition = new Vector3(MARGIN, -1.0f * MARGIN, 0.0f);
            _dialogText.rectTransform.sizeDelta = BOX_STANDARD_SIZE;
            _dialogText.font = _font;
            _dialogText.fontSize = FONT_SIZE;
            _dialogText.color = Color.white;
        }

        private void CreateLocutor(DisplayDialogMessageModel model)
        {
            if(model.locutor != "")
            {
                GameObject locutorBoxGO = new GameObject("Locutor box");
                locutorBoxGO.transform.parent = _canvas.transform;

                _locutorBoxBackground = locutorBoxGO.AddComponent<Image>();

                if (model.style == DialogBoxStyle.Classic)
                {
                    _locutorBoxBackground.sprite = _windowBorder;
                    _locutorBoxBackground.type = Image.Type.Sliced;
                    _locutorBoxBackground.pixelsPerUnitMultiplier = PIXELS_PER_UNIT_MULTIPLIER;
                }
                else
                    _locutorBoxBackground.color = new Color(_locutorBoxBackground.color.r,
                                                            _locutorBoxBackground.color.g,
                                                            _locutorBoxBackground.color.b,
                                                            0.0f);

                _locutorBoxBackground.transform.position = GetLocutorWindowPosition(model.position);
                _locutorBoxBackground.rectTransform.sizeDelta = LOCUTOR_BOX_STANDARD_SIZE;

                GameObject extraBgGO = new GameObject("Locutor Background");
                extraBgGO.transform.parent = locutorBoxGO.transform;

                Image bg = extraBgGO.AddComponent<Image>();
                bg.color = model.backgroundColor * 0.5f;
                bg.transform.position = _locutorBoxBackground.transform.position;
                bg.rectTransform.sizeDelta = _locutorBoxBackground.rectTransform.sizeDelta;

                CreateLocutorText(model.locutor);
            }
        }

        private void CreateLocutorText(string text)
        {
            GameObject locutorTextGO = new GameObject("Locutor text");
            locutorTextGO.transform.parent = _locutorBoxBackground.transform;

            _locutorText = locutorTextGO.AddComponent<Text>();
            _locutorText.text = text;

            _locutorText.transform.localPosition = new Vector3(LOCUTOR_MARGIN_X, -1.0f * LOCUTOR_MARGIN_Y, 0.0f);
            _locutorText.rectTransform.sizeDelta = LOCUTOR_BOX_STANDARD_SIZE;
            _locutorText.font = _font;
            _locutorText.fontSize = FONT_SIZE;
            _locutorText.color = Color.white;
        }

        private Vector2 GetLocutorWindowPosition(DialogBoxPosition position)
        {
            switch(position)
            {
                case DialogBoxPosition.Middle:
                    return new Vector2(SCREEN_MIDDLE_X - BOX_STANDARD_SIZE.x / 2.0f + LOCUTOR_BOX_STANDARD_SIZE.x / 2.0f, 
                                       Screen.height / 2.0f + LOCUTOR_BOX_Y_OFFSET);
                case DialogBoxPosition.Top:
                    return new Vector2(SCREEN_MIDDLE_X + BOX_STANDARD_SIZE.x / 2.0f - LOCUTOR_BOX_STANDARD_SIZE.x / 2.0f, 
                                       Screen.height - BOX_STANDARD_SIZE.y / 2.0f - MARGIN - LOCUTOR_BOX_Y_OFFSET);
                case DialogBoxPosition.Bottom:
                default:
                    return new Vector2(SCREEN_MIDDLE_X - BOX_STANDARD_SIZE.x / 2.0f + LOCUTOR_BOX_STANDARD_SIZE.x / 2.0f, 
                                       BOX_STANDARD_SIZE.y / 2.0f + MARGIN + LOCUTOR_BOX_Y_OFFSET);
            }
        }
    }
}

