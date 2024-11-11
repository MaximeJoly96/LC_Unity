using NUnit.Framework;
using Shop;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using Inventory;
using Core.Model;
using Abilities;
using Language;
using UnityEditor;
using Utils;
using UnityEngine.UI;
using Party;
using Engine.Party;

namespace Testing.Shop
{
    public class ItemDetailsTests
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

        [Test]
        public void ItemDetailsCanBeShownAndHidden()
        {
            ItemDetails details = CreateItemDetails();

            details.Show(true);
            Assert.IsTrue(Mathf.Abs(1.0f - details.GetComponentInChildren<CanvasGroup>().alpha) < 0.01f);

            details.Show(false);
            Assert.IsTrue(Mathf.Abs(0.0f - details.GetComponentInChildren<CanvasGroup>().alpha) < 0.01f);
        }

        [Test]
        public void DataCanBeFedToItemDetails()
        {
            CreateGenericGameObjectWithComponent<WeaponsWrapper>();
            CreateFrenchLocalizer();
            ItemDetails details = CreateItemDetails();
            Weapon weapon = new Weapon(new ElementIdentifier(3, "weapon", "weaponKey"), 3, 100, ItemCategory.Weapon,
                                                             new AbilityAnimation("", "", -1, -1, -1), 3, WeaponType.Sword);
            details.Feed(weapon);

            Assert.AreEqual("Arme", details.ItemName.text);
            Assert.IsTrue(details.ItemDescription.text.Contains("Clé de l'arme"));
            Assert.AreEqual("Epée", details.ItemType.text);
            Assert.AreEqual("En possession : 0", details.InStock.text);

            details.Feed(new Armour(new ElementIdentifier(4, "armour", "armourKey"), 4, 80, ItemCategory.Armour,
                                    2, ArmourType.Body));

            Assert.AreEqual("Armure", details.ItemName.text);
            Assert.IsTrue(details.ItemDescription.text.Contains("Clé de l'armure"));
            Assert.AreEqual("Corps", details.ItemType.text);
            Assert.AreEqual("En possession : 0", details.InStock.text);

            PartyManager.Instance.Clear();
            PartyManager.Instance.Inventory.Add(new InventoryItem(weapon));
            PartyManager.Instance.ChangeItems(new ChangeItems { Id = 3, Quantity = 5 });

            details.Feed(weapon);

            Assert.AreEqual("Arme", details.ItemName.text);
            Assert.IsTrue(details.ItemDescription.text.Contains("Clé de l'arme"));
            Assert.AreEqual("Epée", details.ItemType.text);
            Assert.AreEqual("En possession : 5", details.InStock.text);
        }
    }
}
