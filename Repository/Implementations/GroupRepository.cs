using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using Domain.Exceptions;
using NHibernate.Criterion;
using Repository.Interfaces;

namespace Repository.Implementations
{
    public class GroupRepository:BaseRepository<Group>,IGroupRepository
    {

        public GroupRepository(IHibernateSessionFactory hibernateSessionFactory)
            : base(hibernateSessionFactory)
        {
        }

        public Group GetByName(string groupName)
        {
            var result = this.GetSessionFactory().GetSession().CreateCriteria<Group>()
                            .Add(Restrictions.Eq("name", groupName)).List<Group>();

            if (result.Count.Equals(0))
                throw new GroupNotFoundException();

            return result.ElementAt(0);
        }

        public IList<Group> GetGroupsWhichNamesBeginWith(string groupName)
        {
            var result = this.GetSessionFactory().GetSession().CreateCriteria<Group>()
                .Add(Restrictions.Like("name", groupName, MatchMode.Start)).List<Group>();

            if (result.Count.Equals(0))
                throw new GroupNotFoundException();

            return result;
        }

    }
}
