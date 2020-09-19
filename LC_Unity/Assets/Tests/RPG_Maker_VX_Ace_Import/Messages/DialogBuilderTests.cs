using RPG_Maker_VX_Ace_Import.Messages;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace Tests
{
    public class DialogBuilderTests
    {
        private DialogBuilder _builder;
        private Canvas _canvas;

        [SetUp]
        public void Setup()
        {
            GameObject builderGO = new GameObject();
            _builder = builderGO.AddComponent<DialogBuilder>();

            GameObject canvasGO = new GameObject();
            _canvas = canvasGO.AddComponent<Canvas>();
            _canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        }

        private DisplayDialogMessageModel CreateModel()
        {
            DisplayDialogMessageModel model = new DisplayDialogMessageModel("locutor",
                                                                            "message",
                                                                            DialogBoxStyle.Classic,
                                                                            DialogBoxPosition.Bottom,
                                                                            Color.red);

            return model;
        }

        [Test]
        public void CreateClassicBaseDialogTest()
        {
            _builder.CreateBaseDialog(CreateModel(), _canvas);

            Assert.IsTrue(_builder.BoxBackground.type == Image.Type.Sliced);
            Assert.IsTrue(Mathf.Abs(_builder.BoxBackground.pixelsPerUnitMultiplier - DialogBuilder.PIXELS_PER_UNIT_MULTIPLIER) < 0.001f);
            Assert.IsTrue(Vector2.Distance(_builder.BoxBackground.rectTransform.sizeDelta, DialogBuilder.BOX_STANDARD_SIZE) < 0.001f);
            Assert.IsTrue(_builder.BoxBackground.name == "Dialog box");
        }

        [Test]
        public void CreateTransparentBaseDialogTest()
        {
            DisplayDialogMessageModel transparentModel = new DisplayDialogMessageModel("locutor",
                                                                                       "message",
                                                                                       DialogBoxStyle.Transparent,
                                                                                       DialogBoxPosition.Bottom,
                                                                                       Color.red);

            _builder.CreateBaseDialog(transparentModel, _canvas);

            Assert.IsTrue(Mathf.Abs(_builder.BoxBackground.color.a - 0.0f) < 0.001f);
            Assert.IsTrue(Vector2.Distance(_builder.BoxBackground.rectTransform.sizeDelta, DialogBuilder.BOX_STANDARD_SIZE) < 0.001f);
            Assert.IsTrue(_builder.BoxBackground.name == "Dialog box");
        }

        [Test]
        public void AddExtraBackgroundToBoxTest()
        {
            DisplayDialogMessageModel model = CreateModel();
            _builder.CreateBaseDialog(model, _canvas);
            _builder.AddExtraBackgroundToBox(model, _builder.BoxBackground.gameObject);

            GameObject firstChild = _builder.BoxBackground.gameObject.transform.GetChild(0).gameObject;
            Image firstChildImage = firstChild.GetComponent<Image>();

            Assert.IsTrue(firstChild.name == "Background");
            Assert.IsTrue(Mathf.Abs(firstChildImage.color.r - model.backgroundColor.r * 0.5f) < 0.001f &&
                          Mathf.Abs(firstChildImage.color.g - model.backgroundColor.g * 0.5f) < 0.001f &&
                          Mathf.Abs(firstChildImage.color.b - model.backgroundColor.b * 0.5f) < 0.001f);
            Assert.IsTrue(Vector3.Distance(firstChild.transform.position, _builder.BoxBackground.transform.position) < 0.001f);
            Assert.IsTrue(Vector2.Distance(firstChildImage.rectTransform.sizeDelta, _builder.BoxBackground.rectTransform.sizeDelta) < 0.001f);
        }

        [Test]
        public void GetDialogPositionTest()
        {
            Vector2 bottomPos = DialogBuilder.GetDialogPosition(DialogBoxPosition.Bottom);
            Vector2 midPos = DialogBuilder.GetDialogPosition(DialogBoxPosition.Middle);
            Vector2 topPos = DialogBuilder.GetDialogPosition(DialogBoxPosition.Top);

            Assert.IsTrue(Vector2.Distance(midPos, new Vector2(DialogBuilder.SCREEN_MIDDLE_X, Screen.height / 2.0f)) < 0.001f);
            Assert.IsTrue(Vector2.Distance(topPos, new Vector2(DialogBuilder.SCREEN_MIDDLE_X, Screen.height - DialogBuilder.BOX_STANDARD_SIZE.y / 2.0f - DialogBuilder.MARGIN)) < 0.001f);
            Assert.IsTrue(Vector2.Distance(bottomPos, new Vector2(DialogBuilder.SCREEN_MIDDLE_X, DialogBuilder.BOX_STANDARD_SIZE.y / 2.0f + DialogBuilder.MARGIN)) < 0.001f);
        }

        [Test]
        public void CreateTextTest()
        {
            DisplayDialogMessageModel model = CreateModel();

            _builder.CreateBaseDialog(model, _canvas);
            _builder.CreateText(model);

            Assert.IsTrue(_builder.DialogText.name == "Dialog Text");
            Assert.IsTrue(_builder.DialogText.text == model.message);

            Assert.IsTrue(Vector3.Distance(_builder.DialogText.transform.localPosition, new Vector3(DialogBuilder.MARGIN, -1.0f * DialogBuilder.MARGIN, 0.0f)) < 0.001f);
            Assert.IsTrue(Vector2.Distance(_builder.DialogText.rectTransform.sizeDelta, DialogBuilder.BOX_STANDARD_SIZE) < 0.001f);
            Assert.IsTrue(_builder.DialogText.fontSize == DialogBuilder.FONT_SIZE);

            Color dialogColor = _builder.DialogText.color;
            Assert.IsTrue(Mathf.Abs(dialogColor.r - Color.white.r) < 0.001f &&
                          Mathf.Abs(dialogColor.g - Color.white.g) < 0.001f &&
                          Mathf.Abs(dialogColor.b - Color.white.b) < 0.001f &&
                          Mathf.Abs(dialogColor.a - Color.white.a) < 0.001f);
        }

        [Test]
        public void CreateLocutorTest()
        {
            DisplayDialogMessageModel model = CreateModel();
            _builder.CreateBaseDialog(model, _canvas);
            _builder.CreateText(model);
            _builder.CreateLocutor(model, _canvas);

            Assert.IsTrue(_builder.LocutorBoxBackground.name == "Locutor box");
            Assert.IsTrue(_builder.LocutorBoxBackground.type == Image.Type.Sliced);
            Assert.IsTrue(Mathf.Abs(_builder.LocutorBoxBackground.pixelsPerUnitMultiplier - DialogBuilder.PIXELS_PER_UNIT_MULTIPLIER) < 0.001f);
            Assert.IsTrue(Vector2.Distance(_builder.LocutorBoxBackground.rectTransform.sizeDelta, DialogBuilder.LOCUTOR_BOX_STANDARD_SIZE) < 0.001f);
        }

        [Test]
        public void CreateLocutorTextTest()
        {
            DisplayDialogMessageModel model = CreateModel();
            _builder.CreateBaseDialog(model, _canvas);
            _builder.CreateText(model);
            _builder.CreateLocutor(model, _canvas);

            Assert.IsTrue(_builder.LocutorText.name == "Locutor text");
            Assert.IsTrue(_builder.LocutorText.text == model.locutor);
            Assert.IsTrue(Vector3.Distance(_builder.LocutorText.transform.localPosition, new Vector3(DialogBuilder.LOCUTOR_MARGIN_X, -1.0f * DialogBuilder.LOCUTOR_MARGIN_Y, 0.0f)) < 0.001f);
            Assert.IsTrue(Vector2.Distance(_builder.LocutorText.rectTransform.sizeDelta, DialogBuilder.LOCUTOR_BOX_STANDARD_SIZE) < 0.001f);
            Assert.IsTrue(_builder.LocutorText.fontSize == DialogBuilder.FONT_SIZE);

            Color dialogColor = _builder.LocutorText.color;
            Assert.IsTrue(Mathf.Abs(dialogColor.r - Color.white.r) < 0.001f &&
                          Mathf.Abs(dialogColor.g - Color.white.g) < 0.001f &&
                          Mathf.Abs(dialogColor.b - Color.white.b) < 0.001f &&
                          Mathf.Abs(dialogColor.a - Color.white.a) < 0.001f);
        }

        [Test]
        public void GetLocutorWindowPositionTest()
        {
            Vector2 bottomPos = DialogBuilder.GetLocutorWindowPosition(DialogBoxPosition.Bottom);
            Vector2 midPos = DialogBuilder.GetLocutorWindowPosition(DialogBoxPosition.Middle);
            Vector2 topPos = DialogBuilder.GetLocutorWindowPosition(DialogBoxPosition.Top);

            Assert.IsTrue(Vector2.Distance(bottomPos, new Vector2(DialogBuilder.SCREEN_MIDDLE_X - DialogBuilder.BOX_STANDARD_SIZE.x / 2.0f + DialogBuilder.LOCUTOR_BOX_STANDARD_SIZE.x / 2.0f,
                                                                  DialogBuilder.BOX_STANDARD_SIZE.y / 2.0f + DialogBuilder.MARGIN + DialogBuilder.LOCUTOR_BOX_Y_OFFSET)) < 0.001f);
            Assert.IsTrue(Vector2.Distance(topPos, new Vector2(DialogBuilder.SCREEN_MIDDLE_X + DialogBuilder.BOX_STANDARD_SIZE.x / 2.0f - DialogBuilder.LOCUTOR_BOX_STANDARD_SIZE.x / 2.0f,
                                                               Screen.height - DialogBuilder.BOX_STANDARD_SIZE.y / 2.0f - DialogBuilder.MARGIN - DialogBuilder.LOCUTOR_BOX_Y_OFFSET)) < 0.001f);
            Assert.IsTrue(Vector2.Distance(midPos, new Vector2(DialogBuilder.SCREEN_MIDDLE_X - DialogBuilder.BOX_STANDARD_SIZE.x / 2.0f + DialogBuilder.LOCUTOR_BOX_STANDARD_SIZE.x / 2.0f,
                                                               Screen.height / 2.0f + DialogBuilder.LOCUTOR_BOX_Y_OFFSET)) < 0.001f);
        }
    }
}

