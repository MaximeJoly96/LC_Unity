using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Save;
using Core;
using GameProgression;
using Party;
using System.IO;

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
        public void SaveFilesIdCanBeObtained()
        {
            /*SaveCreator creator = new SaveCreator();
            Dictionary<string, string> save = creator.CreateSaveData();

            creator.WriteSaveDataToDisk(Application.persistentDataPath + "/save0.data", save);
            creator.WriteSaveDataToDisk(Application.persistentDataPath + "/save2.data", save);
            creator.WriteSaveDataToDisk(Application.persistentDataPath + "/save3.data", save);
            creator.WriteSaveDataToDisk(Application.persistentDataPath + "/save5.data", save);

            SaveLoader loader = new SaveLoader();
            List<int> saveIds = loader.GetSavesId();

            Assert.AreEqual(0, saveIds[0]);
            Assert.AreEqual(2, saveIds[1]);
            Assert.AreEqual(3, saveIds[2]);
            Assert.AreEqual(5, saveIds[3]);

            File.Delete(Application.persistentDataPath + "/save0.data");
            File.Delete(Application.persistentDataPath + "/save2.data");
            File.Delete(Application.persistentDataPath + "/save3.data");
            File.Delete(Application.persistentDataPath + "/save5.data");*/
        }
    }
}
