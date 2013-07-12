using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;

namespace Repository.Implementations
{
    public class UserRepository : BaseRepository<User>
    {

        public UserRepository(IHibernateSessionFactory hibernateSessionFactory)
            : base(hibernateSessionFactory)
        {
        }

    }
}
