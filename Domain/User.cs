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

        public virtual bool equals(User user)
        {
            return this.username == user.username;
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
            contacts.RemoveAt(getContactPosition(user));
            user.contacts.RemoveAt(user.getContactPosition(this));
        }

        public virtual int getContactPosition(User user)
        {
            for(int i = 0;i<contacts.Count;i++)            
                if (contacts[i].equals(user)) 
                    return i;
            
            throw new ContactNotFoundException();
        }

        public virtual int getGroupPosition(Group group)
        {
            for (int i = 0; i < groups.Count; i++)
            {
                if (groups[i].equals(group)) return i;
            }
            throw new GroupNotFoundException();
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

            if(group.members.Count(oneMember=>oneMember.username.Equals(this.username)).Equals(0))
                throw new NotJoinedException();

            this.groups.RemoveAt(getGroupPosition(group));

            group.removeMember(this);

        }

        public virtual void registerPurchase(Purchase purchase)
        {
            if (purchase.debtors.Count.Equals(0))
                throw new CantRegisterPurchaseBecauseThereAreNoMembersException();

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
            contactRequest.status=RequestStatus.Cancelled;
        }

        public virtual void rejectContactRequest(ContactRequest contactRequest)
        {
            contactRequest.status = RequestStatus.Rejected;
        }

        public virtual ContactRequest getContactPendingRequestFrom(User sender)
        {
            foreach (var contactRequest in contactRequests)
            {
                if (contactRequest.sender.username.Equals(sender.username) && contactRequest.status.Equals(RequestStatus.Pending))
                    return contactRequest;
            }
            return null;
        }

        public virtual void registerDebtPayment(Payment payment, Group group)
        {
            throw new NotImplementedException();
        }

        public virtual void addDebt(Payment debt)
        {
            //filtra una lista en la que solo esten los pagos pendientes del comprador al deudor
            List<Payment> filteredList = debt.buyer.payments.Where(x => x.status.Equals(PaymentStatus.Unpaid) && x.buyer.id.Equals(this.id)).ToList();
            if (filteredList.Count == 0)
            {
                payments.Add(debt);
                return;
            }

            CrossDebtsNotificationBuilder crossDebts = new CrossDebtsNotificationBuilder(debt);

            foreach (Payment payment in filteredList)
            {
                if (debt.amount >= payment.amount)
                {
                    //si la nueva deuda es mayor que la deuda cruzada, le resto el valor y vuelvo a iterar
                    payment.status = PaymentStatus.Paid;
                    crossDebts.simplifiedPayments.Add(payment);
                    debt.amount -= payment.amount;
                    if (debt.amount.Equals(0))
                    {//si la nueva deuda resulta en 0 se anularon entre si las deudas, termina el ciclo
                        debt.status = PaymentStatus.Paid;
                        break;
                    }
                }
                else
                {
                    payment.amount -= debt.amount;
                    debt.status = PaymentStatus.Paid;
                    crossDebts.finalPayment = payment;
                    payments.Add(debt);
                    crossDebts.sendNotifications();
                    return;
                }
            }

            //si terminado el ciclo sigue habiendo deuda a pagar, se la agrega a la lista de deudas
            if (debt.amount > 0)
            {
                crossDebts.finalPayment = debt;
                payments.Add(debt);
            }

            crossDebts.sendNotifications();
        }

        #endregion

    }
}
