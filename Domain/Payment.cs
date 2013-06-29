using System;
using Domain.Utils;

namespace Domain
{
    public class Payment
    {

        #region Attributes

        public float amount { set; get; }

	    public string description { set; get; }

	    public Group group { set; get; }

	    public User debtor { set; get; }

	    public User buyer { set; get; }

        public PaymentStatus status { set; get; }

        #endregion

        #region Methods

        public void registerPayment()
        {
            throw new NotImplementedException();
        }
	    public void createPurchaseNotification(Purchase purchase,User debtor)
	    {
            throw new NotImplementedException();
	    }

        #endregion

    }
}
