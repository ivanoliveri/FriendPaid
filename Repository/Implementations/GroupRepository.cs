using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using Repository.Interfaces;

namespace Repository.Implementations
{
    public class GroupRepository:BaseRepository<Group>,IGroupRepository
    {

        public GroupRepository(IHibernateSessionFactory hibernateSessionFactory)
            : base(hibernateSessionFactory)
        {
        }

    }
}
