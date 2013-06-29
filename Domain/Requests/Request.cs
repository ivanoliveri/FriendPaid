using Domain.Utils;

namespace Domain.Requests
{
    public abstract class Request
    {

        #region Attributes

        public RequestStatus status { get; set; }

        public string message { set; get; }

        public User sender { get; set; }

        public User receiver { get; set; }

        #endregion

        #region Methods

        public abstract void accept();

        public abstract void reject();

        #endregion

    }
}
