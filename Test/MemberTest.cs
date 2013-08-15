using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Exceptions;
using Domain.Notifications;
using Domain.Utils;
using NUnit.Framework;


namespace Test
{
    [TestFixture]
    public class MemberTest
    {
        [Test]
        public void test_new_member_calculate_owed_amounts()
        {

            Assert.AreEqual(0f, new User().getOwedAmount());
            var newUser = new User();

        }

        [Test]
        public void test_if_new_member_has_debts()
        {

            Assert.IsFalse(new User().hasDebts());

        }

        [Test]
        public void test_one_member_creates_one_group()
        {

            var administrator = new User() { name = "Irene", lastName = "Smith" };

            var newGroup = administrator.createGroup("GroupOne");

            Assert.AreEqual(1, administrator.groups.Count);

            Assert.AreEqual("GroupOne", administrator.groups.ElementAt(0).name);

            Assert.AreEqual("Irene", newGroup.administrator.name);

            Assert.AreEqual("Smith", newGroup.administrator.lastName);

        }

        [Test]
        public void test_one_member_creates_two_groups()
        {

            var administrator = new User() { name = "Irene", lastName = "Smith" };

            var newGroupOne = administrator.createGroup("GroupOne");

            var newGroupTwo = administrator.createGroup("GroupTwo");

            Assert.AreEqual(2, administrator.groups.Count);

            Assert.AreEqual("GroupOne", administrator.groups.ElementAt(0).name);

            Assert.AreEqual("Irene", newGroupOne.administrator.name);

            Assert.AreEqual("Smith", newGroupOne.administrator.lastName);

            Assert.AreEqual("GroupTwo", administrator.groups.ElementAt(1).name);

            Assert.AreEqual("Irene", newGroupTwo.administrator.name);

            Assert.AreEqual("Smith", newGroupTwo.administrator.lastName);

        }

        [Test]
        public void test_one_member_joins_a_group()
        {
            var administrator = new User();

            var memberOne = new User() { name = "Walter", lastName = "Placona" };

            var newGroup = administrator.createGroup("GroupOne");

            memberOne.joinGroup(newGroup);

            Assert.AreEqual(1, memberOne.groups.Count);

            Assert.AreEqual("GroupOne", memberOne.groups.ElementAt(0).name);

            Assert.AreEqual(1, newGroup.members.Count);

        }

        [Test]
        public void test_one_member_joins_two_groups()
        {
            var administrator = new User();

            var memberOne = new User() { name = "Walter", lastName = "Placona" };

            var newGroupOne = administrator.createGroup("GroupOne");

            var newGroupTwo = administrator.createGroup("GroupTwo");

            memberOne.joinGroup(newGroupOne);

            memberOne.joinGroup(newGroupTwo);

            Assert.AreEqual(2, memberOne.groups.Count);

            Assert.AreEqual("GroupOne", memberOne.groups.ElementAt(0).name);

            Assert.AreEqual("GroupTwo", memberOne.groups.ElementAt(1).name);

            Assert.AreEqual(1, newGroupOne.members.Count);

            Assert.AreEqual(1, newGroupTwo.members.Count);

        }

        [Test]
        [ExpectedException(typeof(AlreadyJoinedException))]
        public void test_one_member_tries_to_join_twice_one_group()
        {
            var administrator = new User();

            var memberOne = new User();

            var newGroup = administrator.createGroup("GroupOne");

            memberOne.joinGroup(newGroup);

            memberOne.joinGroup(newGroup);

        }

        [Test]
        [ExpectedException(typeof(AlreadyJoinedException))]
        public void test_one_administrator_tries_to_join_group()
        {
            var administrator = new User();

            var newGroup = administrator.createGroup("GroupOne");

            administrator.joinGroup(newGroup);

        }

        [Test]
        public void test_one_member_leaves_group()
        {
            var administrator = new User(){username = "administrator"};

            var memberOne = new User(){username = "memberOne"};

            var newGroup = administrator.createGroup("GroupOne");

            memberOne.joinGroup(newGroup);

            memberOne.leaveGroup(newGroup);

            Assert.AreEqual(0, memberOne.groups.Count);

            Assert.AreEqual(0, newGroup.members.Count);

        }

        [Test]
        [ExpectedException(typeof(NotJoinedException))]
        public void test_one_unjoined_member_tries_to_leave_group()
        {
            var administrator = new User(){username = "administrator"};

            var memberOne = new User(){username = "memberOne"};

            var newGroup = administrator.createGroup("GroupOne");

            memberOne.leaveGroup(newGroup);

        }

        [Test]
        public void test_one_member_register_purchase()
        {
            var administrator = new User();

            var memberOne = new User();

            var memberTwo = new User();

            var newGroup = administrator.createGroup("GroupOne");

            memberOne.joinGroup(newGroup);

            memberTwo.joinGroup(newGroup);

            var newPurchase = new Purchase()
            {
                buyer = administrator,
                debtors = newGroup.members,
                description = "Pelota",
                group = newGroup,
                totalAmount = 120f
            };

            administrator.registerPurchase(newPurchase);

            Assert.AreEqual(40f, memberOne.payments.ElementAt(0).amount);

            Assert.AreEqual(40f, memberTwo.payments.ElementAt(0).amount);

            Assert.AreEqual(PaymentStatus.Paid, administrator.payments.ElementAt(0).status);

            Assert.AreEqual(PaymentStatus.Unpaid, memberOne.payments.ElementAt(0).status);

            Assert.AreEqual(PaymentStatus.Unpaid, memberTwo.payments.ElementAt(0).status);

        }

        [Test]
        public void test_cancel_cross_debts()
        {
            #region users
            var administrator = new User();
            administrator.name = "admin";

            var memberOne = new User();
            memberOne.name = "mem1";
            memberOne.id = 1;

            var memberTwo = new User();
            memberTwo.name = "mem2";
            memberTwo.id = 2;
            #endregion users

            var debtorslist1 = new List<User>();
            debtorslist1.Add(administrator);
            debtorslist1.Add(memberTwo);

            var newGroup = administrator.createGroup("GroupOne");

            memberOne.joinGroup(newGroup);

            memberTwo.joinGroup(newGroup);
            #region compras
            Purchase newPurchase = new Purchase()
            {
                buyer = administrator,
                debtors = newGroup.members,
                description = "Pelota",
                group = newGroup,
                totalAmount = 120f
            };

            Purchase newPurchase2 = new Purchase()
            {
                buyer = memberOne,
                debtors = debtorslist1,
                description = "Sombrero",
                group = newGroup,
                totalAmount = 60f
            };

            Purchase newPurchase3 = new Purchase()
            {
                buyer = administrator,
                debtors = newGroup.members,
                description = "Remera",
                group = newGroup,
                totalAmount = 70f
            };

            #endregion

            administrator.registerPurchase(newPurchase);
            Assert.AreEqual(memberTwo.payments.ElementAt(0).amount, 120f / 3);
            Assert.AreEqual(memberOne.payments.ElementAt(0).amount, 120f / 3);
            Assert.AreEqual(administrator.payments.ElementAt(0).status, PaymentStatus.Paid);

            memberOne.registerPurchase(newPurchase2);

            Assert.IsInstanceOf<CrossDebtsNotification>(memberOne.notifications[1]);

            //Assert.AreEqual(memberOne.payments.ElementAt());

        }

    }
}
