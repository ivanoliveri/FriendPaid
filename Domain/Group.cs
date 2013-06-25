using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Group
    {

        #region Attributes

        public string name { set; get; }

        public Member administrator { set; get; }

        public List<Member> members { set; get; }

        #endregion

        #region Methods

        public void createPurchaseNotifications(Purchase purchase)
        {
    
        }

        public void createPaymentNotification(Payment payment)
        {

        }

        #endregion

    }
}
