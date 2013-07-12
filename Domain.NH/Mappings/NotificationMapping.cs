﻿using System;
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
            this.Map(notification => notification.message).Not.Nullable().Length(50).Not.LazyLoad();
            this.Map(notification => notification.status).Not.Nullable().Length(50).Not.LazyLoad();
            this.Map(notification => notification.date).Not.Nullable().Length(50).Not.LazyLoad();
        }
     

    }
}