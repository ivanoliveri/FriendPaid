using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Domain.NH.Mappings
{
    public class UserMapping : ClassMap<User>
    {
        public UserMapping()
        {
            this.Id(user => user.id).GeneratedBy.Identity();
            this.Map(user => user.username).Not.Nullable().Length(50).Not.LazyLoad();
            this.Map(user => user.password).Not.Nullable().Length(50).Not.LazyLoad();
            this.Map(user => user.email).Not.Nullable().Length(50).Not.LazyLoad();
            this.Map(user => user.name).Not.Nullable().Length(50).Not.LazyLoad();
            this.Map(user => user.lastName).Not.Nullable().Length(50).Not.LazyLoad();
            this.HasManyToMany(user => user.contacts).Table("ContactsPerUser").AsBag();
            this.HasMany(user => user.notifications).KeyColumn("id");
            this.HasMany(user => user.contactRequests).KeyColumn("id");
            this.HasMany(user => user.facebookContacts).KeyColumn("id");
            this.HasManyToMany(user => user.groups).Cascade.All().Inverse().Table("MembersPerGroup");
            this.HasMany(user => user.payments).KeyColumn("id");     
            this.HasMany(user => user.purchases).KeyColumn("id"); 
        }
    }
}
