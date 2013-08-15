using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Utils;

namespace Domain.Notifications
{
    public class CrossDebtsNotification:Notification
    {
        
        #region Attributes

        public virtual User buyer { get; set; }
        public virtual User debtor { get; set; }
        public virtual IList<Payment> simplifiedPayments { set; get; }
        public virtual Payment finalPayment { get; set; }
        public virtual Payment initialPayment { get; set; }

        #endregion

        public CrossDebtsNotification(){}

        public CrossDebtsNotification(User buyer, User debtor, IList<Payment> payments, Payment fPayment, Payment iPayment, string message)
        {
            this.buyer = buyer;
            this.debtor = debtor;
            simplifiedPayments = payments;
            finalPayment = fPayment;
            initialPayment = iPayment;
            this.message = message;

            status = NotificationStatus.Unread;

            date = DateTime.Now.Date;
        }
    }
}
