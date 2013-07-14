using System;
using System.Collections.Generic;
using Domain.Exceptions;
using Domain.Facebook;
using Domain.Notifications;
using Domain.Requests;
using Domain.Utils;

namespace Domain
{
    public class User
    {

        #region Attributes

        public virtual int id { set; get; }

        public virtual string username { set; get; }

        public virtual string password { set; get; }

        public virtual string email { set; get; }

        public virtual string name { set; get; }

        public virtual string lastName { set; get; }

        public virtual IList<User> contacts { set; get; }

        public virtual IList<Notification> notifications { set; get; }

        public virtual IList<ContactRequest> contactRequests { set; get; }

        public virtual IList<FacebookContact> facebookContacts { set; get; }

        public virtual IList<Group> groups { set; get; }

        public virtual IList<Payment> payments { set; get; }

        public virtual IList<Purchase> purchases { set; get; }

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

            facebookContacts = new List<FacebookContact>();
        }

        public virtual float getOwedAmount()
        {
            float total=0f;

            foreach(var payment in payments)
            {
                if (payment.status.Equals(PaymentStatus.Unpaid))
                    total += payment.amount;
            }

            return total;
        }

        public virtual void invite(User user)
        {
            throw new NotImplementedException();
        }

        public virtual bool hasDebts()
        {
            return this.getOwedAmount() > 0f;
        }

        public virtual void joinGroup(Group group)
        {
            if (this.groups.Contains(group))
                throw new AlreadyJoinedException();

            group.members.Add(this);

            this.groups.Add(group);
        }

        public virtual Group createGroup(string groupName)
        {
            var newGroup = new Group() { administrator = this, name = groupName };
            
            this.groups.Add(newGroup);

            return newGroup;
        }

        public virtual void leaveGroup(Group group)
        {
            if (group.administrator.Equals(this))
                throw new AdministratorCantLeaveGroupException();

            if (!group.members.Contains(this))
                throw new NotJoinedException();

            this.groups.Remove(group);

            group.members.Remove(this);

        }

        public virtual void registerPurchase(Purchase purchase)
        {

            purchase.group.createPurchaseNotifications(purchase);

            purchase.group.createPaymentsAfterPurchase(purchase);

        }

        public virtual void registerDebtPayment(Payment payment, Group group)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
