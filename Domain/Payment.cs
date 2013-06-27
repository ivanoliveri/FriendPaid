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

	    public Member debtor { set; get; }

	    public Member collector { set; get; }

        public PaymentStatus status { set; get; }

        #endregion

        #region Methods

        public void registerPayment()
        {
            throw new NotImplementedException();
        }
	    public void createPurchaseNotification(Purchase purchase,Member debtor)
	    {
            throw new NotImplementedException();
	    }

        #endregion

    }
}
