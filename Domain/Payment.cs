﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

	    public Member collectorName { set; get; }

        public PaymentStatus status { set; get; }

        #endregion

        #region Methods

        public void registerPayment()
        {
        
        }
	    public void createPurchaseNotification(Purchase purchase,Member debtor)
	    {
	    
	    }

        #endregion

    }
}
