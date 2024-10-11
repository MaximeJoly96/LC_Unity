using NUnit.Framework;
using Shop;
using System.Collections.Generic;
using UnityEngine;
using Inventory;
using Abilities;
using Core.Model;
using Language;
using UnityEditor;
using Utils;
using UnityEngine.UI;
using TMPro;

namespace Testing.Shop
{
    public class SelectableItemTests
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

        private SelectableItem CreateSelectableItem()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);
            return go.AddComponent<SelectableItem>();
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

        private T CreateGenericGameObjectWithComponent<T>() where T : Component
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);
            return go.AddComponent<T>();
        }

        [Test]
        public void DataCanBeFedToSelectableItem()
        {
            CreateFrenchLocalizer();
            CreateGenericGameObjectWithComponent<WeaponsWrapper>();

            SelectableItem item = CreateSelectableItem();

            item.SetIconObject(CreateGenericGameObjectWithComponent<Image>());
            item.SetItemNameObject(CreateGenericGameObjectWithComponent<TextMeshProUGUI>());
            item.SetPriceObject(CreateGenericGameObjectWithComponent<TextMeshProUGUI>());

            item.Feed(new Weapon(new ElementIdentifier(3, "weapon", "weaponKey"), 3, 100, ItemCategory.Weapon,
                                                       new AbilityAnimation("", "", -1, -1, -1), 3, WeaponType.Sword));

            Assert.AreEqual("Arme", item.ItemName.text);
            Assert.AreEqual("100", item.Price.text);
        }
    }
}
