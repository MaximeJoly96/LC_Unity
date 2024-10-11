using NUnit.Framework;
using Shop;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Language;
using UnityEngine.UI;
using Inventory;
using Abilities;
using Core.Model;
using Core;
using Utils;
using UnityEditor;

namespace Testing.Shop
{
    public class ShopConfirmationWindowTests
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
            window.SetSelectedQuantityObject(CreateLocalizedText());
            window.SetFirstDigitObject(CreateQuantitySelector());
            window.SetSecondDigitObject(CreateQuantitySelector());

            return window;
        }

        private T CreateGenericGameObjectWithComponent<T>() where T : Component
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);
            return go.AddComponent<T>();
        }

        private LocalizedText CreateLocalizedText()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);
            go.AddComponent<TextMeshProUGUI>();
            return go.AddComponent<LocalizedText>();
        }

        private Localizer CreateFrenchLocalizer()
        {
            GameObject localizer = new GameObject("Localizer");
            _usedGameObjects.Add(localizer);
            Localizer component = localizer.AddComponent<Localizer>();

            TextAsset file = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Shop/french.csv");
            component.LoadLanguage(global::Language.Language.French, file);

            return component;
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

        [Test]
        public void WindowCanBeOpened()
        {
            CreateFrenchLocalizer();
            CreateGenericGameObjectWithComponent<WeaponsWrapper>();
            ShopConfirmationWindow window = CreateConfirmationWindow();
            Weapon weapon = new Weapon(new ElementIdentifier(3, "weapon", "weaponKey"), 3, 100, ItemCategory.Weapon,
                                                             new AbilityAnimation("", "", -1, -1, -1), 3, WeaponType.Sword);

            window.Open(weapon, true);

            Assert.AreEqual("Combien souhaitez-vous en acheter ?", window.SelectedItemQuantity);
            Assert.AreEqual(GlobalStateMachine.Instance.CurrentState, GlobalStateMachine.State.BuyingItems);
            Assert.AreEqual("Arme", window.ItemName);
            Assert.AreEqual("100 Okanes", window.ItemPrice);
            Assert.AreEqual("100 Okanes", window.TotalPrice);

            window.Open(weapon, false);

            Assert.AreEqual("Combien souhaitez-vous en vendre ?", window.SelectedItemQuantity);
            Assert.AreEqual(GlobalStateMachine.Instance.CurrentState, GlobalStateMachine.State.SellingItems);
        }

        [Test]
        public void QuantityCanBeAdjusted()
        {
            CreateFrenchLocalizer();
            CreateGenericGameObjectWithComponent<WeaponsWrapper>();
            ShopConfirmationWindow window = CreateConfirmationWindow();
            Weapon weapon = new Weapon(new ElementIdentifier(3, "weapon", "weaponKey"), 3, 100, ItemCategory.Weapon,
                                                             new AbilityAnimation("", "", -1, -1, -1), 3, WeaponType.Sword);

            window.Open(weapon, true);

            Assert.AreEqual("100 Okanes", window.ItemPrice);
            Assert.AreEqual("100 Okanes", window.TotalPrice);

            window.MoveCursorUp();

            Assert.AreEqual("200 Okanes", window.TotalPrice);

            window.MoveCursorDown();

            Assert.AreEqual("100 Okanes", window.TotalPrice);

            window.MoveCursorLeft();
            window.MoveCursorUp();

            Assert.AreEqual("1100 Okanes", window.TotalPrice);
        }

        [Test]
        public void EventsCanBeCaught()
        {
            CreateFrenchLocalizer();
            CreateGenericGameObjectWithComponent<WeaponsWrapper>();
            ShopConfirmationWindow window = CreateConfirmationWindow();
            Weapon weapon = new Weapon(new ElementIdentifier(3, "weapon", "weaponKey"), 3, 100, ItemCategory.Weapon,
                                                             new AbilityAnimation("", "", -1, -1, -1), 3, WeaponType.Sword);

            window.Open(weapon, true);

            int testEvent = -1;
            window.PurchaseCompleted.AddListener((item, value) => Assert.AreEqual(1, value));
            window.SellCompleted.AddListener((item, value) => Assert.AreEqual(2, value));
            window.PurchaseCancelled.AddListener(() =>
            {
                testEvent++;
                Assert.AreEqual(0, testEvent);
            });

            window.ConfirmPurchase();

            window.MoveCursorUp();

            window.ConfirmSell();
            window.Cancel();
        }
    }
}
