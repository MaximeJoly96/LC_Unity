using Actors;
using Effects;
using Language;
using NUnit.Framework;

namespace Testing.Language
{
    public class LanguageUtilityTests
    {
        [Test]
        public void TranslateLanguageLabelTest()
        {
            Assert.AreEqual("English", LanguageUtility.TranslateLanguageLabel(global::Language.Language.English));
            Assert.AreEqual("Français", LanguageUtility.TranslateLanguageLabel(global::Language.Language.French));
        }

        [Test]
        public void GetStatLanguageKeyTest()
        {
            Assert.AreEqual("health", LanguageUtility.GetStatLanguageKey(Stat.HP));
            Assert.AreEqual("mana", LanguageUtility.GetStatLanguageKey(Stat.MP));
            Assert.AreEqual("strength", LanguageUtility.GetStatLanguageKey(Stat.Strength));
            Assert.AreEqual("defense", LanguageUtility.GetStatLanguageKey(Stat.Defense));
            Assert.AreEqual("magicDefense", LanguageUtility.GetStatLanguageKey(Stat.MagicDefense));
            Assert.AreEqual("magic", LanguageUtility.GetStatLanguageKey(Stat.Magic));
            Assert.AreEqual("agility", LanguageUtility.GetStatLanguageKey(Stat.Agility));
            Assert.AreEqual("luck", LanguageUtility.GetStatLanguageKey(Stat.Luck));
            Assert.AreEqual("critChance", LanguageUtility.GetStatLanguageKey(Stat.CritChance));
            Assert.AreEqual("evasion", LanguageUtility.GetStatLanguageKey(Stat.Evasion));
            Assert.AreEqual("parry", LanguageUtility.GetStatLanguageKey(Stat.Parry));
            Assert.AreEqual("provocation", LanguageUtility.GetStatLanguageKey(Stat.Provocation));
            Assert.AreEqual("accuracy", LanguageUtility.GetStatLanguageKey(Stat.Accuracy));
            Assert.AreEqual("critDmg", LanguageUtility.GetStatLanguageKey(Stat.CritDmg));
        }

        [Test]
        public void GetEffectTypeLanguageKeyTest()
        {
            Assert.AreEqual("bleedI", LanguageUtility.GetEffectTypeLanguageKey(EffectType.BleedI));
            Assert.AreEqual("bleedII", LanguageUtility.GetEffectTypeLanguageKey(EffectType.BleedII));
            Assert.AreEqual("bleedIII", LanguageUtility.GetEffectTypeLanguageKey(EffectType.BleedIII));
            Assert.AreEqual("poison", LanguageUtility.GetEffectTypeLanguageKey(EffectType.Poison));
            Assert.AreEqual("hemoI", LanguageUtility.GetEffectTypeLanguageKey(EffectType.HemoI));
            Assert.AreEqual("breakI", LanguageUtility.GetEffectTypeLanguageKey(EffectType.BreakI));
            Assert.AreEqual("breakII", LanguageUtility.GetEffectTypeLanguageKey(EffectType.BreakII));
            Assert.AreEqual("breakIII", LanguageUtility.GetEffectTypeLanguageKey(EffectType.BreakIII));
            Assert.AreEqual("magicBreakI", LanguageUtility.GetEffectTypeLanguageKey(EffectType.MagicBreakI));
            Assert.AreEqual("magicBreakII", LanguageUtility.GetEffectTypeLanguageKey(EffectType.MagicBreakII));
            Assert.AreEqual("magicBreakIII", LanguageUtility.GetEffectTypeLanguageKey(EffectType.MagicBreakIII));
            Assert.AreEqual("slowI", LanguageUtility.GetEffectTypeLanguageKey(EffectType.SlowI));
            Assert.AreEqual("slowII", LanguageUtility.GetEffectTypeLanguageKey(EffectType.SlowII));
            Assert.AreEqual("slowIII", LanguageUtility.GetEffectTypeLanguageKey(EffectType.SlowIII));
            Assert.AreEqual("blind", LanguageUtility.GetEffectTypeLanguageKey(EffectType.Blind));
            Assert.AreEqual("silence", LanguageUtility.GetEffectTypeLanguageKey(EffectType.Silence));
            Assert.AreEqual("shell", LanguageUtility.GetEffectTypeLanguageKey(EffectType.Shell));
            Assert.AreEqual("protect", LanguageUtility.GetEffectTypeLanguageKey(EffectType.Protect));
            Assert.AreEqual("regen", LanguageUtility.GetEffectTypeLanguageKey(EffectType.Regen));
            Assert.AreEqual("manaRegen", LanguageUtility.GetEffectTypeLanguageKey(EffectType.ManaRegen));
            Assert.AreEqual("bravery", LanguageUtility.GetEffectTypeLanguageKey(EffectType.Bravery));
            Assert.AreEqual("faith", LanguageUtility.GetEffectTypeLanguageKey(EffectType.Faith));
            Assert.AreEqual("grounded", LanguageUtility.GetEffectTypeLanguageKey(EffectType.Grounded));
        }

        [Test]
        public void GetTribeLanguageKeyTest()
        {
            Assert.AreEqual("human", LanguageUtility.GetTribeLanguageKey(TargetTribe.Human));
            Assert.AreEqual("undead", LanguageUtility.GetTribeLanguageKey(TargetTribe.Undead));
        }
    }
}
