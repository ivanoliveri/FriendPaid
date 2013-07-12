using Domain.Utils;

namespace Domain.Requests
{
    public class Request
    {

        #region Attributes

        public virtual int id { set; get; }

        public virtual RequestStatus status { get; set; }

        public virtual string message { set; get; }

        public virtual User sender { get; set; }

        public virtual User receiver { get; set; }

        #endregion

        #region Methods

        public Request()
        {
            
        }

        public virtual void accept(){}

        public virtual void reject(){}

        #endregion

    }
}
