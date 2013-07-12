using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;

namespace Repository.Implementations
{
    public class GroupRepository:BaseRepository<Group>
    {

        public GroupRepository(IHibernateSessionFactory hibernateSessionFactory)
            : base(hibernateSessionFactory)
        {
        }

    }
}
