using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Requests;
using Domain.Utils;
using FluentNHibernate.Mapping;

namespace Domain.NH.Mappings
{
    public class ContactRequestMapping : ClassMap<ContactRequest>
    {
        public ContactRequestMapping()
        {
            this.Id(contactRequest => contactRequest.id).GeneratedBy.Identity(); 
            this.Map(contactRequest => contactRequest.status).Not.Nullable().Length(50).Not.LazyLoad();
            this.Map(contactRequest => contactRequest.message).Not.Nullable().Length(50).Not.LazyLoad();
            this.References(contactRequest => contactRequest.sender).Not.Nullable().Not.LazyLoad();
            this.References(contactRequest => contactRequest.receiver).Not.Nullable().Not.LazyLoad();
        }
    }
}
