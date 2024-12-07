using NUnit.Framework;
using BattleSystem.Data;
using UnityEngine;
using UnityEditor;
using BattleSystem.Model;

namespace Testing.BattleSystem.Data
{
    public class EnemyParserTests : TestFoundation
    {
        [Test]
        public void EnemyCanBeParsed()
        {
            TextAsset file = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/BattleSystem/Data/TestData/TestEnemies.xml");

            EnemyParser parser = new EnemyParser();

            Battler b0 = parser.ParseEnemy(file, 0);
            Battler b1 = parser.ParseEnemy(file, 1);
            Battler b2 = parser.ParseEnemy(file, 2);

            Assert.AreEqual(0, b0.Character.Id);
            Assert.AreEqual("sampleEnemy", b0.Character.Name);
            Assert.AreEqual(0, b0.Character.Stats.Level);
            Assert.AreEqual(1000, b0.Character.Stats.MaxHealth);
            Assert.AreEqual(25, b0.Character.Stats.MaxMana);
            Assert.AreEqual(100, b0.Character.Stats.MaxEssence);
            Assert.AreEqual(8, b0.Character.Stats.BaseStrength);
            Assert.AreEqual(7, b0.Character.Stats.BaseDefense);
            Assert.AreEqual(3, b0.Character.Stats.BaseMagic);
            Assert.AreEqual(8, b0.Character.Stats.BaseMagicDefense);
            Assert.AreEqual(5, b0.Character.Stats.BaseAgility);
            Assert.AreEqual(6, b0.Character.Stats.BaseLuck);

            Assert.AreEqual(1, b1.Character.Id);
            Assert.AreEqual("rat", b1.Character.Name);
            Assert.AreEqual(0, b1.Character.Stats.Level);
            Assert.AreEqual(25, b1.Character.Stats.MaxHealth);
            Assert.AreEqual(0, b1.Character.Stats.MaxMana);
            Assert.AreEqual(100, b1.Character.Stats.MaxEssence);
            Assert.AreEqual(4, b1.Character.Stats.BaseStrength);
            Assert.AreEqual(3, b1.Character.Stats.BaseDefense);
            Assert.AreEqual(1, b1.Character.Stats.BaseMagic);
            Assert.AreEqual(6, b1.Character.Stats.BaseMagicDefense);
            Assert.AreEqual(18, b1.Character.Stats.BaseAgility);
            Assert.AreEqual(6, b1.Character.Stats.BaseLuck);

            Assert.AreEqual(2, b2.Character.Id);
            Assert.AreEqual("scarymbe", b2.Character.Name);
            Assert.AreEqual(0, b2.Character.Stats.Level);
            Assert.AreEqual(32, b2.Character.Stats.MaxHealth);
            Assert.AreEqual(3, b2.Character.Stats.MaxMana);
            Assert.AreEqual(100, b2.Character.Stats.MaxEssence);
            Assert.AreEqual(4, b2.Character.Stats.BaseStrength);
            Assert.AreEqual(3, b2.Character.Stats.BaseDefense);
            Assert.AreEqual(7, b2.Character.Stats.BaseMagic);
            Assert.AreEqual(6, b2.Character.Stats.BaseMagicDefense);
            Assert.AreEqual(6, b2.Character.Stats.BaseAgility);
            Assert.AreEqual(6, b2.Character.Stats.BaseLuck);
        }
    }
}
