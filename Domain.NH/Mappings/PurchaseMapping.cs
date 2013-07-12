using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Domain.NH.Mappings
{
    public class PurchaseMapping : ClassMap<Purchase>
    {
        public PurchaseMapping()
        {
            this.Id(purchase => purchase.id).GeneratedBy.Identity();
            this.References(purchase => purchase.buyer).Not.Nullable().Not.LazyLoad();
            this.Map(purchase => purchase.totalAmount).Not.Nullable().Length(50).Not.LazyLoad();
            this.Map(purchase => purchase.description).Not.Nullable().Length(50).Not.LazyLoad();
            this.References(purchase => purchase.group).Not.Nullable().Not.LazyLoad();
            this.HasMany(purchase => purchase.debtors).KeyColumn("id");
        }
    }
}
