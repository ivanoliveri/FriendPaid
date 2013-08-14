using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Requests;
using Domain.Utils;
using FluentNHibernate.Mapping;

namespace Domain.NH.Mappings
{
    public class RequestMapping : ClassMap<Request>
    {
        public RequestMapping()
        {
            this.Id(request => request.id).GeneratedBy.Identity();
            this.Map(request => request.status).Not.Nullable().Length(100).Not.LazyLoad();
            this.Map(request => request.message).Not.Nullable().Length(100).Not.LazyLoad();
            this.References(request => request.sender).Not.Nullable().Not.LazyLoad();
            this.References(request => request.receiver).Not.Nullable().Not.LazyLoad();
            this.JoinedSubClass<ContactRequest>("ContactRequestID", sm => sm.Map(x => x.id));
        }
    }
}
