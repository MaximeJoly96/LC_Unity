using NUnit.Framework;
using BattleSystem.UI;
using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.TestTools;
using Abilities;
using Language;

namespace Testing.BattleSystem.UI
{
    public class SelectableMoveCategoryTests : TestFoundation
    {
        [UnityTest]
        public IEnumerator DataCanBeFed()
        {
            SelectableMoveCategory category = CreateSelectableMoveCategory();
            Localizer localizer = ComponentCreator.CreateLocalizer("BattleSystem/UI/french.csv", global::Language.Language.French);

            yield return null;

            category.Feed(AbilityCategory.AttackCommand);

            yield return null;

            Assert.AreEqual("Attaquer", category.Label.text);
            Assert.AreEqual(AbilityCategory.AttackCommand, category.Category);

            category.Feed(AbilityCategory.Skill);

            yield return null;

            Assert.AreEqual("Compétences", category.Label.text);
            Assert.AreEqual(AbilityCategory.Skill, category.Category);

            category.Feed(AbilityCategory.ItemCommand);

            yield return null;

            Assert.AreEqual("Objets", category.Label.text);
            Assert.AreEqual(AbilityCategory.ItemCommand, category.Category);

            category.Feed(AbilityCategory.FleeCommand);

            yield return null;

            Assert.AreEqual("Fuir", category.Label.text);
            Assert.AreEqual(AbilityCategory.FleeCommand, category.Category);
        }

        private SelectableMoveCategory CreateSelectableMoveCategory()
        {
            GameObject go = ComponentCreator.CreateEmptyGameObject();
            _usedGameObjects.Add(go);

            TextMeshProUGUI text = go.AddComponent<TextMeshProUGUI>();
            SelectableMoveCategory category = go.AddComponent<SelectableMoveCategory>();
            category.Label = text;

            return category;
        }
    }
}
