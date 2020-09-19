using UnityEngine;
using UnityEngine.UI;

namespace RPG_Maker_VX_Ace_Import.Messages
{
    public class DialogBuilder : MonoBehaviour
    {
        public static Vector2 LOCUTOR_BOX_STANDARD_SIZE { get { return new Vector2(300.0f, 60.0f); } }
        public static Vector2 BOX_STANDARD_SIZE { get { return new Vector2(960.0f, 200.0f); } }
        public static float SCREEN_MIDDLE_X { get { return Screen.width / 2.0f; } }
        public static float LOCUTOR_BOX_Y_OFFSET { get { return BOX_STANDARD_SIZE.y / 2.0f + LOCUTOR_BOX_STANDARD_SIZE.y / 2.0f; } }
        public static float PIXELS_PER_UNIT_MULTIPLIER { get { return 0.75f; } }
        public static float MARGIN { get { return 20.0f; } }
        public static int FONT_SIZE { get { return 32; } }
        public static float LOCUTOR_MARGIN_Y { get { return 10.0f; } }
        public static float LOCUTOR_MARGIN_X { get { return 15.0f; } }
        
        [SerializeField]
        private Sprite _windowBorder;
        [SerializeField]
        private Canvas _canvas;
        [SerializeField]
        private Font _font;

        public Image BoxBackground { get; protected set; }
        public Image LocutorBoxBackground { get; protected set; }
        public Text DialogText { get; protected set; }
        public Text LocutorText { get; protected set; }

        public void BuildDialog(DisplayDialogMessageModel model)
        {
            CreateBaseDialog(model);
            CreateText(model);
            CreateLocutor(model);
        }

        public void CreateBaseDialog(DisplayDialogMessageModel model, Canvas canvas)
        {
            GameObject child = new GameObject("Dialog box");
            child.transform.parent = canvas.transform;

            BoxBackground = child.AddComponent<Image>();

            if (model.style == DialogBoxStyle.Classic)
            {
                BoxBackground.sprite = _windowBorder;
                BoxBackground.type = Image.Type.Sliced;
                BoxBackground.pixelsPerUnitMultiplier = PIXELS_PER_UNIT_MULTIPLIER;
            }
            else
                BoxBackground.color = new Color(BoxBackground.color.r,
                                                BoxBackground.color.g,
                                                BoxBackground.color.b,
                                                0.0f);

            BoxBackground.transform.position = GetDialogPosition(model.position);
            BoxBackground.rectTransform.sizeDelta = BOX_STANDARD_SIZE;

            AddExtraBackgroundToBox(model, child);
        }

        public void CreateBaseDialog(DisplayDialogMessageModel model)
        {
            CreateBaseDialog(model, _canvas);
        }

        public void AddExtraBackgroundToBox(DisplayDialogMessageModel model, GameObject parent)
        {
            GameObject extraBgGO = new GameObject("Background");
            extraBgGO.transform.parent = parent.transform;

            Image bg = extraBgGO.AddComponent<Image>();
            bg.color = model.backgroundColor * 0.5f;
            bg.transform.position = parent.transform.position;
            bg.rectTransform.sizeDelta = parent.GetComponent<Image>().rectTransform.sizeDelta;
        }

        public static Vector2 GetDialogPosition(DialogBoxPosition position)
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

        public void CreateText(DisplayDialogMessageModel model)
        {
            GameObject textGO = new GameObject("Dialog Text");
            textGO.transform.parent = BoxBackground.transform;

            DialogText = textGO.AddComponent<Text>();
            DialogText.text = model.message;

            DialogText.transform.localPosition = new Vector3(MARGIN, -1.0f * MARGIN, 0.0f);
            DialogText.rectTransform.sizeDelta = BOX_STANDARD_SIZE;

            if(_font)
                DialogText.font = _font;

            DialogText.fontSize = FONT_SIZE;
            DialogText.color = Color.white;
        }

        public void CreateLocutor(DisplayDialogMessageModel model, Canvas canvas)
        {
            if(model.locutor != "")
            {
                GameObject locutorBoxGO = new GameObject("Locutor box");
                locutorBoxGO.transform.parent = canvas.transform;

                LocutorBoxBackground = locutorBoxGO.AddComponent<Image>();

                if (model.style == DialogBoxStyle.Classic)
                {
                    LocutorBoxBackground.sprite = _windowBorder;
                    LocutorBoxBackground.type = Image.Type.Sliced;
                    LocutorBoxBackground.pixelsPerUnitMultiplier = PIXELS_PER_UNIT_MULTIPLIER;
                }
                else
                    LocutorBoxBackground.color = new Color(LocutorBoxBackground.color.r,
                                                           LocutorBoxBackground.color.g,
                                                           LocutorBoxBackground.color.b,
                                                           0.0f);

                LocutorBoxBackground.transform.position = GetLocutorWindowPosition(model.position);
                LocutorBoxBackground.rectTransform.sizeDelta = LOCUTOR_BOX_STANDARD_SIZE;

                AddExtraBackgroundToBox(model, locutorBoxGO);
                CreateLocutorText(model.locutor);
            }
        }

        public void CreateLocutor(DisplayDialogMessageModel model)
        {
            CreateLocutor(model, _canvas);
        }

        public void CreateLocutorText(string text)
        {
            GameObject locutorTextGO = new GameObject("Locutor text");
            locutorTextGO.transform.parent = LocutorBoxBackground.transform;

            LocutorText = locutorTextGO.AddComponent<Text>();
            LocutorText.text = text;

            LocutorText.transform.localPosition = new Vector3(LOCUTOR_MARGIN_X, -1.0f * LOCUTOR_MARGIN_Y, 0.0f);
            LocutorText.rectTransform.sizeDelta = LOCUTOR_BOX_STANDARD_SIZE;

            if(_font)
                LocutorText.font = _font;

            LocutorText.fontSize = FONT_SIZE;
            LocutorText.color = Color.white;
        }

        public static Vector2 GetLocutorWindowPosition(DialogBoxPosition position)
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

