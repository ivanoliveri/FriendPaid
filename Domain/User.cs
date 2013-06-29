using System;
using System.Collections.Generic;
using Domain.Notifications;
using Domain.Requests;
using Domain.Utils;

namespace Domain
{
    public class Member
    {

        #region Attributes

        public string username { set; get; }

        public List<Member> contacts { set; get; }

        public List<Notification> notifications { set; get; }

        public List<ContactRequest> contactRequests { set; get; }

        public List<Group> groups { set; get; }

        public List<Payment> payments { set; get; }

        public List<Purchase> purchases { set; get; }

        #endregion

        #region Methods
       
        public Member()
        {
            contacts = new List<Member>();

            notifications = new List<Notification>();

            contactRequests = new List<ContactRequest>();

            groups = new List<Group>();

            payments = new List<Payment>();

            purchases = new List<Purchase>();
        }

        public float getOwedAmount()
        {
            float total=0f;

            foreach(var payment in payments)
            {
                if (payment.status.Equals(PaymentStatus.Unpaid))
                    total += payment.amount;
            }

            return total;
        }

        public void invite(Member member)
        {
            throw new NotImplementedException();
        }

        public bool hasDebts()
        {
            return this.getOwedAmount() > 0f;
        }

        public void joinGroup(Group group)
        {
            throw new NotImplementedException();
        }

        public Group createGroup(string groupName)
        {
            var newGroup = new Group() { administrator = this, members = null, name = groupName };
            
            this.groups.Add(newGroup);

            return newGroup;
        }

        public void leaveGroup(Group group)
        {
            throw new NotImplementedException();
        }

        public void registerPurchase(Purchase purchase)
        {
            throw new NotImplementedException();
        }

        public void registerDebtPayment(Payment payment,Group group)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
