using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using System.Xml;
using Inventory;

namespace Testing.Inventory
{
    public class ItemParserTests
    {
        [Test]
        public void RecipeCanBeParsed()
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Inventory/TestData/TestWeapons.xml").text);

            ItemRecipe recipe = ItemParser.ParseRecipeFromNode(document.SelectSingleNode("Items").
                                                               ChildNodes[2].SelectSingleNode("Recipe"));

            Assert.AreEqual(2, recipe.Components.Count);
            Assert.AreEqual(1201, recipe.Components[0].ItemId);
            Assert.AreEqual(1203, recipe.Components[1].ItemId);
            Assert.AreEqual(1, recipe.Components[0].Quantity);
            Assert.AreEqual(1, recipe.Components[1].Quantity);
        }

        [Test]
        public void ItemStatsCanBeParsed()
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Inventory/TestData/TestWeapons.xml").text);

            ItemStats stats = ItemParser.ParseItemStats(document.SelectSingleNode("Items").
                                                        ChildNodes[0].SelectSingleNode("Stats"));

            Assert.AreEqual(20, stats.Health);
            Assert.AreEqual(20, stats.Mana);
            Assert.AreEqual(0, stats.Essence);
            Assert.AreEqual(5, stats.Strength);
            Assert.AreEqual(0, stats.Defense);
            Assert.AreEqual(10, stats.Magic);
            Assert.AreEqual(0, stats.MagicDefense);
            Assert.AreEqual(0, stats.Agility);
            Assert.AreEqual(0, stats.Luck);
        }
    }
}
