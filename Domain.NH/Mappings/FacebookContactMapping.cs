using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Facebook;
using FluentNHibernate.Mapping;

namespace Domain.NH.Mappings
{
    public class FacebookContactMapping : ClassMap<FacebookContact>
    {
        public FacebookContactMapping()
        {
            this.Id(facebookContact => facebookContact.id).GeneratedBy.Identity(); 
            this.Map(facebookContact => facebookContact.name).Not.Nullable().Length(50).Not.LazyLoad();
        }
    }
}
