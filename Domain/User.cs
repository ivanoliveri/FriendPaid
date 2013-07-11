using System;
using System.Collections.Generic;
using Domain.Exceptions;
using Domain.Notifications;
using Domain.Requests;
using Domain.Utils;

namespace Domain
{
    public class User
    {

        #region Attributes

        public string username { set; get; }

        public string password { set; get; }

        public string email { set; get; }

        public string name { set; get; }

        public string lastName { set; get; }

        public List<User> contacts { set; get; }

        public List<Notification> notifications { set; get; }

        public List<ContactRequest> contactRequests { set; get; }

        public List<Group> groups { set; get; }

        public List<Payment> payments { set; get; }

        public List<Purchase> purchases { set; get; }

        #endregion

        #region Methods
       
        public User()
        {
            contacts = new List<User>();

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

        public void invite(User user)
        {
            throw new NotImplementedException();
        }

        public bool hasDebts()
        {
            return this.getOwedAmount() > 0f;
        }

        public void joinGroup(Group group)
        {
            if (this.groups.Contains(group))
                throw new AlreadyJoinedException();

            group.members.Add(this);

            this.groups.Add(group);
        }

        public Group createGroup(string groupName)
        {
            var newGroup = new Group() { administrator = this, name = groupName };
            
            this.groups.Add(newGroup);

            return newGroup;
        }

        public void leaveGroup(Group group)
        {
            if (group.administrator.Equals(this))
                throw new AdministratorCantLeaveGroupException();

            if (!group.members.Contains(this))
                throw new NotJoinedException();

            this.groups.Remove(group);

            group.members.Remove(this);

        }

        public void registerPurchase(Purchase purchase)
        {

            purchase.group.createPurchaseNotifications(purchase);

            purchase.group.createPaymentsAfterPurchase(purchase);

        }

        public void registerDebtPayment(Payment payment,Group group)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
