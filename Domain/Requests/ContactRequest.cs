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

        public virtual void generateMessage()
        {
            message = "El usuario " + sender.username + " te ha enviado una solicitud de contacto.";
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
