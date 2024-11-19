using NUnit.Framework;
using Save;
using Save.Model;
using System.Collections.Generic;
using UnityEngine;

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

        [Test]
        public void EmptySlotsCanBeCreated()
        {
            SaveManager manager = SaveManager.Instance;
            List<SaveDescriptor> data = manager.CreateEmptyDataSlots(10);

            Assert.AreEqual(10, data.Count);

            for(int i = 0;i < data.Count; i++)
            {
                Assert.AreEqual(i, data[i].Id);
                Assert.AreEqual(-1, data[i].MapId);
                Assert.IsTrue(Mathf.Abs(data[i].InGameTime - 0.0f) < 0.01f);
            }
        }

        [Test]
        public void DescriptorsCanBeMerged()
        {
            SaveManager manager = SaveManager.Instance;
            List<SaveDescriptor> emptyData = manager.CreateEmptyDataSlots(10);
            List<SaveDescriptor> actualData = new List<SaveDescriptor>
            {
                new SaveDescriptor(1, 5, 250.0f),
                new SaveDescriptor(4, 7, 62.3f),
                new SaveDescriptor(5, 1000, 778.5f),
                new SaveDescriptor(8, 4, 44.6f),
            };

            List<SaveDescriptor> merged = manager.MergeEmptyDescriptorsWithData(emptyData, actualData);

            Assert.AreEqual(10, merged.Count);

            Assert.AreEqual(0, merged[0].Id);
            Assert.AreEqual(-1, merged[0].MapId);
            Assert.IsTrue(Mathf.Abs(merged[0].InGameTime - 0.0f) < 0.01f);

            Assert.AreEqual(1, merged[1].Id);
            Assert.AreEqual(5, merged[1].MapId);
            Assert.IsTrue(Mathf.Abs(merged[1].InGameTime - 250.0f) < 0.01f);

            Assert.AreEqual(2, merged[2].Id);
            Assert.AreEqual(-1, merged[2].MapId);
            Assert.IsTrue(Mathf.Abs(merged[2].InGameTime - 0.0f) < 0.01f);

            Assert.AreEqual(3, merged[3].Id);
            Assert.AreEqual(-1, merged[3].MapId);
            Assert.IsTrue(Mathf.Abs(merged[3].InGameTime - 0.0f) < 0.01f);

            Assert.AreEqual(4, merged[4].Id);
            Assert.AreEqual(7, merged[4].MapId);
            Assert.IsTrue(Mathf.Abs(merged[4].InGameTime - 62.3f) < 0.01f);

            Assert.AreEqual(5, merged[5].Id);
            Assert.AreEqual(1000, merged[5].MapId);
            Assert.IsTrue(Mathf.Abs(merged[5].InGameTime - 778.5f) < 0.01f);

            Assert.AreEqual(6, merged[6].Id);
            Assert.AreEqual(-1, merged[6].MapId);
            Assert.IsTrue(Mathf.Abs(merged[6].InGameTime - 0.0f) < 0.01f);

            Assert.AreEqual(7, merged[7].Id);
            Assert.AreEqual(-1, merged[7].MapId);
            Assert.IsTrue(Mathf.Abs(merged[7].InGameTime - 0.0f) < 0.01f);

            Assert.AreEqual(8, merged[8].Id);
            Assert.AreEqual(4, merged[8].MapId);
            Assert.IsTrue(Mathf.Abs(merged[8].InGameTime - 44.6f) < 0.01f);

            Assert.AreEqual(9, merged[9].Id);
            Assert.AreEqual(-1, merged[9].MapId);
            Assert.IsTrue(Mathf.Abs(merged[9].InGameTime - 0.0f) < 0.01f);
        }
    }
}
