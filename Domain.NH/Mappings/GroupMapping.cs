using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Domain.NH.Mappings
{
    public class GroupMapping : ClassMap<Group>
    {
        public GroupMapping()
        {
            this.Id(group => group.id).GeneratedBy.Identity();
            this.Map(group => group.name).Not.Nullable().Length(50).Not.LazyLoad();
            this.References(group => group.administrator).Not.Nullable().Not.LazyLoad();
            this.HasManyToMany(group => group.members).Table("MemberPerGroup").ParentKeyColumn("id_group").ChildKeyColumn("id_user").Not.LazyLoad();
        }
    }
}
