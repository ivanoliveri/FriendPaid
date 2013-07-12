using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Domain.NH.Mappings
{
    public class PaymentMapping : ClassMap<Payment>
    {
        public PaymentMapping()
        {
            this.Id(payment => payment.id).GeneratedBy.Identity();
            this.Map(payment => payment.amount).Not.Nullable().Length(50).Not.LazyLoad();
            this.Map(payment => payment.description).Not.Nullable().Length(50).Not.LazyLoad();
            this.References(payment => payment.debtor).Not.Nullable().Not.LazyLoad();
            this.References(payment => payment.buyer).Not.Nullable().Not.LazyLoad();
            this.Map(payment => payment.status).Not.Nullable().Length(50).Not.LazyLoad();
        }

    }
}
