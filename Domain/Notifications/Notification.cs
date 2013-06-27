using System;
using Domain.Utils;

namespace Domain.Notifications
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
