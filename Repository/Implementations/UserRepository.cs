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
        public User GetByUsernameAndPassword(string username, string password)
        {
            //GetSessionFactory().SessionInterceptor(()=>
                                                       //{
                                                        //   result = this.GetByUsernameAndPassword(username, password);
                                                      // });
            return (User) this.GetSessionFactory().GetSession().QueryOver<User>()
                              .Where(user => user.username.Equals(username) &&
                                             user.password.Equals(password));
        }

    }
}
