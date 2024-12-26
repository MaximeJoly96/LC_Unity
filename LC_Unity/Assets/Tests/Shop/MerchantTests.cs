using NUnit.Framework;
using Shop;
using Inventory;
using Core.Model;
using Abilities;
using System.Collections.Generic;

namespace Testing.Shop
{
    public class MerchantTests : TestFoundation
    {
        [Test]
        public void EmptyMerchantCanBeCreated()
        {
            Merchant merchant = new Merchant(new ElementIdentifier(3, "merchantName", "merchantDesc"));

            Assert.AreEqual(3, merchant.Id);
            Assert.AreEqual("merchantName", merchant.Name);
            Assert.AreEqual("merchantDesc", merchant.Description);
            Assert.AreEqual(0, merchant.Items.Count);
        }

        [Test]
        public void ItemsCanBeAddedToMerchant()
        {
            Merchant merchant = new Merchant(new ElementIdentifier(3, "merchantName", "merchantDesc"));

            Consumable consumable = new Consumable(new ElementIdentifier(5, "cons", "consDesc"), 3, 10,
                                                   ItemCategory.Consumable, ItemUsability.MenuOnly, 1,
                                                   new AbilityAnimation("", "", -1, -1, -1), TargetEligibility.All, 100);
            Weapon weapon = new Weapon(new ElementIdentifier(7, "weapon", "weaponDesc"), 5, 150,
                                       ItemCategory.Weapon, new AbilityAnimation("", "", -1, -1, -1), 3,
                                       WeaponType.Staff);

            merchant.AddItem(consumable);
            merchant.AddItem(weapon);

            Assert.AreEqual(2, merchant.Items.Count);
            Assert.AreEqual("cons", merchant.Items[0].Name);
            Assert.AreEqual(150, merchant.Items[1].Price);
        }

        [Test]
        public void TypesOfSoldItemsCanBeObtained()
        {
            Merchant merchant = new Merchant(new ElementIdentifier(3, "merchantName", "merchantDesc"));

            Consumable consumable = new Consumable(new ElementIdentifier(5, "cons", "consDesc"), 3, 10,
                                                   ItemCategory.Consumable, ItemUsability.MenuOnly, 1,
                                                   new AbilityAnimation("", "", -1, -1, -1), TargetEligibility.All, 100);
            Weapon weapon = new Weapon(new ElementIdentifier(7, "weapon", "weaponDesc"), 5, 150,
                                       ItemCategory.Weapon, new AbilityAnimation("", "", -1, -1, -1), 3,
                                       WeaponType.Staff);

            merchant.AddItem(consumable);
            merchant.AddItem(weapon);

            List<ItemCategory> types = merchant.SoldItemsTypes;

            Assert.AreEqual(2, types.Count);
            Assert.IsTrue(types.Contains(ItemCategory.Consumable)); 
            Assert.IsTrue(types.Contains(ItemCategory.Weapon));
        }
    }
}
