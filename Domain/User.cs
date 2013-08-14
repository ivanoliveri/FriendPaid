using System;
using System.Collections.Generic;
using System.Linq;
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

        public virtual void addContact(User user)
        {
            this.contacts.Add(user);
            user.contacts.Add(this);
        }

        public virtual void removeContact(User user)
        {
            var found = false;
            var index = 0;

            foreach (var oneUser in this.contacts)
            {
                if (oneUser.username != user.username)
                {
                    index++;
                }else{
                    found = true;
                }
            }

            this.contacts.RemoveAt(index);
            
            found = false;
            index = 0;

            foreach (var oneUser in user.contacts)
            {
                if (oneUser.username != this.username){
                    index++;
                }else{
                    found = true;
                }
            }

            user.contacts.RemoveAt(index);
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
            if (group.administrator.username.Equals(this.username))
                throw new AdministratorCantLeaveGroupException();

            var found = false;
            var index = 0;

            foreach (var oneGroup in groups){
                if(oneGroup.name!=group.name){
                    index++;
                }else{
                    found = true;
                }
            }

            this.groups.RemoveAt(index);

            found = false;
            index = 0;

            foreach (var oneMember in group.members)
            {
                if (oneMember.username != this.username){
                    index++;
                }else{
                    found = true;
                }
            }

            group.members.RemoveAt(index);

        }

        public virtual void registerPurchase(Purchase purchase)
        {

            purchase.group.createPurchaseNotifications(purchase);

            purchase.group.createPaymentsAfterPurchase(purchase);

        }

        public virtual void sendContactRequest(User receiver)
        {
            var newContactRequest = new ContactRequest()
                                        {
                                            receiver = receiver,
                                            sender = this
                                        };

            newContactRequest.generateMessage();

            receiver.contactRequests.Add(newContactRequest);
        }

        public virtual void aceptContactRequest(ContactRequest contactRequest)
        {
            this.addContact(contactRequest.sender);

            contactRequest.accept();
        }

        public virtual void deleteContactRequest(ContactRequest contactRequest)
        {
            var found = false;
            var index = 0;

            foreach (var oneContactRequest in this.contactRequests)
            {
                if (oneContactRequest.id != contactRequest.id)
                {
                    index++;
                }else{
                    found = true;
                }
            }

            this.contactRequests.RemoveAt(index);

        }

        public virtual ContactRequest getContactRequestFrom(User sender)
        {
            foreach (var contactRequest in contactRequests)
            {
                if (contactRequest.sender.username.Equals(sender.username))
                    return contactRequest;
            }
            return null;
        }

        public virtual void registerDebtPayment(Payment payment, Group group)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
