using NUnit.Framework;
using Shop;
using System.Collections.Generic;
using UnityEngine;
using Inputs;
using UnityEngine.TestTools;
using System.Collections;
using UnityEditor;
using Inventory;
using Engine.SceneControl;
using Core;

namespace Testing.Shop
{
    public class ShopManagerTests
    {
        private List<GameObject> _usedGameObjects;

        [TearDown]
        public void TearDown()
        {
            for (int i = 0; i < _usedGameObjects.Count; i++)
            {
                GameObject.Destroy(_usedGameObjects[i]);
            }
        }

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            _usedGameObjects = new List<GameObject>();
        }

        private InputController CreateInputController()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);
            return go.AddComponent<InputController>();
        }

        private ItemsWrapper CreateEmptyWrapper()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);
            return go.AddComponent<ItemsWrapper>();
        }

        private ShopWindow CreateShopWindow()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);
            return go.AddComponent<ShopWindow>();
        }

        [UnityTest]
        public IEnumerator ShopCanBeSetup()
        {
            CreateInputController();

            GameObject go = new GameObject();
            _usedGameObjects.Add(go);
            ShopManager manager = go.AddComponent<ShopManager>();
            manager.gameObject.AddComponent<InputReceiver>();

            ItemsWrapper wrapper = CreateEmptyWrapper();
            wrapper.FeedConsumables(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Shop/TestData/TestConsumables.xml"));
            wrapper.FeedArmours(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Shop/TestData/TestArmours.xml"));

            yield return null;

            manager.SetupWindow(CreateShopWindow());
            manager.LoadMerchants(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Shop/TestData/TestMerchants.xml"));
            manager.SetupShop(new ShopProcessing
            {
                MerchantId = 0
            });
        }
    }
}
