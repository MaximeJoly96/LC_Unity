using NUnit.Framework;
using Shop;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Testing.Shop
{
    public class ShopWindowTests
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

        private ShopWindow CreateShopWindow()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);
            ShopWindow window = go.AddComponent<ShopWindow>();

            List<ShopOptions> options = new List<ShopOptions>
            {
                CreateOptions(ShopOption.Buy),
                CreateOptions(ShopOption.Sell),
                CreateOptions(ShopOption.Leave)
            };

            window.SetOptions(options);

            TextMeshProUGUI shopName = CreateGenericGameObjectWithComponent<TextMeshProUGUI>();
            shopName.transform.SetParent(window.transform);
            window.SetShopNameObject(shopName);

            ScrollRect scroll = CreateGenericGameObjectWithComponent<ScrollRect>();
            scroll.transform.SetParent(window.transform);
            window.SetScrollViewObject(scroll);

            TextMeshProUGUI currentGold = CreateGenericGameObjectWithComponent<TextMeshProUGUI>();
            currentGold.transform.SetParent(window.transform);
            window.SetCurrentGoldObject(currentGold);

            PartyPreview partyPreview = CreateGenericGameObjectWithComponent<PartyPreview>();
            partyPreview.transform.SetParent(window.transform);
            window.SetPartyPreviewObject(partyPreview);

            return window;
        }

        private ShopOptions CreateOptions(ShopOption option)
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);
            ShopOptions opt = go.AddComponent<ShopOptions>();
            opt.Option = option;

            return opt;
        }

        private T CreateGenericGameObjectWithComponent<T>() where T : Component
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);
            return go.AddComponent<T>();
        }
    }
}
