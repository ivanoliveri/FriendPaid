using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Notifications;
using FluentNHibernate.Mapping;

namespace Domain.NH.Mappings
{
    public class NotificationMapping : ClassMap<Notification>
    {
        public NotificationMapping()
        {
            this.Id(notification => notification.id).GeneratedBy.Identity(); 
            this.Map(notification => notification.message).Not.Nullable().Length(350).Not.LazyLoad();
            this.Map(notification => notification.status).Not.Nullable().Length(50).Not.LazyLoad();
            this.Map(notification => notification.date).Not.Nullable().Length(50).Not.LazyLoad();
            this.JoinedSubClass<PurchaseNotification>("PurchaseNotificationID", purchaseNotification => purchaseNotification.Map(notification => notification.id));
            this.JoinedSubClass<PaymentNotification>("PaymentNotificationID", paymentNotification => paymentNotification.Map(notification => notification.id));
            this.JoinedSubClass<CrossDebtsNotification>("CrossDebtsNotificationID", crossDebtsNotification => crossDebtsNotification.Map(notification => notification.id));
        }
     

    }
}
