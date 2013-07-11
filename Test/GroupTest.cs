using System.Linq;
using Domain;
using Domain.Utils;
using NUnit.Framework;

namespace Test
{

    [TestFixture]
    public class GroupTest
    {
        [Test]
        public void test_create_payment_notifications()
        {

            var administrator = new User();

            var memberOne = new User();

            var newGroup = administrator.createGroup("GroupOne");

            memberOne.joinGroup(newGroup);

            var newPayment = new Payment() { amount = 120f, buyer = administrator, debtor = memberOne,group=newGroup, description = "Pelota"};

            newGroup.createPaymentNotification(newPayment);

            Assert.AreEqual(1, administrator.notifications.Count);

            Assert.AreEqual(NotificationStatus.Unread, administrator.notifications.ElementAt(0).status);

            Assert.AreEqual(0, memberOne.notifications.Count);

        }
        [Test]
        public void test_create_purchase_notifications()
        {
            var administrator = new User();

            var memberOne = new User();

            var memberTwo = new User();

            var newGroup = administrator.createGroup("GroupOne");

            memberOne.joinGroup(newGroup);

            memberTwo.joinGroup(newGroup);

            var newPurchase = new Purchase() { buyer = administrator, debtors = newGroup.members, description = "Glass", totalAmount = 30f, @group = newGroup };

            newGroup.createPurchaseNotifications(newPurchase);

            Assert.AreEqual(1, memberOne.notifications.Count);

            Assert.AreEqual(NotificationStatus.Unread, memberOne.notifications.ElementAt(0).status);

            Assert.AreEqual(1, memberTwo.notifications.Count);

            Assert.AreEqual(NotificationStatus.Unread, memberTwo.notifications.ElementAt(0).status);

            Assert.AreEqual(0, administrator.notifications.Count);

        }
    }

}
