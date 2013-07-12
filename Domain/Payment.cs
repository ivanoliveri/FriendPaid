using System;
using Domain.Utils;

namespace Domain
{
    public class Payment
    {

        #region Attributes

        public virtual int id { set; get; }

        public virtual float amount { set; get; }

        public virtual string description { set; get; }

        public virtual Group group { set; get; }

        public virtual User debtor { set; get; }

        public virtual User buyer { set; get; }

        public virtual PaymentStatus status { set; get; }

        #endregion

        #region Methods

        public Payment()
        {
            
        }

        public virtual void registerPayment()
        {
            throw new NotImplementedException();
        }
        public virtual void createPurchaseNotification(Purchase purchase, User debtor)
	    {
            throw new NotImplementedException();
	    }

        #endregion

    }
}
