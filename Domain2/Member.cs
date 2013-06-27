using System;
using System.Collections.Generic;
using Domain2.Notifications;
using Domain2.Requests;

namespace Domain2
{
    public class Member
    {

        #region Attributes

        public string username { set; get; }

        public string password { set; get; }

        public string name { set; get; }

        public string lastName { set; get; }

        public string email { set; get; }

        public List<Member> contacts { set; get; }

        public List<Notification> notifications { set; get; }

        public List<ContactRequest> contactRequests { set; get; }

        public List<Group> groups { set; get; }

        public List<Payment> payments { set; get; }

        public List<Purchase> purchases { set; get; }

        #endregion

        #region Methods

        public float getOwedAmount()
        {
            throw new NotImplementedException();
        }

        public void invite(Member member)
        {
            throw new NotImplementedException();
        }

        public bool hasDebts()
        {
            throw new NotImplementedException();
        }

        public void joinGroup(Group group)
        {
            throw new NotImplementedException();
        }

        public Group createGroup(string name)
        {
            throw new NotImplementedException();
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
