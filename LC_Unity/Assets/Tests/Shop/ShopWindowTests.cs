using NUnit.Framework;
using Shop;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Language;
using UnityEditor;
using Core.Model;
using Abilities;
using Inventory;
using Party;
using Engine.Party;
using UnityEngine.TestTools;
using System.Collections;

namespace Testing.Shop
{
    public class ShopWindowTests : TestFoundation
    {
        [UnityTest]
        public IEnumerator MerchantCanBeSetup()
        {
            CreateFrenchLocalizer();
            PartyManager.Instance.Clear();

            ShopWindow window = CreateShopWindow();
            Merchant merchant = new Merchant(new ElementIdentifier(2, "merchantTest", "merchantTestDesc"));
            Weapon weapon = new Weapon(new ElementIdentifier(3, "weapon", "weaponKey"), 3, 100, ItemCategory.Weapon,
                                                             new AbilityAnimation("", "", -1, -1, -1), 3, WeaponType.Sword);
            merchant.AddItem(weapon);
            PartyManager.Instance.ChangeGold(new ChangeGold { Value = 300 });

            window.SetupMerchant(merchant);

            yield return null;

            Assert.AreEqual("Marchand de test", window.ShopName);
            Assert.IsTrue(window.GetComponentInChildren<PartyPreview>().gameObject.activeInHierarchy);
            Assert.AreEqual("300 Okanes", window.CurrentGold);
            Assert.IsTrue(Mathf.Abs(0.0f - window.ItemDetails.gameObject.GetComponentInChildren<CanvasGroup>().alpha) < 0.01f);
        }

        private ShopWindow CreateShopWindow()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);
            ShopWindow window = go.AddComponent<ShopWindow>();

            List<ShopHorizontalMenuButton> options = new List<ShopHorizontalMenuButton>
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

        private ShopHorizontalMenuButton CreateOptions(ShopOption option)
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);
            ShopHorizontalMenuButton opt = go.AddComponent<ShopHorizontalMenuButton>();
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
    }
}
