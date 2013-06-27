using Domain.Utils;

namespace Domain.Requests
{
    public class ContactRequest : Request
    {

        #region Methods

        public ContactRequest()
        {
            status = RequestStatus.Pending;
        }

        public override void accept()
        {
            status = RequestStatus.Accepted;
        }

        public override void reject()
        {
            status = RequestStatus.Rejected;
        }

        #endregion

    }
}
