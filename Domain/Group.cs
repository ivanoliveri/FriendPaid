using System;
using System.Collections.Generic;

namespace Domain
{
    public class Group
    {

        #region Attributes

        public string name { set; get; }

        public User administrator { set; get; }

        public List<User> members { set; get; }

        #endregion

        #region Methods

        public Group()
        {
            members = new List<User>();
        }

        public void createPurchaseNotifications(Purchase purchase)
        {
            throw new NotImplementedException();
        }

        public void createPaymentNotification(Payment payment)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
