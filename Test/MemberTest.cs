using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using Domain.Exceptions;
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

            Assert.AreEqual(0f, new Member().getOwedAmount());

        }

        [Test]
        public void test_if_new_member_has_debts()
        {

            Assert.IsFalse(new Member().hasDebts());

        }

        [Test]
        public void test_one_member_creates_a_group()
        {

            var memberOne = new Member(){name = "Irene",lastName = "Smith"};

            var newGroup =memberOne.createGroup("GroupOne");

            Assert.AreEqual(1,memberOne.groups.Count);

            Assert.AreEqual("GroupOne",memberOne.groups.ElementAt(0).name);

            Assert.AreEqual("Irene",newGroup.administrator.name);

            Assert.AreEqual("Smith", newGroup.administrator.lastName);

        }

        [Test]
        public void test_one_member_creates_two_groups()
        {

            var memberOne = new Member() { name = "Irene", lastName = "Smith" };

            var newGroupOne = memberOne.createGroup("GroupOne");

            var newGroupTwo = memberOne.createGroup("GroupTwo");

            Assert.AreEqual(2, memberOne.groups.Count);

            Assert.AreEqual("GroupOne", memberOne.groups.ElementAt(0).name);

            Assert.AreEqual("Irene", newGroupOne.administrator.name);

            Assert.AreEqual("Smith", newGroupOne.administrator.lastName);

            Assert.AreEqual("GroupTwo", memberOne.groups.ElementAt(1).name);

            Assert.AreEqual("Irene", newGroupTwo.administrator.name);

            Assert.AreEqual("Smith", newGroupTwo.administrator.lastName);

        }

        [Test]
        public void test_one_member_joins_a_group()
        {

            var memberOne = new Member();

            var memberTwo = new Member(){name="Walter",lastName = "Placona"};

            var newGroup = memberOne.createGroup("GroupOne");

            memberTwo.joinGroup(newGroup);

            Assert.AreEqual(1,memberTwo.groups.Count);

            Assert.AreEqual("GroupOne",memberTwo.groups.ElementAt(0).name);

            Assert.AreEqual(1,newGroup.members.Count);

        }

        [Test]
        public void test_one_member_joins_two_groups()
        {

            var memberOne = new Member();

            var memberTwo = new Member() { name = "Walter", lastName = "Placona" };

            var newGroupOne = memberOne.createGroup("GroupOne");

            var newGroupTwo = memberOne.createGroup("GroupTwo");

            memberTwo.joinGroup(newGroupOne);

            memberTwo.joinGroup(newGroupTwo);

            Assert.AreEqual(2, memberTwo.groups.Count);

            Assert.AreEqual("GroupOne", memberTwo.groups.ElementAt(0).name);

            Assert.AreEqual("GroupTwo", memberTwo.groups.ElementAt(0).name);

            Assert.AreEqual(1, newGroupOne.members.Count);

            Assert.AreEqual(1, newGroupTwo.members.Count);

        }

        [Test]
        [ExpectedException(typeof (AlreadyJoinedException))]
        public void test_one_member_tries_to_join_twice_one_group()
        {

            var memberOne = new Member();

            var memberTwo = new Member();

            var newGroup = memberTwo.createGroup("GroupOne");

            memberOne.joinGroup(newGroup);

            memberOne.joinGroup(newGroup);

        }

        [Test]
        [ExpectedException(typeof(AlreadyJoinedException))]
        public void test_one_administrator_tries_to_join_group()
        {

            var memberOne = new Member();

            var newGroup = memberOne.createGroup("GroupOne");

            memberOne.joinGroup(newGroup);

        }

        [Test]
        public void test_one_member_leaves_group()
        {

            var memberOne = new Member();

            var memberTwo = new Member();

            var newGroup = memberOne.createGroup("GroupOne");

            memberTwo.joinGroup(newGroup);

            memberTwo.leaveGroup(newGroup);

            Assert.AreEqual(0,memberTwo.groups.Count);

            Assert.AreEqual(0,newGroup.members.Count);

        }

        [Test]
        [ExpectedException(typeof(NotJoinedException))]
        public void test_one_unjoined_member_tries_to_leave_group()
        {

            var memberOne = new Member();

            var memberTwo = new Member();

            var newGroup = memberOne.createGroup("GroupOne");

            memberTwo.leaveGroup(newGroup);

        }

        [Test]
        public void test_one_member_register_purchase()
        {

            var memberOne = new Member();

            var memberTwo = new Member();

            var memberThree = new Member();

            var newGroup = memberOne.createGroup("GroupOne");

            memberTwo.joinGroup(newGroup);

            memberThree.joinGroup(newGroup);

            var newPurchase = new Purchase()
            {
                buyer = memberOne,
                debtors = newGroup.members,
                description = "Pelota",
                group = newGroup,
                totalAmount = 120f
            };

            memberOne.registerPurchase(newPurchase);

            Assert.AreEqual(40f, memberTwo.payments.ElementAt(0).amount);

            Assert.AreEqual(40f, memberThree.payments.ElementAt(0).amount);

            Assert.AreEqual(PaymentStatus.Paid, memberOne.payments.ElementAt(0).status);

            Assert.AreEqual(PaymentStatus.Unpaid, memberTwo.payments.ElementAt(0).status);

            Assert.AreEqual(PaymentStatus.Unpaid, memberThree.payments.ElementAt(0).status);

        }

    }
}
