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
using Language;
using TMPro;
using UnityEngine.UI;

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

            ItemDetails details = CreateItemDetails();
            details.transform.SetParent(window.transform);
            window.SetItemDetailsObject(details);

            ShopConfirmationWindow confirmationWindow = CreateConfirmationWindow();
            confirmationWindow.transform.SetParent(window.transform);
            window.SetConfirmationWindowObject(confirmationWindow);

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

        private Localizer CreateFrenchLocalizer()
        {
            GameObject localizer = new GameObject("Localizer");
            _usedGameObjects.Add(localizer);
            Localizer component = localizer.AddComponent<Localizer>();

            TextAsset[] files = new TextAsset[] { AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Shop/french.csv") };
            component.LoadLanguage(global::Language.Language.French, files);

            return component;
        }

        private ItemDetails CreateItemDetails()
        {
            ItemDetails details = CreateGenericGameObjectWithComponent<ItemDetails>();

            CanvasGroup group = CreateGenericGameObjectWithComponent<CanvasGroup>();
            group.transform.SetParent(details.transform);
            details.SetCanvasGroupObject(group);

            TextMeshProUGUI inStock = CreateGenericGameObjectWithComponent<TextMeshProUGUI>();
            inStock.transform.SetParent(details.transform);
            details.SetInStockObject(inStock);

            TextMeshProUGUI itemName = CreateGenericGameObjectWithComponent<TextMeshProUGUI>();
            itemName.transform.SetParent(details.transform);
            details.SetItemNameObject(itemName);

            TextMeshProUGUI itemDescription = CreateGenericGameObjectWithComponent<TextMeshProUGUI>();
            itemDescription.transform.SetParent(details.transform);
            details.SetItemDescriptionObject(itemDescription);

            TextMeshProUGUI itemType = CreateGenericGameObjectWithComponent<TextMeshProUGUI>();
            itemType.transform.SetParent(details.transform);
            details.SetItemTypeObject(itemType);

            Image icon = CreateGenericGameObjectWithComponent<Image>();
            icon.transform.SetParent(details.transform);
            details.SetItemIconObject(icon);

            return details;
        }

        private ShopConfirmationWindow CreateConfirmationWindow()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);

            ShopConfirmationWindow window = go.AddComponent<ShopConfirmationWindow>();
            window.SetItemIconObject(CreateGenericGameObjectWithComponent<Image>());
            window.SetItemNameObject(CreateGenericGameObjectWithComponent<TextMeshProUGUI>());
            window.SetItemPriceObject(CreateGenericGameObjectWithComponent<TextMeshProUGUI>());
            window.SetTotalPriceObject(CreateGenericGameObjectWithComponent<TextMeshProUGUI>());
            window.SetInStockObject(CreateGenericGameObjectWithComponent<TextMeshProUGUI>());

            LocalizedText localizedText = CreateLocalizedText();
            localizedText.UpdateKey("wishToBuy");
            window.SetSelectedQuantityObject(localizedText);
            window.SetFirstDigitObject(CreateQuantitySelector());
            window.SetSecondDigitObject(CreateQuantitySelector());

            return window;
        }

        private LocalizedText CreateLocalizedText()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);
            go.AddComponent<TextMeshProUGUI>();
            return go.AddComponent<LocalizedText>();
        }

        private QuantitySelector CreateQuantitySelector()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);
            QuantitySelector selector = go.AddComponent<QuantitySelector>();

            GameObject background = new GameObject();
            _usedGameObjects.Add(background);
            selector.SetBackgroundObject(background.transform);

            GameObject upArrow = new GameObject();
            _usedGameObjects.Add(upArrow);
            selector.SetUpArrowObject(upArrow.transform);

            GameObject downArrow = new GameObject();
            _usedGameObjects.Add(downArrow);
            selector.SetDownArrowObject(downArrow.transform);

            selector.gameObject.AddComponent<TextMeshProUGUI>();

            return selector;
        }

        [UnityTest]
        public IEnumerator ShopCanBeSetup()
        {
            CreateFrenchLocalizer();
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
