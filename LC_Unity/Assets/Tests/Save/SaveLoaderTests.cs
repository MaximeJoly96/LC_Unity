using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Save;
using Core;
using GameProgression;
using Party;
using System.IO;
using Save.Model;

namespace Testing.Save
{
    public class SaveLoaderTests
    {
        [SetUp]
        public void Setup()
        {
            PersistentDataHolder.Instance.Reset();
            PartyManager.Instance.Clear();
            GlobalStateMachine.Instance.CurrentMapId = -1;
        }

        [Test]
        public void DefaultSaveFileCanBeLoaded()
        {
            SaveCreator creator = new SaveCreator();
            Dictionary<string, string> save = creator.CreateSaveData();
            string path = Application.persistentDataPath + "/test0.data";

            creator.WriteSaveDataToDisk(path, save);

            SaveLoader loader = new SaveLoader();
            Dictionary<string, string> loadedSave = loader.LoadSaveFile(path);

            Assert.AreEqual("0", loadedSave["positionX"]);
            Assert.AreEqual("0", loadedSave["positionY"]);
            Assert.AreEqual("1000", loadedSave["mapId"]);
            Assert.AreEqual("0", loadedSave["inGameTime"]);
            Assert.AreEqual("0", loadedSave["gold"]);

            File.Delete(path);
        }

        [Test]
        public void SaveDescriptorsCanBeObtained()
        {
            SaveLoader loader = new SaveLoader();
            List<SaveDescriptor> descriptors = loader.GetSaveDescriptors("Assets/Tests/Save/TestData/");

            Assert.AreEqual(3, descriptors.Count);

            Assert.AreEqual(0, descriptors[0].Id);
            Assert.AreEqual(2, descriptors[0].MapId);
            Assert.IsTrue(Mathf.Abs(descriptors[0].InGameTime - 32568.9f) < 0.01f);

            Assert.AreEqual(1, descriptors[1].Id);
            Assert.AreEqual(3, descriptors[1].MapId);
            Assert.IsTrue(Mathf.Abs(descriptors[1].InGameTime - 17.8f) < 0.01f);

            Assert.AreEqual(2, descriptors[2].Id);
            Assert.AreEqual(1000, descriptors[2].MapId);
            Assert.IsTrue(Mathf.Abs(descriptors[2].InGameTime - 498563.6f) < 0.01f);
        }
    }
}
