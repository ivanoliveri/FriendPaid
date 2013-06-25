using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Utils;

namespace Domain.Requests
{
    public abstract class Request
    {

        #region Attributes

        public RequestStatus status { get; set; }

        public string message { set; get; }

        public Member sender { get; set; }

        public Member receiver { get; set; }

        #endregion

        #region Methods

        public abstract void accept();

        public abstract void reject();

        #endregion

    }
}
