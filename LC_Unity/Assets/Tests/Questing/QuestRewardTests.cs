using Questing;
using NUnit.Framework;
using System.Collections.Generic;
using Inventory;

namespace Testing.Questing
{
    public class QuestRewardTests : TestFoundation
    {
        [Test]
        public void QuestRewardCanBeCreated()
        {
            QuestReward reward = new QuestReward(50, 75, new List<InventoryItem>());

            Assert.AreEqual(50, reward.Exp);
            Assert.AreEqual(75, reward.Gold);
            Assert.AreEqual(0, reward.Items.Count);
        }
    }
}
