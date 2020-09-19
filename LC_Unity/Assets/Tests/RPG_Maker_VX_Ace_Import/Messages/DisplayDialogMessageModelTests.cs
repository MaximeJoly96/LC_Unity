using RPG_Maker_VX_Ace_Import.Messages;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class DisplayDialogMessageModelTests
    {
        [Test]
        public void CreateDialogMessageTest()
        {
            DisplayDialogMessageModel model = new DisplayDialogMessageModel("locutor", "my message", DialogBoxStyle.Classic, DialogBoxPosition.Bottom, Color.white);

            Assert.IsTrue(model.locutor == "locutor");
            Assert.IsTrue(model.message == "my message");
            Assert.IsTrue(model.style == DialogBoxStyle.Classic);
            Assert.IsTrue(model.position == DialogBoxPosition.Bottom);

            Assert.IsTrue(model.locutor != "something");
            Assert.IsTrue(model.message != "something else");
            Assert.IsTrue(model.style != DialogBoxStyle.Transparent);
            Assert.IsTrue(model.position != DialogBoxPosition.Top);
            Assert.IsTrue(model.backgroundColor != Color.black);
        }

        [Test]
        public void UpdateDialogMessageFieldsTest()
        {
            DisplayDialogMessageModel model = new DisplayDialogMessageModel("locutor", "my message", DialogBoxStyle.Classic, DialogBoxPosition.Bottom, Color.white);

            model.locutor = "hello";
            model.message = "my new message";
            model.position = DialogBoxPosition.Middle;
            model.style = DialogBoxStyle.Transparent;
            model.backgroundColor = Color.red;

            Assert.IsTrue(model.locutor != "locutor");
            Assert.IsTrue(model.message != "my message");
            Assert.IsTrue(model.position != DialogBoxPosition.Bottom);
            Assert.IsTrue(model.style != DialogBoxStyle.Classic);
            Assert.IsTrue(model.backgroundColor != Color.white);
        }
    }
}
