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
    public class UserRepository : BaseRepository<User>,IUserRepository
    {

        public UserRepository(IHibernateSessionFactory hibernateSessionFactory)
            : base(hibernateSessionFactory)
        {
        }
        public User GetByUsernameAndPassword(string username, string password)
        {
            var result = this.GetSessionFactory().GetSession().CreateCriteria<User>()
                            .Add(Restrictions.Eq("username", username))
                            .Add(Restrictions.Eq("password", password)).List<User>();

            if (result.Count.Equals(0))
                throw new UserNotFoundException();

            return result.ElementAt(0);
        }

        public User GetByUsername(string username)
        {
            var result = this.GetSessionFactory().GetSession().CreateCriteria<User>()
                            .Add(Restrictions.Eq("username", username)).List<User>();

            if (result.Count.Equals(0))
                throw new UserNotFoundException();

            return result.ElementAt(0);
        }

    }
}
