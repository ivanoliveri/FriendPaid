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
            this.Map(user => user.password).Length(70).Not.LazyLoad();
            this.Map(user => user.email).Not.Nullable().Length(50).Not.LazyLoad();
            this.Map(user => user.name).Not.Nullable().Length(50).Not.LazyLoad();
            this.Map(user => user.lastName).Not.Nullable().Length(50).Not.LazyLoad();
            this.HasManyToMany(user => user.contacts).Table("ContactsPerUser").ParentKeyColumn("id_user").ChildKeyColumn("id_user_contact").AsBag();
            this.HasMany(user => user.notifications).KeyColumn("id_notification").Cascade.All().Not.LazyLoad();
            this.HasMany(user => user.contactRequests).KeyColumn("id_contact_request").Not.LazyLoad();
            this.HasMany(user => user.facebookContacts).KeyColumn("id_facebook_contact").Not.LazyLoad();
            this.HasManyToMany(user => user.groups).Table("MemberPerGroup").ParentKeyColumn("id_user").ChildKeyColumn("id_group").Not.LazyLoad();
            this.HasMany(user => user.payments).KeyColumn("id_payment").Cascade.All().Not.LazyLoad();   
            this.HasManyToMany(user => user.purchases).Table("MemberPerPurchase").ParentKeyColumn("id_user").ChildKeyColumn("id_purchase").Not.LazyLoad();    
           
        }
    }
}
