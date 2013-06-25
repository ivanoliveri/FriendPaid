using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Notifications;
using Domain.Requests;

namespace Domain
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
            
        }

        public void invite(Member member)
        {
            
        }

        public bool hasDebts()
        {
            
        }

        public void joinGroup(Group group)
        {
            
        }

        public Group createGroup(string name)
        {
            
        }

        public void leaveGroup(Group group)
        {
            
        }

        public void registerPurchase(Purchase purchase)
        {
            
        }

        public void registerDebtPayment(Payment payment,Group group)
        {

        }

        #endregion

    }
}
