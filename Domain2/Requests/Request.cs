using Domain2.Utils;

namespace Domain2.Requests
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
