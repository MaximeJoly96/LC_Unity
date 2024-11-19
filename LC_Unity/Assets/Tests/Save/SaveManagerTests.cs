using NUnit.Framework;
using Save;

namespace Testing.Save
{
    public class SaveManagerTests : TestFoundation
    {
        [Test]
        public void SaveCreationRequestIsDetected()
        {
            SaveManager.SaveState state = SaveManager.SaveState.Closed;

            SaveManager manager = SaveManager.Instance;
            manager.SaveStateChanged.RemoveAllListeners();
            manager.SaveStateChanged.AddListener((s) => state = s);

            Assert.AreEqual(SaveManager.SaveState.Closed, state);

            manager.InitSaveCreation();

            Assert.AreEqual(SaveManager.SaveState.CreateSave, state);
        }

        [Test]
        public void SaveLoadRequestIsDetected()
        {
            SaveManager.SaveState state = SaveManager.SaveState.Closed;

            SaveManager manager = SaveManager.Instance;
            manager.SaveStateChanged.RemoveAllListeners();
            manager.SaveStateChanged.AddListener((s) => state = s);

            Assert.AreEqual(SaveManager.SaveState.Closed, state);

            manager.InitSaveLoad();

            Assert.AreEqual(SaveManager.SaveState.LoadSave, state);
        }
    }
}
