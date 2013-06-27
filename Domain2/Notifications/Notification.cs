using System;
using Domain2.Utils;

namespace Domain2.Notifications
{
    public class Notification
    {

        #region Attributes

        public string message { get; set; }

        public NotificationStatus status { get; set; }

        public DateTime date { get; set; }

        #endregion

        #region Methods

        public string leer()
        {
            if (status.Equals(NotificationStatus.Unread))
                status = NotificationStatus.Read;
            return message;
        }

        #endregion

    }
}
