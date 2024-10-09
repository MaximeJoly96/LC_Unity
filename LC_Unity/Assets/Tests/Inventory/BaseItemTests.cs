using Inventory;
using NUnit.Framework;
using Core.Model;
using Effects;
using System.Collections.Generic;
using UnityEngine;
using Language;
using UnityEditor;

namespace Testing.Inventory
{
    public class BaseItemTests
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

        [Test]
        public void BaseItemCanBeCreated()
        {
            BaseItem item = new BaseItem(new ElementIdentifier(3, "item", "description"), 12, 6, ItemCategory.Consumable);

            Assert.AreEqual(3, item.Id);
            Assert.AreEqual("item", item.Name);
            Assert.AreEqual("description", item.Description);
            Assert.AreEqual(12, item.Icon);
            Assert.AreEqual(6, item.Price);
            Assert.AreEqual(ItemCategory.Consumable, item.Category);

            Assert.AreEqual(0, item.Effects.Count);
        }

        [Test]
        public void EffectsCanBeAddedToBaseItem()
        {
            BaseItem item = new BaseItem(new ElementIdentifier(3, "item", "description"), 12, 6, ItemCategory.Consumable);
            item.AddEffect(new InflictStatus { Value = global::Actors.EffectType.BleedII });

            Assert.AreEqual(1, item.Effects.Count);
            Assert.IsTrue(item.Effects[0] is InflictStatus);

            List<IEffect> effects = new List<IEffect>
            {
                new AdditionalStrike { Amount = 2},
                new Dispel { Value = global::Actors.EffectType.Poison }
            };

            item.AddEffects(effects);
            Assert.AreEqual(3, item.Effects.Count);
            Assert.IsTrue(item.Effects[1] is AdditionalStrike);
            Assert.IsTrue(item.Effects[2] is Dispel);
        }

        [Test]
        public void DetailedDescriptionOfBaseItemCanBeObtained()
        {
            GameObject localizer = new GameObject("Localizer");
            _usedGameObjects.Add(localizer);
            Localizer component = localizer.AddComponent<Localizer>();

            TextAsset file = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Inventory/french.csv");
            component.LoadLanguage(global::Language.Language.French, file);

            BaseItem item = new BaseItem(new ElementIdentifier(3, "itemKey", "descriptionKey"), 12, 6, ItemCategory.Consumable);
            item.AddEffect(new AdditionalStrike { Amount = 3 });

            Assert.AreEqual("description de l'objet\nAttaques suppl. : 3\n", item.DetailedDescription());
        }
    }
}
