using System;
using Domain.Utils;

namespace Domain.Notifications
{
    public class Notification
    {

        #region Attributes

        public virtual int id { set; get; }

        public virtual string message { get; set; }

        public virtual NotificationStatus status { get; set; }

        public virtual DateTime date { get; set; }

        #endregion

        #region Methods

        public Notification()
        {
            
        }

        public virtual string leer()
        {
            if (status.Equals(NotificationStatus.Unread))
                status = NotificationStatus.Read;
            return message;
        }

        #endregion

    }
}
